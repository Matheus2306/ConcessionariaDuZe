using Microsoft.AspNetCore.Identity;

namespace ConcessionariaDuZe.Model
{
    public class Usuario
    {
        public Guid UsuarioId { get; set; }
        public string Nome { get; set; }
        public string cpf { get; set; }
        public string idade { get; set; }
        public Guid? UserId { get; set; }
        public IdentityUser? User { get; set; }
    }
}
