namespace ConcessionariaDuZe.Model
{
    public class Compra
    {
        public Guid CompraId { get; set; }
        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public Guid FornecedorId { get; set; }
        public Fornecedor Fornecedor { get; set; }
        public Guid StatusId { get; set; }
        public Status Status { get; set; }
        public Guid FormaDePagamentoId { get; set; }
        public FormaDePagamento FormaDePagamento { get; set; }
    }
}
