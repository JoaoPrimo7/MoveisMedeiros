using Microsoft.EntityFrameworkCore;
using MoveisMedeiros.Models;

namespace MoveisMedeiros.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Obra> Obras { get; set; }
        public DbSet<Inspecao> Inspecoes { get; set; }
    }
}