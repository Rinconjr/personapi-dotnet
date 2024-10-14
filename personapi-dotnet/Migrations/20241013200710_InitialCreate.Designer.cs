﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using personapi_dotnet.Data;

#nullable disable

namespace personapi_dotnet.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241013200710_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("personapi_dotnet.Models.Estudios", b =>
                {
                    b.Property<int>("IdProf")
                        .HasColumnType("int")
                        .HasColumnName("idProf");

                    b.Property<int>("CcPer")
                        .HasColumnType("int")
                        .HasColumnName("ccPer");

                    b.Property<DateTime?>("Fecha")
                        .HasColumnType("date")
                        .HasColumnName("fecha");

                    b.Property<string>("Univer")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("univer");

                    b.HasKey("IdProf", "CcPer")
                        .HasName("PK__estudios__FB3F71A6A5F98B81");

                    b.HasIndex("CcPer");

                    b.ToTable("estudios", (string)null);
                });

            modelBuilder.Entity("personapi_dotnet.Models.Persona", b =>
                {
                    b.Property<int>("Cc")
                        .HasColumnType("int")
                        .HasColumnName("cc");

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasMaxLength(45)
                        .IsUnicode(false)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("apellido");

                    b.Property<int?>("Edad")
                        .HasColumnType("int")
                        .HasColumnName("edad");

                    b.Property<string>("Genero")
                        .IsRequired()
                        .HasMaxLength(1)
                        .IsUnicode(false)
                        .HasColumnType("char(1)")
                        .HasColumnName("genero")
                        .IsFixedLength();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(45)
                        .IsUnicode(false)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("nombre");

                    b.HasKey("Cc")
                        .HasName("PK__persona__3213666D7E28CEAB");

                    b.ToTable("persona", (string)null);
                });

            modelBuilder.Entity("personapi_dotnet.Models.Profesiones", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Des")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("text")
                        .HasColumnName("des");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasMaxLength(90)
                        .IsUnicode(false)
                        .HasColumnType("varchar(90)")
                        .HasColumnName("nom");

                    b.HasKey("Id")
                        .HasName("PK__profesio__3213E83F7B9BC971");

                    b.ToTable("profesion", (string)null);
                });

            modelBuilder.Entity("personapi_dotnet.Models.Telefono", b =>
                {
                    b.Property<string>("Num")
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)")
                        .HasColumnName("num");

                    b.Property<int>("Duenio")
                        .HasColumnType("int")
                        .HasColumnName("duenio");

                    b.Property<string>("Oper")
                        .IsRequired()
                        .HasMaxLength(45)
                        .IsUnicode(false)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("oper");

                    b.HasKey("Num")
                        .HasName("PK__telefono__DF908D65463F6D4B");

                    b.HasIndex("Duenio");

                    b.ToTable("telefono", (string)null);
                });

            modelBuilder.Entity("personapi_dotnet.Models.Estudios", b =>
                {
                    b.HasOne("personapi_dotnet.Models.Persona", "Persona")
                        .WithMany("Estudios")
                        .HasForeignKey("CcPer")
                        .IsRequired()
                        .HasConstraintName("FK__estudios__cc_per__29572725");

                    b.HasOne("personapi_dotnet.Models.Profesiones", "Profesion")
                        .WithMany("Estudios")
                        .HasForeignKey("IdProf")
                        .IsRequired()
                        .HasConstraintName("FK__estudios__id_pro__2A4B4B5E");

                    b.Navigation("Persona");

                    b.Navigation("Profesion");
                });

            modelBuilder.Entity("personapi_dotnet.Models.Telefono", b =>
                {
                    b.HasOne("personapi_dotnet.Models.Persona", "Persona")
                        .WithMany("Telefonos")
                        .HasForeignKey("Duenio")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__telefono__duenio__2D27B809");

                    b.Navigation("Persona");
                });

            modelBuilder.Entity("personapi_dotnet.Models.Persona", b =>
                {
                    b.Navigation("Estudios");

                    b.Navigation("Telefonos");
                });

            modelBuilder.Entity("personapi_dotnet.Models.Profesiones", b =>
                {
                    b.Navigation("Estudios");
                });
#pragma warning restore 612, 618
        }
    }
}
