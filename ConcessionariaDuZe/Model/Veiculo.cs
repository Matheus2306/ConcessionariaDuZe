using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ConcessionariaDuZe.Model
{
    public class Veiculo
    {
        public Guid VeiculoId { get; set; }
        [Required(ErrorMessage = "O campo marca não pode ser nulo")]
        public string Nome { get; set; }
        public string Marca { get; set; }
        [Required(ErrorMessage = "O campo modelo não pode ser nulo")]
        public string Modelo { get; set; }
        [Required(ErrorMessage = "O campo ano não pode ser nulo")]
        public string Ano { get; set; }
        [Required(ErrorMessage = "O campo valor não pode ser nulo")]
        public decimal Valor { get; set; }
        [Required(ErrorMessage = "O campo quilometragem não pode ser nulo")]
        public decimal Quilometragem { get; set; }
        [Required(ErrorMessage = "O campo quilometragem não pode ser nulo")]
        public string TipoDeCombustivel { get; set; }
        [Required(ErrorMessage = "O campo quilometragem não pode ser nulo")]
        public string Transmissao { get; set; }
        [Required(ErrorMessage = "O campo quilometragem não pode ser nulo")]
        public int Estoque { get; set; }
        [Required(ErrorMessage = "O campo quilometragem não pode ser nulo")]
        public string Imagem { get; set; }
    }
}
