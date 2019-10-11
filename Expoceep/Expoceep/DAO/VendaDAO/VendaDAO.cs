using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expoceep.DB;
using Expoceep.Models;

namespace Expoceep.DAO.VendaDAO
{
    public class VendaDAO : IVendaDAO
    {
        private ERPDatabaseContext conn;
        public VendaDAO(ERPDatabaseContext con)
        {
            conn = con;
        }
        public void NovaVenda(Venda venda)
        {
            
            var vendaProduto = new List<VendaProdutos>();
            foreach (var item in venda.ListProduto)
            {
                var prop = conn.ProdutosPropriedadess.Where(p => p.ProdutoId == item.ProdutoId).Where(p1=> p1.Tamanho == item.Tamanho).First();
                vendaProduto.Add(new VendaProdutos() {ProdutoId = item.ProdutoId, ProdutoPropriedadesId = prop.Id });
                var newprop = prop;
                newprop.Unidades -= item.Unidades;
                conn.Entry(prop).CurrentValues.SetValues(newprop);
            }
            if (venda.ClienteID != null)
            {
               //venda.Cliente = conn.Clientes.Where(c => c.Id == venda.ClienteID).First();
            }
            conn.Vendas.Add(venda);
            conn.SaveChanges();
            foreach (var item in vendaProduto)
            {
                item.VendaId = venda.Id;
                item.Venda = venda;
                item.Produto = conn.Produtos.Where(p => p.Id == item.ProdutoId).First();
            }
            conn.VendaProdutos.AddRange(vendaProduto);
            conn.SaveChanges();

        }
    }
}
