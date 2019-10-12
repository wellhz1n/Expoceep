using Microsoft.EntityFrameworkCore.Migrations;

namespace Expoceep.Migrations
{
    public partial class NivelUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NivelUsuario",
                table: "Usuarios",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NivelUsuario",
                table: "Usuarios");
        }
    }
}
