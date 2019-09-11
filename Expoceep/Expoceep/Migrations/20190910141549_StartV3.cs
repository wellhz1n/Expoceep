using Microsoft.EntityFrameworkCore.Migrations;

namespace Expoceep.Migrations
{
    public partial class StartV3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProdutosPropriedadess_Produtos_ProdutoId",
                table: "ProdutosPropriedadess");

            migrationBuilder.AlterColumn<long>(
                name: "ProdutoId",
                table: "ProdutosPropriedadess",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProdutosPropriedadess_Produtos_ProdutoId",
                table: "ProdutosPropriedadess",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProdutosPropriedadess_Produtos_ProdutoId",
                table: "ProdutosPropriedadess");

            migrationBuilder.AlterColumn<long>(
                name: "ProdutoId",
                table: "ProdutosPropriedadess",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_ProdutosPropriedadess_Produtos_ProdutoId",
                table: "ProdutosPropriedadess",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
