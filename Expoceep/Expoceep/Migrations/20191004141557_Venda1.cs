using Microsoft.EntityFrameworkCore.Migrations;

namespace Expoceep.Migrations
{
    public partial class Venda1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ListaVendaProdutos_VendaId",
                table: "ListaVendaProdutos");

            migrationBuilder.CreateIndex(
                name: "IX_ListaVendaProdutos_VendaId",
                table: "ListaVendaProdutos",
                column: "VendaId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ListaVendaProdutos_VendaId",
                table: "ListaVendaProdutos");

            migrationBuilder.CreateIndex(
                name: "IX_ListaVendaProdutos_VendaId",
                table: "ListaVendaProdutos",
                column: "VendaId");
        }
    }
}
