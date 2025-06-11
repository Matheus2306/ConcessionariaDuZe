using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ConcessionariaDuZe.Model
{
    public class Fornecedor
    {
        [Key]
        public Guid FornecedorId { get; set; }
        [Required(ErrorMessage = "O nome do Fornecedor não pode ser nulo")]
        [MinLength(3, ErrorMessage = "O nome do Fornecedor precisa de no minimo 3 caracteres")]
        public string NomeFornecedor { get; set; }
        public string CNPJ { get; set; }
        [Required(ErrorMessage = "telefone não pode ser nulo")]
        [MinLength(11, ErrorMessage = "telefone precisa de no minimo 11 caracteres")]
        [MaxLength(14, ErrorMessage = "telefone pode ter no maximo 14 caracteres")]
        public string Telefone { get; set; }
        [Required(ErrorMessage = "Campo email é obrigatório")]
        [EmailAddress(ErrorMessage = "informe um email valido")]
        public string Email { get; set; }
    }
}
