using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet que representa la tabla 'persona'
        public virtual DbSet<Persona> Personas { get; set; }

        // DbSet que representa la tabla 'profesion'
        public virtual DbSet<Profesion> Profesiones { get; set; }

        // DbSet que representa la tabla 'estudio'
        public virtual DbSet<Estudio> Estudios { get; set; }

        // DbSet que representa la tabla 'telefono'
        public virtual DbSet<Telefono> Telefonos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Server=127.0.0.1,1433;Database=arq_per_db;User Id=sa;Password=YourSecurePassword123!;TrustServerCertificate=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuraciones para 'Estudio'
            modelBuilder.Entity<Estudio>(entity =>
            {
                entity.HasKey(e => new { e.IdProf, e.CcPer }).HasName("PK_Estudios");

                entity.ToTable("estudios");

                entity.Property(e => e.IdProf).HasColumnName("id_prof");
                entity.Property(e => e.CcPer).HasColumnName("cc_per");
                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha");
                entity.Property(e => e.Univer)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("univer");

                entity.HasOne(d => d.CcPerNavigation)
                    .WithMany(p => p.Estudios)
                    .HasForeignKey(d => d.CcPer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Estudios_Persona");

                entity.HasOne(d => d.IdProfNavigation)
                    .WithMany(p => p.Estudios)
                    .HasForeignKey(d => d.IdProf)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Estudios_Profesion");
            });

            // Configuraciones para 'Persona'
            modelBuilder.Entity<Persona>(entity =>
            {
                entity.HasKey(e => e.Cc).HasName("PK_Persona");

                entity.ToTable("persona");

                entity.Property(e => e.Cc)
                    .ValueGeneratedNever()
                    .HasColumnName("cc");
                entity.Property(e => e.Apellido)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("apellido");
                entity.Property(e => e.Edad).HasColumnName("edad");
                entity.Property(e => e.Genero)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("genero");
                entity.Property(e => e.Nombre)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            // Configuraciones para 'Profesion'
            modelBuilder.Entity<Profesion>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Profesion");

                entity.ToTable("profesion");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");
                entity.Property(e => e.Des)
                    .HasColumnType("text")
                    .HasColumnName("des");
                entity.Property(e => e.Nom)
                    .HasMaxLength(90)
                    .IsUnicode(false)
                    .HasColumnName("nom");
            });

            // Configuraciones para 'Telefono'
            modelBuilder.Entity<Telefono>(entity =>
            {
                entity.HasKey(e => e.Num).HasName("PK_Telefono");

                entity.ToTable("telefono");

                entity.Property(e => e.Num)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("num");
                entity.Property(e => e.Duenio).HasColumnName("duenio");
                entity.Property(e => e.Oper)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("oper");

                entity.HasOne(d => d.DuenioNavigation)
                    .WithMany(p => p.Telefonos)
                    .HasForeignKey(d => d.Duenio)
                    .HasConstraintName("FK_Telefono_Persona");
            });

            // Datos de prueba para 'Persona' y 'Profesion'
            modelBuilder.Entity<Persona>().HasData(
                new Persona { Cc = 1, Nombre = "Juan", Apellido = "Pérez", Genero = "M", Edad = 30 },
                new Persona { Cc = 2, Nombre = "María", Apellido = "García", Genero = "F", Edad = 25 }
            );

            modelBuilder.Entity<Profesion>().HasData(
                new Profesion { Id = 1, Nom = "Ingeniero", Des = "Profesional de la ingeniería" },
                new Profesion { Id = 2, Nom = "Doctor", Des = "Profesional de la medicina" }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}