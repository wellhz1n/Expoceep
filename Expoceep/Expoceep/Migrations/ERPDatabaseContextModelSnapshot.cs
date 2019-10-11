﻿// <auto-generated />
using System;
using Expoceep.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Expoceep.Migrations
{
    [DbContext(typeof(ERPDatabaseContext))]
    partial class ERPDatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Expoceep.Models.Cliente", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Cpf");

                    b.Property<string>("Email");

                    b.Property<string>("Nome");

                    b.Property<string>("Sobrenome");

                    b.HasKey("Id");

                    b.HasIndex("Cpf")
                        .IsUnique();

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("Expoceep.Models.Produto", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Codigo");

                    b.Property<string>("Nome");

                    b.HasKey("Id");

                    b.HasIndex("Codigo")
                        .IsUnique();

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("Expoceep.Models.ProdutoPropriedades", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DatadeModificacao");

                    b.Property<string>("Preco");

                    b.Property<long>("ProdutoId");

                    b.Property<int>("Tamanho");

                    b.Property<int>("Unidades");

                    b.HasKey("Id");

                    b.HasIndex("ProdutoId");

                    b.ToTable("ProdutosPropriedadess");
                });

            modelBuilder.Entity("Expoceep.Models.Usuario", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Cpf");

                    b.Property<string>("Email");

                    b.Property<string>("Login");

                    b.Property<int>("NivelUsuario");

                    b.Property<string>("Nome");

                    b.Property<string>("Senha");

                    b.HasKey("Id");

                    b.HasIndex("Cpf")
                        .IsUnique();

                    b.HasIndex("Login")
                        .IsUnique();

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Expoceep.Models.Venda", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("ClienteID");

                    b.Property<DateTime>("DataDaVenda");

                    b.Property<long>("UsuarioId");

                    b.Property<string>("ValorTotal");

                    b.HasKey("Id");

                    b.HasIndex("ClienteID");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Vendas");
                });

            modelBuilder.Entity("Expoceep.Models.VendaProdutos", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("ProdutoId");

                    b.Property<long>("ProdutoPropriedadesId");

                    b.Property<long>("VendaId");

                    b.HasKey("Id");

                    b.HasIndex("ProdutoId");

                    b.HasIndex("ProdutoPropriedadesId");

                    b.HasIndex("VendaId");

                    b.ToTable("VendaProdutos");
                });

            modelBuilder.Entity("Expoceep.Models.ProdutoPropriedades", b =>
                {
                    b.HasOne("Expoceep.Models.Produto")
                        .WithMany("Propriedades")
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Expoceep.Models.Venda", b =>
                {
                    b.HasOne("Expoceep.Models.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteID");

                    b.HasOne("Expoceep.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Expoceep.Models.VendaProdutos", b =>
                {
                    b.HasOne("Expoceep.Models.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Expoceep.Models.ProdutoPropriedades", "ProdutoPropriedades")
                        .WithMany()
                        .HasForeignKey("ProdutoPropriedadesId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Expoceep.Models.Venda", "Venda")
                        .WithMany()
                        .HasForeignKey("VendaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
