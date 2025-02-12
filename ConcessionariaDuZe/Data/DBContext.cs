using ConcessionariaDuZe.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ConcessionariaDuZe.Data
{
    public class DBContext : IdentityDbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }
        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
    }
}
