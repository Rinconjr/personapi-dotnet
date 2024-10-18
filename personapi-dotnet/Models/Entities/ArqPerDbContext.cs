using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.Entities;

public partial class ArqPerDbContext : DbContext
{
    public ArqPerDbContext()
    {
    }

    public ArqPerDbContext(DbContextOptions<ArqPerDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Estudio> Estudios { get; set; }
    public virtual DbSet<Persona> Personas { get; set; }
    public virtual DbSet<Profesion> Profesions { get; set; }
    public virtual DbSet<Telefono> Telefonos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder
                .UseSqlServer("Server=db,1433;Database=arq_per_db;User Id=sa;Password=YourSecurePassword123!;TrustServerCertificate=True;")
                .LogTo(Console.WriteLine, LogLevel.Information)  // Habilitar logging de EF Core
                .EnableSensitiveDataLogging();  // Mostrar datos sensibles en los logs
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Estudio>(entity =>
        {
            entity.HasKey(e => new { e.IdProf, e.CcPer }).HasName("PK__estudios__FB3F71A68D845759");

            entity.ToTable("estudios");

            entity.Property(e => e.IdProf).HasColumnName("id_prof");
            entity.Property(e => e.CcPer).HasColumnName("cc_per");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.Univer)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("univer");

            entity.HasOne(d => d.CcPerNavigation).WithMany(p => p.Estudios)
                .HasForeignKey(d => d.CcPer)
                .OnDelete(DeleteBehavior.Cascade)  // Cambiado a cascada
                .HasConstraintName("FK__estudios__cc_per__5EBF139D");

            entity.HasOne(d => d.IdProfNavigation).WithMany(p => p.Estudios)
                .HasForeignKey(d => d.IdProf)
                .OnDelete(DeleteBehavior.Cascade)  // Cambiado a cascada
                .HasConstraintName("FK__estudios__id_pro__5FB337D6");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.Cc).HasName("PK__persona__3213666D7815E751");

            entity.ToTable("persona");

            entity.Property(e => e.Cc)
                .ValueGeneratedNever()  // El cc no se genera automáticamente
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

        modelBuilder.Entity<Profesion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__profesion__3213E83F74FC0B45");

            entity.ToTable("profesion");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()  // Generación automática de ID
                .HasColumnName("id");
            entity.Property(e => e.Des)
                .HasColumnType("text")
                .HasColumnName("des");
            entity.Property(e => e.Nom)
                .HasMaxLength(90)
                .IsUnicode(false)
                .HasColumnName("nom");
        });

        modelBuilder.Entity<Telefono>(entity =>
        {
            entity.HasKey(e => e.Num).HasName("PK__telefono__DF908D6529F173DD");

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

            entity.HasOne(d => d.DuenioNavigation).WithMany(p => p.Telefonos)
                .HasForeignKey(d => d.Duenio)
                .OnDelete(DeleteBehavior.Cascade)  // Cambiado a cascada
                .HasConstraintName("FK__telefono__duenio__628FA481");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
