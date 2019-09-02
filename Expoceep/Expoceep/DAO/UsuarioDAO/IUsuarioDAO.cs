using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expoceep.Models;
namespace Expoceep.DAO.UsuarioDAO
{
   public interface IUsuarioDAO
    {
        void AdicionarUsuario(Usuario u);
        bool Login(string login, string senha);
        IEnumerable<Usuario> SelectUsuarios();
        void ApagarUsuario(int id);
        void AtualizaUsuario(Usuario u);
        void Popular();
    }
}
