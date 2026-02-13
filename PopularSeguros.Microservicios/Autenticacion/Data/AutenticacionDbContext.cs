using Autenticacion.Entities;
using Microsoft.EntityFrameworkCore;

namespace Autenticacion.Data
{
    public class AutenticacionDbContext : DbContext
    {
        public AutenticacionDbContext(DbContextOptions<AutenticacionDbContext> options) : base(options)
        {
        }

        public DbSet<UsuarioEntity> UsuarioTable { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UsuarioEntity>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("UsuarioTable", "AutenticacionSchema");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.NombreUsuario)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Contraseña)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Activo)
                    .HasDefaultValue(true);

                entity.Property(e => e.FechaCreacion)
                    .HasDefaultValueSql("GETUTCDATE()");

                entity.HasIndex(e => e.NombreUsuario)
                    .IsUnique();

                entity.HasIndex(e => e.Email)
                    .IsUnique();
            });
        }
    }
}
