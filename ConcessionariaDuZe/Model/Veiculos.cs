using Microsoft.EntityFrameworkCore;

namespace ConcessionariaDuZe.Model
{
    public class Veiculos
    {
        public Guid VeiculoId { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Ano { get; set; }
        public decimal Valor { get; set; }
        public decimal Quilometragem { get; set; }
        public string TipoDeCombustivel { get; set; }
        public string Transmissao { get; set; }
        public int Estoque { get; set; }
    }
}
