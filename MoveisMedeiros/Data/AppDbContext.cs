using Microsoft.EntityFrameworkCore;
using MoveisMedeiros.Models;


namespace MoveisMedeiros.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Obra> Obras { get; set; }
    public DbSet<Inspecao> Inspecoes { get; set; }
    public DbSet<ItemInspecao> ItensInspecao { get; set; }

}
