using Microsoft.EntityFrameworkCore;

namespace ConcessionariaDuZe.Model
{
    public class Veiculos
    {
        public Guid VeiculosId { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Ano { get; set; }
        public decimal Valor { get; set; }
    }
}
