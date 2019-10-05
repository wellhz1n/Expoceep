using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Expoceep.Models
{
    public class Venda
    {
        public long Id { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime DataDaVenda { get; set; }
        public string ValorTotal { get; set; }
        public long ProdutoId { get; set; }
        [NotMapped]
        public List<ProdutoPropriedades> ListProduto { get; set; }


        [ForeignKey("ProdutoId")]
        public virtual Produto Produto { get; set; }
    }
  
}
