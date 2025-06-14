﻿using System;
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
    //[Authorize]
    public class VeiculosController : ControllerBase
    {
        private readonly DBContext _context;

        public VeiculosController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Veiculos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Veiculo>>> GetVeiculos()
        {
            return await _context.Veiculos.ToListAsync();
        }

        // GET: api/Veiculos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Veiculo>> GetVeiculo(Guid id)
        {
            var veiculo = await _context.Veiculos.FindAsync(id);

            if (veiculo == null)
            {
                return NotFound();
            }

            return veiculo;
        }
        [HttpGet("BuscarModelo")]
        public async Task<ActionResult<IEnumerable<Veiculo>>> BuscarModelo([FromQuery] string modelo)
        {
            if (string.IsNullOrWhiteSpace(modelo))
            {
                return BadRequest("O nome do modelo deve ser informado.");
            }

            var veiculos = await _context.Veiculos
                .Where(v => EF.Functions.Like(v.Modelo, $"%{modelo}%"))
                .ToListAsync();

            if (veiculos == null || veiculos.Count == 0)
            {
                return NotFound("Nenhum veículo encontrado com esse modelo.");
            }

            return veiculos;
        }

        // PUT: api/Veiculos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVeiculo(Guid id, Veiculo veiculo)
        {
            if (id != veiculo.VeiculoId)
            {
                return BadRequest();
            }

            _context.Entry(veiculo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VeiculoExists(id))
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

        [HttpPost("{id}/UploadImagem")]
        public async Task<IActionResult> UploadImagem(Guid id, IFormFile imagem)
        {
            if (imagem == null || imagem.Length == 0)
            {
                return BadRequest("Nenhuma imagem enviada.");
            }

            var veiculo = await _context.Veiculos.FindAsync(id);
            if (veiculo == null)
            {
                return NotFound("Veículo não encontrado.");
            }

            // Exemplo: salvar a imagem em wwwroot/imagens e guardar o caminho no banco
            var extensao = Path.GetExtension(imagem.FileName);
            var nomeArquivo = $"{Guid.NewGuid()}{extensao}";
            var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagens");
            Directory.CreateDirectory(caminhoPasta); // Garante que a pasta existe
            var caminhoCompleto = Path.Combine(caminhoPasta, nomeArquivo);

            using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
            {
                await imagem.CopyToAsync(stream);
            }

            // Salva o caminho relativo no banco
            veiculo.Imagem = $"/imagens/{nomeArquivo}";
            _context.Veiculos.Update(veiculo);
            await _context.SaveChangesAsync();

            return Ok(new { mensagem = "Imagem enviada com sucesso.", caminho = veiculo.Imagem });
        }

        // POST: api/Veiculos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Veiculo>> PostVeiculo(Veiculo veiculo)
        {
            if (veiculo == null)
            {
                return BadRequest("O produto não pode ser nulo");
            }

            // Verifica se já existe um veículo com o mesmo nome, marca, modelo e ano
            var veiculoExistente = await _context.Veiculos
                .AsNoTracking()
                .FirstOrDefaultAsync(v =>
                    v.Nome == veiculo.Nome &&
                    v.Marca == veiculo.Marca &&
                    v.Modelo == veiculo.Modelo &&
                    v.Ano == veiculo.Ano);

            if (veiculoExistente != null)
            {
                return BadRequest("Já existe um veículo cadastrado com o mesmo nome, marca, modelo e ano.");
            }

            _context.Veiculos.Add(veiculo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVeiculo", new { id = veiculo.VeiculoId }, veiculo);
        }

        // DELETE: api/Veiculos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVeiculo(Guid id)
        {
            var veiculo = await _context.Veiculos.FindAsync(id);
            if (veiculo == null)
            {
                return NotFound();
            }

            _context.Veiculos.Remove(veiculo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Veiculos/search
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Veiculo>>> SearchVeiculos(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return BadRequest("Search term cannot be empty.");
            }

            var veiculos = await _context.Veiculos
                .Where(v => v.Nome.Contains(searchTerm) || v.Marca.Contains(searchTerm))
                .ToListAsync();

            if (veiculos == null || veiculos.Count == 0)
            {
                return NotFound("No vehicles found matching the search term.");
            }

            return veiculos;
        }

        private bool VeiculoExists(Guid id)
        {
            return _context.Veiculos.Any(e => e.VeiculoId == id);
        }
    }

}
