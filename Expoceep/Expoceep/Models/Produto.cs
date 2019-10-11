using Expoceep.Bibliotecas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Expoceep.Models
{
    public enum tamanho
    {
        P,
        M,
        G
    }
    public class Produto
    {
        public long Id { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public ICollection<ProdutoPropriedades> Propriedades { get; set; }
        //public ICollection<VendaProdutos> VendaProdutos { get; set; }
        //public  ICollection<Venda> Vendas { get; set; }
        [NotMapped]
        public bool Novo { get; set; }
        [NotMapped]
        public bool Editando { get; set; }
    }
   public class ProdutoPropriedades
    {
        public long Id { get; set; }
        public string Preco { get; set; }
        public int Unidades { get; set; }
        public tamanho Tamanho { get; set; }
        public long ProdutoId { get; set; }
        public DateTime DatadeModificacao { get; set; }
        //public ICollection<VendaProdutos> VendaProdutos { get; set; }

        [NotMapped]
        public string TamanhoString { get; set; }
    }
    public class Select2
    {
        public long id { get; set; }
        public string text { get; set; }

        public bool DoesContain(char[] identifiers, string containingString)
        {
            return !identifiers.Except(containingString.ToCharArray()).Any();
        }
        public string Select2json<T> (List<T> lista)
        {
            return "{ \"results\" : " + new ConversorDeObjetos().ConverterParaString(lista) + "}";
        }
    }
}

