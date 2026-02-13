using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Autenticacion.Migrations
{
    /// <inheritdoc />
    public partial class AutenticacionActualizada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "AutenticacionSchema");

            migrationBuilder.CreateTable(
                name: "UsuarioTable",
                schema: "AutenticacionSchema",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NombreUsuario = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Contraseña = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    FechaUltimoLogin = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioTable", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioTable_Email",
                schema: "AutenticacionSchema",
                table: "UsuarioTable",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioTable_NombreUsuario",
                schema: "AutenticacionSchema",
                table: "UsuarioTable",
                column: "NombreUsuario",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuarioTable",
                schema: "AutenticacionSchema");
        }
    }
}
