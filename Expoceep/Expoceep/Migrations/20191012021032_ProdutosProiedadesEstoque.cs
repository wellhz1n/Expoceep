using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Expoceep.Migrations
{
    public partial class ProdutosProiedadesEstoque : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProdutoPropriedadesEstoques",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProdutoPropriedadesId = table.Column<long>(nullable: false),
                    Unidades = table.Column<int>(nullable: false),
                    DatadeModificacao = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutoPropriedadesEstoques", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProdutoPropriedadesEstoques_ProdutosPropriedadess_ProdutoPro~",
                        column: x => x.ProdutoPropriedadesId,
                        principalTable: "ProdutosPropriedadess",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoPropriedadesEstoques_ProdutoPropriedadesId",
                table: "ProdutoPropriedadesEstoques",
                column: "ProdutoPropriedadesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProdutoPropriedadesEstoques");
        }
    }
}
