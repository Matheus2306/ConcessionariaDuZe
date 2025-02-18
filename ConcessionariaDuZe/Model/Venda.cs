namespace ConcessionariaDuZe.Model
{
    public class Venda
    {
        public Guid VendaId { get; set; }
        public DateTime DataVenda { get; set; } = DateTime.UtcNow;
        public double ValorTotal { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid StatusId { get; set; }
        public Status? Status { get; set; }
        public Guid FormaDePagamentoId { get; set; }
        public FormaDePagamento FormaDePagamento { get; set; }
    }
}
