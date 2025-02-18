namespace ConcessionariaDuZe.Model
{
    public class FormaDePagamento
    {
        public Guid FormaDePagamentoId { get; set; }
        public string Tipo { get; set; }
        public string Validade { get; set; }
        public int Numero { get; set; }
        public string NomeTitular { get; set; }
        public string Codigo { get; set; }


    }
}
