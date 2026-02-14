using Autenticacion.Data;
using Autenticacion.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Autenticacion.Helper
{
    public static class AutenticacionDbSeeder
    {
        public static async Task SeedAsync(
            AutenticacionDbContext context,
            ILogger logger)
        {
            if (await context.UsuarioTable.AnyAsync())
            {
                logger.LogInformation("La base de datos ya contiene usuarios. Seeding omitido.");
                return;
            }

            logger.LogInformation("Insertando usuario inicial de prueba...");

            var usuario = new UsuarioEntity
            {
                Id = Guid.NewGuid(),
                NombreUsuario = "allfernandez",
                Contraseña = "$2a$12$9/nMjyj9B2KQVmgWzgY4lOGXmniv/mBGRWLmdl1SJy3x0c7BUnnMC",
                Email = "allfernandez@bp.fi.cr",
                Activo = true,
                FechaCreacion = DateTime.UtcNow
            };

            await context.UsuarioTable.AddAsync(usuario);
            await context.SaveChangesAsync();

            logger.LogInformation("Usuario inicial insertado correctamente.");
        }
    }
}