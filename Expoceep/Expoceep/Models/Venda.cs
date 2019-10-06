using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Expoceep.Models
{

    public class Venda
    {
        [Key]
        public long Id { get; set; }

        public long? ClienteID { get; set; }

        public DateTime DataDaVenda { get; set; }

        public string ValorTotal { get; set; }

        #region NOtMapped
        [NotMapped]
        public List<ProdutoPropriedades> ListProduto { get; set; }
        [NotMapped]
        public Cliente Client { get; set; }
        #endregion
        #region ForeignKey

        //[ForeignKey("ProdutoPropriedadesId")]
        //public virtual ProdutoPropriedades ProdutoPropriedades { get; set; }
        //[ForeignKey("Id")]
        //public virtual Cliente Cliente { get; set; }
        #endregion

    }
    public class VendaProdutos
    {
        [Key]
        public long Id { get; set; }
        public long VendaId { get; set; }
        public long ProdutoId { get; set; }
        public long ProdutoPropriedadesId { get; set; }
        [ForeignKey("ProdutoPropriedadesId")]
        public virtual ProdutoPropriedades ProdutoPropriedades { get; set; }
        [ForeignKey("ProdutoId")]
        public virtual Produto Produto { get; set; }
        [ForeignKey("VendaId")]
        public virtual Venda Venda { get; set; }

    }

}
