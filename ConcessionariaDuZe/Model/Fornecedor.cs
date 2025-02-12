namespace ConcessionariaDuZe.Model
{
    public class Fornecedor
    {
        public Guid FornecedorId { get; set; }
        public string NomeFornecedor { get; set; }
        public string CNPJ { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
    }
}
