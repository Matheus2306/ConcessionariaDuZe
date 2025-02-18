using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConcessionariaDuZe.Data;
using ConcessionariaDuZe.Model;
using Microsoft.AspNetCore.Authorization;

namespace ConcessionariaDuZe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormaDePagamentosController : ControllerBase
    {
        private readonly DBContext _context;

        public FormaDePagamentosController(DBContext context)
        {
            _context = context;
        }

        // GET: api/FormaDePagamentos
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<IEnumerable<FormaDePagamento>>> GetFormaDePagamento()
        {
            return await _context.FormaDePagamento.ToListAsync();
        }

        // GET: api/FormaDePagamentos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FormaDePagamento>> GetFormaDePagamento(Guid id)
        {
            var formaDePagamento = await _context.FormaDePagamento.FindAsync(id);

            if (formaDePagamento == null)
            {
                return NotFound();
            }

            return formaDePagamento;
        }

        // PUT: api/FormaDePagamentos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFormaDePagamento(Guid id, FormaDePagamento formaDePagamento)
        {
            if (id != formaDePagamento.FormaDePagamentoId)
            {
                return BadRequest();
            }

            _context.Entry(formaDePagamento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FormaDePagamentoExists(id))
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

        // POST: api/FormaDePagamentos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FormaDePagamento>> PostFormaDePagamento(FormaDePagamento formaDePagamento)
        {
            _context.FormaDePagamento.Add(formaDePagamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFormaDePagamento", new { id = formaDePagamento.FormaDePagamentoId }, formaDePagamento);
        }

        // DELETE: api/FormaDePagamentos/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteFormaDePagamento(Guid id)
        {
            var formaDePagamento = await _context.FormaDePagamento.FindAsync(id);
            if (formaDePagamento == null)
            {
                return NotFound();
            }

            _context.FormaDePagamento.Remove(formaDePagamento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FormaDePagamentoExists(Guid id)
        {
            return _context.FormaDePagamento.Any(e => e.FormaDePagamentoId == id);
        }
    }
}
