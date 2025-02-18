namespace ConcessionariaDuZe.Model
{
    public class servico
    {
        public Guid ServicoId { get; set; }
        public string TipoServico { get; set; }
        public double ValorServico { get; set; }
        public Guid FormaDePagamentoId { get; set; }
        public FormaDePagamento FormaDePagamento { get; set; }
    }
}
