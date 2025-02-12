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

            return NoContent();
        }

        private bool ItemvendaExists(Guid id)
        {
            return _context.Itemvenda.Any(e => e.ItemVendaId == id);
        }
    }
}
