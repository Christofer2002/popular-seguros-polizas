using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cliente.Migrations
{
    /// <inheritdoc />
    public partial class _120220261700_EliminadoVirtualCliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EstaEliminado",
                schema: "ClienteSchema",
                table: "ClienteTable",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstaEliminado",
                schema: "ClienteSchema",
                table: "ClienteTable");
        }
    }
}
