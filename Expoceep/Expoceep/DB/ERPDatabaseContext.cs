using Expoceep.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Expoceep.DB
{
    public class ERPDatabaseContext : DbContext
    {

        public ERPDatabaseContext(DbContextOptions<ERPDatabaseContext> options) : base(options)
        {
            Tabelas.Add(typeof(Usuario).Name);
            Tabelas.Add(typeof(Produto).Name);
            Tabelas.Add(typeof(ProdutoPropriedades).Name);
            GeraAdmin();

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region UsuarioKEYS
            builder.Entity<Usuario>().HasIndex(u => u.Login).IsUnique();
            builder.Entity<Usuario>().HasIndex(u => u.Cpf).IsUnique();
            #endregion
            #region ProdutoKeys
            builder.Entity<Produto>().HasIndex(u => u.Codigo).IsUnique();
            #endregion

        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<ProdutoPropriedades> ProdutosPropriedadess { get; set; }
        public List<string> Tabelas = new List<string>();

        public void GeraAdmin()
        {
            var u = Usuarios.ToListAsync();
            if (u.Result.Count < 1)
                Usuarios.Add(new Usuario { Nome = "Admin", Login = "admin", Senha = "admin" });
        }
    }

}
