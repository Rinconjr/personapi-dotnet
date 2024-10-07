using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models;

namespace personapi_dotnet.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet que representa la tabla 'persona'
        public DbSet<Persona> Personas { get; set; }

        // DbSet que representa la tabla 'profesion'
        public DbSet<Profesion> Profesiones { get; set; }

        // DbSet que representa la tabla 'estudios'
        public DbSet<Estudios> Estudios { get; set; }

        // DbSet que representa la tabla 'telefono'
        public DbSet<Telefono> Telefonos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuraciones adicionales si es necesario
            modelBuilder.Entity<Estudios>()
                .HasKey(e => new { e.IdProf, e.CcPer });

            modelBuilder.Entity<Estudios>()
                .HasOne(e => e.Persona)
                .WithMany(p => p.Estudios)
                .HasForeignKey(e => e.CcPer);

            modelBuilder.Entity<Estudios>()
                .HasOne(e => e.Profesion)
                .WithMany(p => p.Estudios)
                .HasForeignKey(e => e.IdProf);

            base.OnModelCreating(modelBuilder);
        }
    }
}
