

using System.ComponentModel.DataAnnotations.Schema;

namespace Expoceep.Models
{
  
    public class Usuario
    {
        
        public long Id { get; set; }
        
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public NivelUsuario NivelUsuario { get; set; }
        [NotMapped]
        public bool Novo { get; set; }
        [NotMapped]
        public bool Editando { get; set; }

    }
    public enum NivelUsuario
    {
        COMUM,
        ADM,
        CONTADOR
    }
}
