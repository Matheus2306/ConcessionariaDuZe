using Microsoft.AspNetCore.Identity;

namespace ConcessionariaDuZe.Model
{
    public class Usuario
    {
        public Guid UsuarioId { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public Guid? UserId { get; set; }
        public IdentityUser? User { get; set; }
        public Guid FormaDePagamentoId { get; set; }
        public FormaDePagamento? FormaDePagamento { get; set; }
    }
}
