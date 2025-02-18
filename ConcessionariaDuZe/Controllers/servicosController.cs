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
    [Authorize(Roles = "vendedor")]
    public class servicosController : ControllerBase
    {
        private readonly DBContext _context;

        public servicosController(DBContext context)
        {
            _context = context;
        }

        // GET: api/servicos
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<IEnumerable<servico>>> Getservico()
        {
            return await _context.servico.ToListAsync();
        }

        // GET: api/servicos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<servico>> Getservico(Guid id)
        {
            var servico = await _context.servico.FindAsync(id);

            if (servico == null)
            {
                return NotFound();
            }

            return servico;
        }

        // PUT: api/servicos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putservico(Guid id, servico servico)
        {
            if (id != servico.ServicoId)
            {
                return BadRequest();
            }

            _context.Entry(servico).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!servicoExists(id))
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

        // POST: api/servicos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<servico>> Postservico(servico servico)
        {
            _context.servico.Add(servico);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getservico", new { id = servico.ServicoId }, servico);
        }

        // DELETE: api/servicos/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Deleteservico(Guid id)
        {
            var servico = await _context.servico.FindAsync(id);
            if (servico == null)
            {
                return NotFound();
            }

            _context.servico.Remove(servico);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool servicoExists(Guid id)
        {
            return _context.servico.Any(e => e.ServicoId == id);
        }
    }
}
