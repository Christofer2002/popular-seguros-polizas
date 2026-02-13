using Cliente.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cliente.Data
{
    public class ClienteDbContext : DbContext
    {
        public ClienteDbContext(DbContextOptions<ClienteDbContext> options)
            : base(options)
        {
        }

        public DbSet<ClienteEntity> ClienteTable { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClienteEntity>()
                .HasKey(c => c.CedulaAsegurado);

            modelBuilder.Entity<ClienteEntity>()
                .Property(c => c.CedulaAsegurado)
                .IsRequired()
                .HasMaxLength(20);

            base.OnModelCreating(modelBuilder);
        }
    }
}
