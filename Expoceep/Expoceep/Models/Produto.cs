using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Expoceep.Models
{
    public class Produto
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        [NotMapped]
        List<Usuario> user { get; set; }
    }
}
