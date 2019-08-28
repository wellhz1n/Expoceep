using Expoceep.DAO.UsuarioDAO;
using Expoceep.DB;
using Expoceep.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expoceep
{
    public class UsuarioDAO : IUsuarioDAO
    {
        private ERPDatabaseContext conn;
        public UsuarioDAO(ERPDatabaseContext connection)
        {
            conn = connection;
            if (SelectUsuarios().Count() < 1)
            {
                Usuario u = new Usuario() { Login = "admin", Senha = "admin", Email = "admin@gmail.com" };
                conn.Usuarios.Add(u);
            }
        }

        public void AdicionarUsuario(Usuario u)
        {
            conn.Usuarios.Add(u);
            conn.SaveChanges();
        }
        public bool Login(string login, string senha)
        {
            var usuarioslog = conn.Usuarios.Where(u => u.Login == login && u.Senha == senha).FirstOrDefault();
            return usuarioslog != null ? true : false;
        }
        public IEnumerable<Usuario> SelectUsuarios()
        {
                return  conn.Usuarios.ToList();
        }
    }
}
