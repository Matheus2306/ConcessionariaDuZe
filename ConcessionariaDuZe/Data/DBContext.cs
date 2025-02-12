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
        public DbSet<ConcessionariaDuZe.Model.Compra> Compra { get; set; } = default!;
        public DbSet<ConcessionariaDuZe.Model.Fornecedor> Fornecedor { get; set; } = default!;
        public DbSet<ConcessionariaDuZe.Model.itemCompra> itemCompra { get; set; } = default!;
        public DbSet<ConcessionariaDuZe.Model.Itemvenda> Itemvenda { get; set; } = default!;
        public DbSet<ConcessionariaDuZe.Model.ServicoPrestado> ServicoPrestado { get; set; } = default!;
        public DbSet<ConcessionariaDuZe.Model.servico> servico { get; set; } = default!;
        public DbSet<ConcessionariaDuZe.Model.Status> Status { get; set; } = default!;
    }
}
