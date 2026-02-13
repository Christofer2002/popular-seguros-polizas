using Microsoft.EntityFrameworkCore;
using Poliza.Entities.Catalogo;

namespace Poliza.Data
{
    public class CatalogoDbContext : DbContext
    {
        public CatalogoDbContext(DbContextOptions<CatalogoDbContext> options)
            : base(options)
        {
        }

        public DbSet<TipoPolizaEntity> TipoPolizaTable { get; set; }
        public DbSet<EstadoPolizaEntity> EstadoPolizaTable { get; set; }
        public DbSet<TipoCoberturaEntity> TipoCoberturaTable { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TipoPolizaEntity>().HasKey(t => t.Id);
            modelBuilder.Entity<EstadoPolizaEntity>().HasKey(e => e.Id);
            modelBuilder.Entity<TipoCoberturaEntity>().HasKey(c => c.Id);

            // Seed catalogs
            modelBuilder.Entity<TipoPolizaEntity>().HasData(
                new TipoPolizaEntity { Id = 1, Nombre = "Vida" },
                new TipoPolizaEntity { Id = 2, Nombre = "Auto" },
                new TipoPolizaEntity { Id = 3, Nombre = "Hogar" }
            );

            modelBuilder.Entity<EstadoPolizaEntity>().HasData(
                new EstadoPolizaEntity { Id = 1, Nombre = "Vigente" },
                new EstadoPolizaEntity { Id = 2, Nombre = "Vencida" },
                new EstadoPolizaEntity { Id = 3, Nombre = "Cancelada" }
            );

            modelBuilder.Entity<TipoCoberturaEntity>().HasData(
                new TipoCoberturaEntity { Id = 1, Nombre = "RC" },
                new TipoCoberturaEntity { Id = 2, Nombre = "Todo Riesgo" }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
