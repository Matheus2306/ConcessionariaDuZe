
using ConcessionariaDuZe.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ConcessionariaDuZe.Data
{
    public class ConcessionariaDuZe : IdentityDbContext
    {
        public ConcessionariaDuZe(DbContextOptions<ConcessionariaDuZe> options)
            : base(options)
        {
        }
    }
}
