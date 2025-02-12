namespace ConcessionariaDuZe.Model
{
    public class itemCompra
    {
        public Guid ItemCompraId { get; set; }
        public Guid CompraId { get; set; }
        public Compra? Compra { get; set; }
        public Guid VeiculoId { get; set; }
        public Veiculo? Veiculo { get; set; }
        public double Quantidade { get; set; }
    }
}
