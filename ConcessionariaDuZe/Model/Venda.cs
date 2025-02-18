namespace ConcessionariaDuZe.Model
{
    public class Venda
    {
        public Guid VendaId { get; set; }
        public DateTime DataVenda { get; set; } = DateTime.UtcNow;
        public string formaDePagamento { get; set; }
        public Guid UsuarioId { get; set; }
        public Usuario? usuario { get; set; }
        public Guid StatusId { get; set; }
        public Status? Status { get; set; }
        public Guid FormaDePagamentoId { get; set; }
        public FormaDePagamento FormaDePagamento { get; set; }
    }
}
