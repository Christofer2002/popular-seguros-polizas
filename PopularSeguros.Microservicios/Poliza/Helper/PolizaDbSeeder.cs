using Poliza.Data;
using Poliza.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Poliza.Helper
{
    public static class PolizaDbSeeder
    {
        public static async Task SeedAsync(
            PolizaDbContext context,
            ILogger logger)
        {
            if (await context.PolizaTable.AnyAsync())
            {
                logger.LogInformation("La tabla Poliza ya contiene datos. Seeding omitido.");
                return;
            }

            logger.LogInformation("Insertando pólizas iniciales...");

            var ahora = DateTime.UtcNow;

            var polizas = new List<PolizaEntity>
            {
                new PolizaEntity { Id = Guid.NewGuid(), NumeroPoliza = "POL-2024-001", TipoPolizaId = 1, CedulaAsegurado = "001-0123456-7", MontoAsegurado = 500000m, FechaVencimiento = new DateTime(2026,5,15), FechaEmision = new DateTime(2024,5,15), TipoCoberturaId = 1, EstadoPolizaId = 1, Prima = 2500m, Periodo = ahora, FechaInclusion = ahora, Aseguradora = "Popular Seguros", EstaEliminado = false },

                new PolizaEntity { Id = Guid.NewGuid(), NumeroPoliza = "POL-2024-002", TipoPolizaId = 1, CedulaAsegurado = "001-0123457-8", MontoAsegurado = 750000m, FechaVencimiento = new DateTime(2026,8,22), FechaEmision = new DateTime(2024,8,22), TipoCoberturaId = 1, EstadoPolizaId = 1, Prima = 3750m, Periodo = ahora, FechaInclusion = ahora, Aseguradora = "Popular Seguros", EstaEliminado = false },

                new PolizaEntity { Id = Guid.NewGuid(), NumeroPoliza = "POL-2024-003", TipoPolizaId = 1, CedulaAsegurado = "001-0123458-9", MontoAsegurado = 600000m, FechaVencimiento = new DateTime(2026,12,3), FechaEmision = new DateTime(2024,12,3), TipoCoberturaId = 1, EstadoPolizaId = 1, Prima = 3000m, Periodo = ahora, FechaInclusion = ahora, Aseguradora = "Popular Seguros", EstaEliminado = false },

                new PolizaEntity { Id = Guid.NewGuid(), NumeroPoliza = "POL-2024-004", TipoPolizaId = 1, CedulaAsegurado = "001-0123459-0", MontoAsegurado = 450000m, FechaVencimiento = new DateTime(2026,3,18), FechaEmision = new DateTime(2024,3,18), TipoCoberturaId = 1, EstadoPolizaId = 1, Prima = 2250m, Periodo = ahora, FechaInclusion = ahora, Aseguradora = "Popular Seguros", EstaEliminado = false },

                new PolizaEntity { Id = Guid.NewGuid(), NumeroPoliza = "POL-2024-005", TipoPolizaId = 1, CedulaAsegurado = "001-0123460-1", MontoAsegurado = 800000m, FechaVencimiento = new DateTime(2026,7,10), FechaEmision = new DateTime(2024,7,10), TipoCoberturaId = 1, EstadoPolizaId = 1, Prima = 4000m, Periodo = ahora, FechaInclusion = ahora, Aseguradora = "Popular Seguros", EstaEliminado = false },

                new PolizaEntity { Id = Guid.NewGuid(), NumeroPoliza = "POL-2024-006", TipoPolizaId = 1, CedulaAsegurado = "001-0123461-2", MontoAsegurado = 550000m, FechaVencimiento = new DateTime(2026,1,27), FechaEmision = new DateTime(2024,1,27), TipoCoberturaId = 1, EstadoPolizaId = 1, Prima = 2750m, Periodo = ahora, FechaInclusion = ahora, Aseguradora = "Popular Seguros", EstaEliminado = false },

                new PolizaEntity { Id = Guid.NewGuid(), NumeroPoliza = "POL-2024-007", TipoPolizaId = 1, CedulaAsegurado = "001-0123462-3", MontoAsegurado = 900000m, FechaVencimiento = new DateTime(2026,9,5), FechaEmision = new DateTime(2024,9,5), TipoCoberturaId = 1, EstadoPolizaId = 1, Prima = 4500m, Periodo = ahora, FechaInclusion = ahora, Aseguradora = "Popular Seguros", EstaEliminado = false },

                new PolizaEntity { Id = Guid.NewGuid(), NumeroPoliza = "POL-2024-008", TipoPolizaId = 1, CedulaAsegurado = "001-0123463-4", MontoAsegurado = 650000m, FechaVencimiento = new DateTime(2026,6,14), FechaEmision = new DateTime(2024,6,14), TipoCoberturaId = 1, EstadoPolizaId = 1, Prima = 3250m, Periodo = ahora, FechaInclusion = ahora, Aseguradora = "Popular Seguros", EstaEliminado = false },

                new PolizaEntity { Id = Guid.NewGuid(), NumeroPoliza = "POL-2024-009", TipoPolizaId = 1, CedulaAsegurado = "001-0123464-5", MontoAsegurado = 700000m, FechaVencimiento = new DateTime(2026,11,20), FechaEmision = new DateTime(2024,11,20), TipoCoberturaId = 1, EstadoPolizaId = 1, Prima = 3500m, Periodo = ahora, FechaInclusion = ahora, Aseguradora = "Popular Seguros", EstaEliminado = false },

                new PolizaEntity { Id = Guid.NewGuid(), NumeroPoliza = "POL-2024-010", TipoPolizaId = 1, CedulaAsegurado = "001-0123465-6", MontoAsegurado = 520000m, FechaVencimiento = new DateTime(2026,4,8), FechaEmision = new DateTime(2024,4,8), TipoCoberturaId = 1, EstadoPolizaId = 1, Prima = 2600m, Periodo = ahora, FechaInclusion = ahora, Aseguradora = "Popular Seguros", EstaEliminado = false },

                new PolizaEntity { Id = Guid.NewGuid(), NumeroPoliza = "POL-2024-011", TipoPolizaId = 1, CedulaAsegurado = "001-0123466-7", MontoAsegurado = 850000m, FechaVencimiento = new DateTime(2026,10,25), FechaEmision = new DateTime(2024,10,25), TipoCoberturaId = 1, EstadoPolizaId = 1, Prima = 4250m, Periodo = ahora, FechaInclusion = ahora, Aseguradora = "Popular Seguros", EstaEliminado = false },

                new PolizaEntity { Id = Guid.NewGuid(), NumeroPoliza = "POL-2024-012", TipoPolizaId = 1, CedulaAsegurado = "001-0123467-8", MontoAsegurado = 580000m, FechaVencimiento = new DateTime(2026,2,12), FechaEmision = new DateTime(2024,2,12), TipoCoberturaId = 1, EstadoPolizaId = 1, Prima = 2900m, Periodo = ahora, FechaInclusion = ahora, Aseguradora = "Popular Seguros", EstaEliminado = false }
            };

            await context.PolizaTable.AddRangeAsync(polizas);
            await context.SaveChangesAsync();

            logger.LogInformation("Las 12 pólizas fueron insertadas correctamente.");
        }
    }
}
