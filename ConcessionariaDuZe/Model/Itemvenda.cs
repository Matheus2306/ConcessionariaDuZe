using System.ComponentModel.DataAnnotations;

namespace ConcessionariaDuZe.Model
{
    public class Itemvenda
    {
        public Guid ItemVendaId { get; set; }
        public Guid VendaId { get; set; }
        public Venda? Venda { get; set; }
        public Guid? VeiculoId { get; set; }
        public Veiculo? veiculo { get; set; }
        public double Quantidade { get; set; }
        public double ValorUnitario { get; set; }
        public double ValorTotal { get; set; }
    }
}
