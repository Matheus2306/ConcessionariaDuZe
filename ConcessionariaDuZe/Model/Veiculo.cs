using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ConcessionariaDuZe.Model
{
    public class Veiculo
    {
        public Guid VeiculoId { get; set; }
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo marca não pode ser nulo")]
        public string Marca { get; set; }
        [Required(ErrorMessage = "O campo modelo não pode ser nulo")]
        public string Modelo { get; set; }
        [Required(ErrorMessage = "O campo ano não pode ser nulo")]
        public string Ano { get; set; }
        [Required(ErrorMessage = "O campo valor não pode ser nulo")]
        public decimal Valor { get; set; }
        public decimal Quilometragem { get; set; }
        public string TipoDeCombustivel { get; set; }
        public string Transmissao { get; set; }
        public int Estoque { get; set; }
        public string? Imagem { get; set; }
    }
}
