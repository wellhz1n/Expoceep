using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expoceep.Models
{
    public class Venda
    {
        public long Id { get; set; }
        public ListaVendaProduto ListaVendaProdutoID { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime DataDaVenda { get; set; }
        public string ValorTotal { get; set; }
    }
    public class ListaVendaProduto
    {
        public long Id { get; set; }
        public long VendaId { get; set; }
        public long ProdutoId { get; set; }
    }
}
