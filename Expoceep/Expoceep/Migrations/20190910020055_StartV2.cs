using Microsoft.EntityFrameworkCore.Migrations;

namespace Expoceep.Migrations
{
    public partial class StartV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdProduto",
                table: "ProdutosPropriedadess");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "IdProduto",
                table: "ProdutosPropriedadess",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
