using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Poliza.Migrations
{
    /// <inheritdoc />
    public partial class _120220261853_InicialPoliza : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "CatalogoSchema");

            migrationBuilder.EnsureSchema(
                name: "PolizaSchema");

            migrationBuilder.CreateTable(
                name: "EstadoPolizaTable",
                schema: "CatalogoSchema",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoPolizaTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoCoberturaTable",
                schema: "CatalogoSchema",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoCoberturaTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoPolizaTable",
                schema: "CatalogoSchema",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoPolizaTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PolizaTable",
                schema: "PolizaSchema",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    NumeroPoliza = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TipoPolizaId = table.Column<int>(type: "int", nullable: false),
                    CedulaAsegurado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MontoAsegurado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaVencimiento = table.Column<DateTime>(type: "date", nullable: false),
                    FechaEmision = table.Column<DateTime>(type: "date", nullable: false),
                    TipoCoberturaId = table.Column<int>(type: "int", nullable: false),
                    EstadoPolizaId = table.Column<int>(type: "int", nullable: false),
                    Prima = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Periodo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaInclusion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Aseguradora = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EstaEliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolizaTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PolizaTable_EstadoPolizaTable_EstadoPolizaId",
                        column: x => x.EstadoPolizaId,
                        principalSchema: "CatalogoSchema",
                        principalTable: "EstadoPolizaTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PolizaTable_TipoCoberturaTable_TipoCoberturaId",
                        column: x => x.TipoCoberturaId,
                        principalSchema: "CatalogoSchema",
                        principalTable: "TipoCoberturaTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PolizaTable_TipoPolizaTable_TipoPolizaId",
                        column: x => x.TipoPolizaId,
                        principalSchema: "CatalogoSchema",
                        principalTable: "TipoPolizaTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "CatalogoSchema",
                table: "EstadoPolizaTable",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Vigente" },
                    { 2, "Vencida" },
                    { 3, "Cancelada" }
                });

            migrationBuilder.InsertData(
                schema: "CatalogoSchema",
                table: "TipoCoberturaTable",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "RC" },
                    { 2, "Todo Riesgo" }
                });

            migrationBuilder.InsertData(
                schema: "CatalogoSchema",
                table: "TipoPolizaTable",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Vida" },
                    { 2, "Auto" },
                    { 3, "Hogar" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PolizaTable_EstadoPolizaId",
                schema: "PolizaSchema",
                table: "PolizaTable",
                column: "EstadoPolizaId");

            migrationBuilder.CreateIndex(
                name: "IX_PolizaTable_TipoCoberturaId",
                schema: "PolizaSchema",
                table: "PolizaTable",
                column: "TipoCoberturaId");

            migrationBuilder.CreateIndex(
                name: "IX_PolizaTable_TipoPolizaId",
                schema: "PolizaSchema",
                table: "PolizaTable",
                column: "TipoPolizaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PolizaTable",
                schema: "PolizaSchema");

            migrationBuilder.DropTable(
                name: "EstadoPolizaTable",
                schema: "CatalogoSchema");

            migrationBuilder.DropTable(
                name: "TipoCoberturaTable",
                schema: "CatalogoSchema");

            migrationBuilder.DropTable(
                name: "TipoPolizaTable",
                schema: "CatalogoSchema");
        }
    }
}
