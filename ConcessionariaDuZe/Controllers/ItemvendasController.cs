using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConcessionariaDuZe.Data;
using ConcessionariaDuZe.Model;

namespace ConcessionariaDuZe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemvendasController : ControllerBase
    {
        private readonly DBContext _context;

        public ItemvendasController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Itemvendas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Itemvenda>>> GetItemvenda()
        {
            return await _context.Itemvenda.ToListAsync();
        }

        // GET: api/Itemvendas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Itemvenda>> GetItemvenda(Guid id)
        {
            var itemvenda = await _context.Itemvenda.FindAsync(id);

            if (itemvenda == null)
            {
                return NotFound();


            }

            return itemvenda;
        }

        // PUT: api/Itemvendas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemvenda(Guid id, Itemvenda itemvenda)
        {
            if (id != itemvenda.ItemVendaId)
            {
                return BadRequest();
            }

            _context.Entry(itemvenda).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemvendaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Itemvendas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Itemvenda>> PostItemvenda(Itemvenda itemvenda)
        {
            _context.Itemvenda.Add(itemvenda);
            await _context.SaveChangesAsync();

            // Atualiza o valor total da venda
            await AtualizarValorTotalVenda(itemvenda.VendaId);

            return CreatedAtAction("GetItemvenda", new { id = itemvenda.ItemVendaId }, itemvenda);
        }

        // DELETE: api/Itemvendas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemvenda(Guid id)
        {
            var itemvenda = await _context.Itemvenda.FindAsync(id);
            if (itemvenda == null)
            {
                return NotFound();
            }

            _context.Itemvenda.Remove(itemvenda);
            await _context.SaveChangesAsync();

            // Atualiza o valor total da venda
            await AtualizarValorTotalVenda(itemvenda.VendaId);

            return NoContent();
        }

        private bool ItemvendaExists(Guid id)
        {
            return _context.Itemvenda.Any(e => e.ItemVendaId == id);
        }

        // Método para atualizar o valor total da venda
        private async Task AtualizarValorTotalVenda(Guid vendaId)
        {
            var venda = await _context.Venda.FindAsync(vendaId);
            if (venda != null)
            {
                venda.ValorTotal = await _context.Itemvenda
                    .Where(iv => iv.VendaId == vendaId)
                    .SumAsync(iv => iv.ValorTotal);

                _context.Entry(venda).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }

        // PUT: api/Itemvendas/ConcluirVenda/5
        [HttpPut("ConcluirVenda/{id}")]
        public async Task<IActionResult> ConcluirVenda(Guid id)
        {
            var venda = await _context.Venda.FindAsync(id);
            if (venda == null)
            {
                return NotFound();
            }

            // Atualiza o status da venda para "concluído"
            var statusConcluido = await _context.Status.FirstOrDefaultAsync(s => s.StatusNome == "concluído");
            if (statusConcluido == null)
            {
                return BadRequest("Status 'concluído' não encontrado.");
            }

            venda.StatusId = statusConcluido.StatusId;
            _context.Entry(venda).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool VendaExists(Guid id)
        {
            return _context.Venda.Any(e => e.VendaId == id);
        }
    }
}
