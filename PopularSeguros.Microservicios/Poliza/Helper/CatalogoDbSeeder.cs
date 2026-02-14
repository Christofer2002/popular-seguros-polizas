using Poliza.Data;
using Poliza.Entities.Catalogo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Poliza.Helper
{
    public static class CatalogoDbSeeder
    {
        public static async Task SeedAsync(
            CatalogoDbContext context,
            ILogger logger)
        {
            if (await context.TipoPolizaTable.AnyAsync())
            {
                logger.LogInformation("La tabla Catálogos ya contiene datos. Seeding omitido.");
                return;
            }

            logger.LogInformation("Insertando catálogos iniciales...");

            var tiposPoliza = new List<TipoPolizaEntity>
            {
                new TipoPolizaEntity { Id = 1, Nombre = "Vida" },
                new TipoPolizaEntity { Id = 2, Nombre = "Auto" },
                new TipoPolizaEntity { Id = 3, Nombre = "Hogar" }
            };

            await context.TipoPolizaTable.AddRangeAsync(tiposPoliza);

            var tiposCoberturas = new List<TipoCoberturaEntity>
            {
                new TipoCoberturaEntity { Id = 1, Nombre = "RC" },
                new TipoCoberturaEntity { Id = 2, Nombre = "Todo Riesgo" }
            };

            await context.TipoCoberturaTable.AddRangeAsync(tiposCoberturas);

            var estadosPoliza = new List<EstadoPolizaEntity>
            {
                new EstadoPolizaEntity { Id = 1, Nombre = "Vigente" },
                new EstadoPolizaEntity { Id = 2, Nombre = "Vencida" },
                new EstadoPolizaEntity { Id = 3, Nombre = "Cancelada" }
            };

            await context.EstadoPolizaTable.AddRangeAsync(estadosPoliza);

            await context.SaveChangesAsync();

            logger.LogInformation("Catálogos insertados correctamente.");
        }
    }
}
