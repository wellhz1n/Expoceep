using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Expoceep.Migrations
{
    public partial class Dataproduto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DatadeModificacao",
                table: "ProdutosPropriedadess",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DatadeModificacao",
                table: "ProdutosPropriedadess");
        }
    }
}
