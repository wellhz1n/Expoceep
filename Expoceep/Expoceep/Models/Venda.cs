using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expoceep.Models
{
    public class Venda
    {
        public long Id { get; set; }
        public List<Produto> ProdutoId { get; set; }
        public Cliente Cliente { get; set; }
    }
}
