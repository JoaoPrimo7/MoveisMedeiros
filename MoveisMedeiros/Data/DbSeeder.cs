using MoveisMedeiros.Models;

namespace MoveisMedeiros.Data
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext context)
        {
            if (!context.Usuarios.Any())
            {
                context.Usuarios.Add(new Usuario
                {
                    Nome = "Administrador",
                    Email = "admin@moveismedeiros.com",
                    Senha = "123"
                });
                context.SaveChanges();
            }
        }
    }
}