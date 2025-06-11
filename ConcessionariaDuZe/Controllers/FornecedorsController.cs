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
    [Authorize]
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

        [HttpGet("BuscarPorNome")]
        public async Task<ActionResult<IEnumerable<Fornecedor>>> BuscarPorNome([FromQuery] string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                return BadRequest("O nome do fornecedor deve ser informado.");
            }

            var fornecedores = await _context.Fornecedor
                .Where(f => EF.Functions.Like(f.NomeFornecedor, $"%{nome}%"))
                .ToListAsync();

            if (fornecedores == null || fornecedores.Count == 0)
            {
                return NotFound("Nenhum fornecedor encontrado com esse nome.");
            }

            return fornecedores;
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
            if (fornecedor == null)
            {
                BadRequest("O fornecedor não pode ser nulo");
            }

            bool cnpjValido = ValidarCNPJ(fornecedor.CNPJ);
            if (!cnpjValido)
            {
                return BadRequest("CNPJ Invalido");
            }

            // Verifica se já existe um fornecedor com o mesmo CNPJ
            var fornecedorExistente = await _context.Fornecedor
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.CNPJ == fornecedor.CNPJ);
            if (fornecedorExistente != null)
            {
                return BadRequest("Já existe um fornecedor cadastrado com este CNPJ.");
            }


            _context.Fornecedor.Add(fornecedor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFornecedor", new { id = fornecedor.FornecedorId }, fornecedor);
        }

        public static bool ValidarCNPJ(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
                return false;

            cnpj = new string(cnpj.Where(char.IsDigit).ToArray());

            if (cnpj.Length != 14)
                return false;

            // CNPJs com todos os dígitos iguais são inválidos
            if (cnpj.Distinct().Count() == 1)
                return false;

            int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCnpj = cnpj.Substring(0, 12);
            int soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            int resto = (soma % 11);
            int digito1 = resto < 2 ? 0 : 11 - resto;

            tempCnpj += digito1;
            soma = 0;

            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);
            int digito2 = resto < 2 ? 0 : 11 - resto;

            return cnpj.EndsWith($"{digito1}{digito2}");
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
