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
        public string Preco { get; set; }
        public int Unidades { get; set; }
        public tamanho Tamanho { get; set; }
        [NotMapped]
        public string TamanhoString { get; set; }
    }
   
}

