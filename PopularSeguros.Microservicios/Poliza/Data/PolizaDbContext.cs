using Microsoft.EntityFrameworkCore;
using Poliza.Entities;
using Poliza.Entities.Catalogo;

namespace Poliza.Data
{
    public class PolizaDbContext : DbContext
    {
        public PolizaDbContext(DbContextOptions<PolizaDbContext> options)
            : base(options)
        {
        }

        public DbSet<PolizaEntity> PolizaTable { get; set; }

        public DbSet<TipoPolizaEntity> TipoPolizaTable { get; set; }
        public DbSet<EstadoPolizaEntity> EstadoPolizaTable { get; set; }
        public DbSet<TipoCoberturaEntity> TipoCoberturaTable { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PolizaEntity>().HasKey(p => p.Id);

            modelBuilder.Entity<PolizaEntity>()
                .Property(p => p.Id)
                .IsRequired()
                .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<PolizaEntity>()
                .Property(p => p.NumeroPoliza)
                .IsRequired()
                .HasMaxLength(50);

            // Catalog keys
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