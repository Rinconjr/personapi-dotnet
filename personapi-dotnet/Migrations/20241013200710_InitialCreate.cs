using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace personapi_dotnet.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "persona",
                columns: table => new
                {
                    cc = table.Column<int>(type: "int", nullable: false),
                    nombre = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: false),
                    apellido = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: false),
                    genero = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false),
                    edad = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__persona__3213666D7E28CEAB", x => x.cc);
                });

            migrationBuilder.CreateTable(
                name: "profesion",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    nom = table.Column<string>(type: "varchar(90)", unicode: false, maxLength: 90, nullable: false),
                    des = table.Column<string>(type: "text", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__profesio__3213E83F7B9BC971", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "telefono",
                columns: table => new
                {
                    num = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    oper = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: false),
                    duenio = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__telefono__DF908D65463F6D4B", x => x.num);
                    table.ForeignKey(
                        name: "FK__telefono__duenio__2D27B809",
                        column: x => x.duenio,
                        principalTable: "persona",
                        principalColumn: "cc",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "estudios",
                columns: table => new
                {
                    idProf = table.Column<int>(type: "int", nullable: false),
                    ccPer = table.Column<int>(type: "int", nullable: false),
                    univer = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    fecha = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__estudios__FB3F71A6A5F98B81", x => new { x.idProf, x.ccPer });
                    table.ForeignKey(
                        name: "FK__estudios__cc_per__29572725",
                        column: x => x.ccPer,
                        principalTable: "persona",
                        principalColumn: "cc");
                    table.ForeignKey(
                        name: "FK__estudios__id_pro__2A4B4B5E",
                        column: x => x.idProf,
                        principalTable: "profesion",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_estudios_ccPer",
                table: "estudios",
                column: "ccPer");

            migrationBuilder.CreateIndex(
                name: "IX_telefono_duenio",
                table: "telefono",
                column: "duenio");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "estudios");

            migrationBuilder.DropTable(
                name: "telefono");

            migrationBuilder.DropTable(
                name: "profesion");

            migrationBuilder.DropTable(
                name: "persona");
        }
    }
}
