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
    [Authorize(Roles = "Admin")]
    public class FornecedorsController : ControllerBase
    {
        private readonly DBContext _context;

        public FornecedorsController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Fornecedors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fornecedor>>> GetFornecedor()
        {
            return await _context.Fornecedor.ToListAsync();
        }

        // GET: api/Fornecedors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Fornecedor>> GetFornecedor(Guid id)
        {
            var fornecedor = await _context.Fornecedor.FindAsync(id);

            if (fornecedor == null)
            {
                return NotFound();
            }

            return fornecedor;
        }

        // PUT: api/Fornecedors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFornecedor(Guid id, Fornecedor fornecedor)
        {
            if (id != fornecedor.FornecedorId)
            {
                return BadRequest();
            }

            _context.Entry(fornecedor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FornecedorExists(id))
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

        // POST: api/Fornecedors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Fornecedor>> PostFornecedor(Fornecedor fornecedor)
        {
            _context.Fornecedor.Add(fornecedor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFornecedor", new { id = fornecedor.FornecedorId }, fornecedor);
        }

        // DELETE: api/Fornecedors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFornecedor(Guid id)
        {
            var fornecedor = await _context.Fornecedor.FindAsync(id);
            if (fornecedor == null)
            {
                return NotFound();
            }

            _context.Fornecedor.Remove(fornecedor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FornecedorExists(Guid id)
        {
            return _context.Fornecedor.Any(e => e.FornecedorId == id);
        }
    }
}
