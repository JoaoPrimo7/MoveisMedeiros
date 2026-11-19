using Microsoft.EntityFrameworkCore;
using MoveisMedeiros.Models;

namespace MoveisMedeiros.Api.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        if (!await db.Usuarios.AnyAsync())
        {
            db.Usuarios.Add(new Usuario
            {
                Nome = "Administrador",

                Email = "admin@moveismedeiros.com",

                Senha = "Admin@123"
            });

            await db.SaveChangesAsync();
        }
    }
}