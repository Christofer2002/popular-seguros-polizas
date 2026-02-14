using Cliente.Data;
using Cliente.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cliente.Helper
{
    public static class ClienteDbSeeder
    {
        public static async Task SeedAsync(
            ClienteDbContext context,
            ILogger logger)
        {
            if (await context.ClienteTable.AnyAsync())
            {
                logger.LogInformation("La tabla Cliente ya contiene datos. Seeding omitido.");
                return;
            }

            logger.LogInformation("Insertando clientes iniciales...");

            var clientes = new List<ClienteEntity>
            {
                new ClienteEntity { CedulaAsegurado = "001-0123456-7", Nombre = "Juan", PrimerApellido = "Pérez", SegundoApellido = "García", TipoPersona = "Física", FechaNacimiento = new DateTime(1980,5,15), EstaEliminado = false },
                new ClienteEntity { CedulaAsegurado = "001-0123457-8", Nombre = "María", PrimerApellido = "López", SegundoApellido = "Rodríguez", TipoPersona = "Física", FechaNacimiento = new DateTime(1985,8,22), EstaEliminado = false },
                new ClienteEntity { CedulaAsegurado = "001-0123458-9", Nombre = "Carlos", PrimerApellido = "Martínez", SegundoApellido = "Hernández", TipoPersona = "Física", FechaNacimiento = new DateTime(1975,12,3), EstaEliminado = false },
                new ClienteEntity { CedulaAsegurado = "001-0123459-0", Nombre = "Ana", PrimerApellido = "González", SegundoApellido = "Sánchez", TipoPersona = "Física", FechaNacimiento = new DateTime(1990,3,18), EstaEliminado = false },
                new ClienteEntity { CedulaAsegurado = "001-0123460-1", Nombre = "Roberto", PrimerApellido = "Fernández", SegundoApellido = "Díaz", TipoPersona = "Física", FechaNacimiento = new DateTime(1982,7,10), EstaEliminado = false },
                new ClienteEntity { CedulaAsegurado = "001-0123461-2", Nombre = "Carmen", PrimerApellido = "Torres", SegundoApellido = "Moreno", TipoPersona = "Física", FechaNacimiento = new DateTime(1988,1,27), EstaEliminado = false },
                new ClienteEntity { CedulaAsegurado = "001-0123462-3", Nombre = "Diego", PrimerApellido = "Ramírez", SegundoApellido = "Vega", TipoPersona = "Física", FechaNacimiento = new DateTime(1979,9,5), EstaEliminado = false },
                new ClienteEntity { CedulaAsegurado = "001-0123463-4", Nombre = "Isabel", PrimerApellido = "Jiménez", SegundoApellido = "Castro", TipoPersona = "Física", FechaNacimiento = new DateTime(1992,6,14), EstaEliminado = false },
                new ClienteEntity { CedulaAsegurado = "001-0123464-5", Nombre = "Francisco", PrimerApellido = "Ruiz", SegundoApellido = "Medina", TipoPersona = "Física", FechaNacimiento = new DateTime(1981,11,20), EstaEliminado = false },
                new ClienteEntity { CedulaAsegurado = "001-0123465-6", Nombre = "Patricia", PrimerApellido = "Ortiz", SegundoApellido = "Romero", TipoPersona = "Física", FechaNacimiento = new DateTime(1987,4,8), EstaEliminado = false },
                new ClienteEntity { CedulaAsegurado = "001-0123466-7", Nombre = "Manuel", PrimerApellido = "Herrera", SegundoApellido = "Flores", TipoPersona = "Física", FechaNacimiento = new DateTime(1983,10,25), EstaEliminado = false },
                new ClienteEntity { CedulaAsegurado = "001-0123467-8", Nombre = "Gloria", PrimerApellido = "Vargas", SegundoApellido = "Lucena", TipoPersona = "Física", FechaNacimiento = new DateTime(1986,2,12), EstaEliminado = false }
            };

            await context.ClienteTable.AddRangeAsync(clientes);
            await context.SaveChangesAsync();

            logger.LogInformation("Clientes insertados correctamente.");
        }
    }
}
