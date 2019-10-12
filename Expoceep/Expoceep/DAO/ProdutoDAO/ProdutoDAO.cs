using Expoceep.DB;
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
                c = new Random().Next(maxValue: 180).ToString();
            }
            List<string> codigo = GerarCodigo(c);
            var code = $"{codigo[0]}{codigo[1]}{codigo[2]}{codigo[3]}";
            if (code.Length > 4)
            {
                produto.Codigo = code.Substring(0, 4);
            }
            else
                produto.Codigo = code;
            if (SelectProdutos().Where(p => p.Codigo == produto.Codigo).FirstOrDefault() != null)
            {
                codigo.RemoveRange(0, codigo.Count());
                codigo = GerarCodigo((int.Parse(c) * 180).ToString());
                code = $"{codigo[0]}{codigo[1]}{codigo[2]}{codigo[3]}";
                if (code.Length > 4)
                {
                    produto.Codigo = code.Substring(0, 4);
                }
                else
                    produto.Codigo = code;
            }


            conn.Produtos.Add(produto);
            SalvaEstoque(produto);
        }

        private void SalvaEstoque(Produto produto)
        {
            conn.SaveChanges();
            var listapropestoque = new List<ProdutoPropriedadesEstoque>();
            foreach (var item in conn.Produtos.Where(p => p.Id == produto.Id).First().Propriedades)
            {
                listapropestoque.Add(new ProdutoPropriedadesEstoque
                {
                    ProdutoPropriedadesId = item.Id,
                    ProdutoPropriedades = item,
                    Unidades = item.Unidades,
                    DatadeModificacao = item.DatadeModificacao
                });
            }
            conn.ProdutoPropriedadesEstoques.AddRange(listapropestoque);
            conn.SaveChanges();
        }

        public void ApagarProduto(List<Produto> produto)
        {
            conn.Produtos.RemoveRange(produto);
            conn.SaveChanges();

        }

        public void AtualizarProduto(Produto produto)
        {
            var produtodb = conn.Produtos.SingleOrDefault(p => p.Id == produto.Id);
            var prodprop = conn.ProdutosPropriedadess.Where(ps => ps.ProdutoId == produto.Id).ToList();
            if (prodprop.Count < 1)
                prodprop = produto.Propriedades.ToList();
         
                foreach (var item in produto.Propriedades)
                {
                    var prodp = conn.ProdutosPropriedadess.SingleOrDefault(p => p.Id == item.Id);
                    prodp.Preco = item.Preco;
                    prodp.Unidades = item.Unidades;
                    conn.SaveChanges();
                }
            

            conn.Entry(produtodb).CurrentValues.SetValues(produto);
            conn.SaveChanges();
            SalvaEstoque(produto);
        }

        public IEnumerable<Produto> SelectProdutos()
        {
            var prod = conn.Produtos.ToList();
            prod.ForEach(p => p.Propriedades = conn.ProdutosPropriedadess.ToList().Where(pp => pp.ProdutoId == p.Id).ToList());
            return prod;
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
