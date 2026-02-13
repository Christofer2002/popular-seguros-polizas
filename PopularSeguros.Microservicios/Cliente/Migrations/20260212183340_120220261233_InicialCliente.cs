using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cliente.Migrations
{
    /// <inheritdoc />
    public partial class _120220261233_InicialCliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ClienteSchema");

            migrationBuilder.CreateTable(
                name: "ClienteTable",
                schema: "ClienteSchema",
                columns: table => new
                {
                    CedulaAsegurado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PrimerApellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SegundoApellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TipoPersona = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteTable", x => x.CedulaAsegurado);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClienteTable",
                schema: "ClienteSchema");
        }
    }
}
