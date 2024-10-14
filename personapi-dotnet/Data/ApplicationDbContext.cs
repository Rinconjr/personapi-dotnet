using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models;

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

        // DbSet that represents the 'persona' table
        public virtual DbSet<Persona> Personas { get; set; }

        // DbSet that represents the 'profesion' table
        public virtual DbSet<Profesiones> Profesiones { get; set; }

        // DbSet that represents the 'estudios' table
        public virtual DbSet<Estudios> Estudios { get; set; }

        // DbSet that represents the 'telefono' table
        public virtual DbSet<Telefono> Telefonos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Server=127.0.0.1,1433;Database=arq_per_db;User Id=sa;Password=YourSecurePassword123!;TrustServerCertificate=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Configurations for 'Estudios'
            modelBuilder.Entity<Estudios>(entity =>
            {
                entity.HasKey(e => new { e.IdProf, e.CcPer }).HasName("PK__estudios__FB3F71A6A5F98B81");

                entity.ToTable("estudios");

                entity.Property(e => e.IdProf).HasColumnName("idProf");
                entity.Property(e => e.CcPer).HasColumnName("ccPer");
                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha");
                entity.Property(e => e.Univer)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("univer");

                entity.HasOne(d => d.Persona)
                    .WithMany(p => p.Estudios)
                    .HasForeignKey(d => d.CcPer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__estudios__cc_per__29572725");

                entity.HasOne(d => d.Profesion)
                    .WithMany(p => p.Estudios)
                    .HasForeignKey(d => d.IdProf)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__estudios__id_pro__2A4B4B5E");
            });

            // Configurations for 'Persona'
            modelBuilder.Entity<Persona>(entity =>
            {
                entity.HasKey(e => e.Cc).HasName("PK__persona__3213666D7E28CEAB");

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

            // Configurations for 'Profesiones'
            modelBuilder.Entity<Profesiones>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__profesio__3213E83F7B9BC971");

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

            // Configurations for 'Telefono'
            modelBuilder.Entity<Telefono>(entity =>
            {
                entity.HasKey(e => e.Num).HasName("PK__telefono__DF908D65463F6D4B");

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

                entity.HasOne(d => d.Persona)
                    .WithMany(p => p.Telefonos)
                    .HasForeignKey(d => d.Duenio)
                    .HasConstraintName("FK__telefono__duenio__2D27B809");
            });


            modelBuilder.Entity<Persona>().HasData(
        new Persona { Cc = 1, Nombre = "Juan", Apellido = "Pérez", Genero = 'M', Edad = 30 },
        new Persona { Cc = 2, Nombre = "María", Apellido = "García", Genero = 'F', Edad = 25 }
    );

            modelBuilder.Entity<Profesiones>().HasData(
                new Profesiones { Id = 1, Nom = "Ingeniero", Des = "Profesional de la ingeniería" },
                new Profesiones { Id = 2, Nom = "Doctor", Des = "Profesional de la medicina" }
            );


            base.OnModelCreating(modelBuilder);
        }
    }
}
