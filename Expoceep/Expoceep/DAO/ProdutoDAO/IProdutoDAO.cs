using Expoceep.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expoceep.DAO.ProdutoDAO
{
    public interface IProdutoDAO
    {
        void AdicionarProduto(Produto produto);
        void AtualizarProduto(Produto produto);
        void ApagarProduto(List<Produto> produto);
        IEnumerable<Produto> SelectProdutos();

    }
}
