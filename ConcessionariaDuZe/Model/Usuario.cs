using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ConcessionariaDuZe.Model
{
    public class Usuario
    {
        public Guid UsuarioId { get; set; }
        public string nome { get; set; }
        [Required(ErrorMessage = "O campo CPF não pode ser nulo")]
        public string CPF { get; set; }
        [Required(ErrorMessage = "telefone não pode ser nulo")]
        [MinLength(11, ErrorMessage = "telefone precisa de no minimo 11 caracteres")]
        [MaxLength(14, ErrorMessage = "telefone pode ter no maximo 14 caracteres")]
        public string Telefone { get; set; }
        public Guid? UserId { get; set; }
        public IdentityUser? User { get; set; }
        public Guid? FormaDePagamentoId { get; set; }
        public FormaDePagamento? FormaDePagamento { get; set; }
    }
}
