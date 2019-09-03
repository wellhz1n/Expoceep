﻿using Expoceep.DB;
using Expoceep.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expoceep.DAO.ProdutoDAO
{
    public class ProdutoDAO : IProdutoDAO
    {
        private ERPDatabaseContext conn;
        public ProdutoDAO(ERPDatabaseContext conecxao)
        {
            conn = conecxao;
        }

        public void AdicionarProduto(Produto produto)
        {
            string c;
            try
            {
                c = SelectProdutos().ToList().Last().Id.ToString() != "" ? SelectProdutos().ToList().Last().Id.ToString() : "0";
            }
            catch
            {
                c = new Random().Next(maxValue:180).ToString();
            }
            List<string> codigo = GerarCodigo(c);
            var code = $"{codigo[0]}{codigo[1]}{codigo[2]}{codigo[3]}";
            if (code.Length > 4)
            {
                produto.Codigo = code.Substring(0, 4);
            }
            else
                produto.Codigo = code;
            conn.Produtos.Add(produto);
            conn.SaveChanges();
        }


        public void ApagarProduto(Produto produto)
        {
            conn.Remove(produto);
            conn.SaveChanges();

        }

        public void AtualizarProduto(Produto produto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Produto> SelectProdutos()
        {
            return conn.Produtos.ToList();
        }
        private static List<string> GerarCodigo(string c)
        {
            var d = new Random().Next(minValue: 0, maxValue: int.Parse(c + 1));
            List<string> codigo = new List<string>();
            for (int i = 0; i < 4; i++)
            {
                codigo.Add($"{d * i}");
            }

            return codigo;
        }

    }
}
