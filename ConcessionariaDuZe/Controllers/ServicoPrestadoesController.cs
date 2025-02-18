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
    public class ServicoPrestadoesController : ControllerBase
    {
        private readonly DBContext _context;

        public ServicoPrestadoesController(DBContext context)
        {
            _context = context;
        }

        // GET: api/ServicoPrestadoes
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<IEnumerable<ServicoPrestado>>> GetServicoPrestado()
        {
            return await _context.ServicoPrestado.ToListAsync();
        }

        // GET: api/ServicoPrestadoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServicoPrestado>> GetServicoPrestado(Guid id)
        {
            var servicoPrestado = await _context.ServicoPrestado.FindAsync(id);

            if (servicoPrestado == null)
            {
                return NotFound();
            }

            return servicoPrestado;
        }

        // PUT: api/ServicoPrestadoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServicoPrestado(Guid id, ServicoPrestado servicoPrestado)
        {
            if (id != servicoPrestado.ServicoPrestadoId)
            {
                return BadRequest();
            }

            _context.Entry(servicoPrestado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServicoPrestadoExists(id))
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

        // POST: api/ServicoPrestadoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ServicoPrestado>> PostServicoPrestado(ServicoPrestado servicoPrestado)
        {
            _context.ServicoPrestado.Add(servicoPrestado);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetServicoPrestado", new { id = servicoPrestado.ServicoPrestadoId }, servicoPrestado);
        }

        // DELETE: api/ServicoPrestadoes/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteServicoPrestado(Guid id)
        {
            var servicoPrestado = await _context.ServicoPrestado.FindAsync(id);
            if (servicoPrestado == null)
            {
                return NotFound();
            }

            _context.ServicoPrestado.Remove(servicoPrestado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ServicoPrestadoExists(Guid id)
        {
            return _context.ServicoPrestado.Any(e => e.ServicoPrestadoId == id);
        }
    }
}
