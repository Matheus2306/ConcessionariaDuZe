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
    public class itemComprasController : ControllerBase
    {
        private readonly DBContext _context;

        public itemComprasController(DBContext context)
        {
            _context = context;
        }

        // GET: api/itemCompras
        [HttpGet]
        public async Task<ActionResult<IEnumerable<itemCompra>>> GetitemCompra()
        {
            return await _context.itemCompra.ToListAsync();
        }

        // GET: api/itemCompras/5
        [HttpGet("{id}")]
        public async Task<ActionResult<itemCompra>> GetitemCompra(Guid id)
        {
            var itemCompra = await _context.itemCompra.FindAsync(id);

            if (itemCompra == null)
            {
                return NotFound();
            }

            return itemCompra;
        }

        // PUT: api/itemCompras/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutitemCompra(Guid id, itemCompra itemCompra)
        {
            if (id != itemCompra.ItemCompraId)
            {
                return BadRequest();
            }

            _context.Entry(itemCompra).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!itemCompraExists(id))
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

        // POST: api/itemCompras
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<itemCompra>> PostitemCompra(itemCompra itemCompra)
        {
            _context.itemCompra.Add(itemCompra);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetitemCompra", new { id = itemCompra.ItemCompraId }, itemCompra);
        }

        // DELETE: api/itemCompras/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteitemCompra(Guid id)
        {
            var itemCompra = await _context.itemCompra.FindAsync(id);
            if (itemCompra == null)
            {
                return NotFound();
            }

            _context.itemCompra.Remove(itemCompra);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool itemCompraExists(Guid id)
        {
            return _context.itemCompra.Any(e => e.ItemCompraId == id);
        }
    }
}
