using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expoceep.Models
{
    public class Usuario
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public Usuario(string name)
        {
            this.Nome = name;
        }
    }
}
