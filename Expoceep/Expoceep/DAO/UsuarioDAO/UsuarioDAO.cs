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

        public void ApagarUsuario(int id)
        {
            var usr = conn.Usuarios.ToList().Find(u => u.Id.ToString() == id.ToString());
            conn.Remove(usr);
            conn.SaveChanges();
        }

        public void AtualizaUsuario(Usuario u)
        {
            var user = conn.Usuarios.SingleOrDefault(o => o.Id.ToString() == u.Id.ToString());
            user.Login = u.Login;
            user.Nome = u.Nome;
            user.Email = u.Email;
            user.Cpf = u.Cpf;
            conn.SaveChanges();
        }

        public bool Login(string login, string senha)
        {
            var usuarioslog = conn.Usuarios.Where(u => u.Login == login && u.Senha == senha).FirstOrDefault();
            return usuarioslog != null ? true : false;
        }
        public IEnumerable<Usuario> SelectUsuarios()
        {
            try
            {
                return conn.Usuarios.ToList();

            }
            catch (Exception)
            {

                throw new ArgumentException("Ocorreu um Erro no Banco De Dados");
            }
        }
        public void Popular()
        {
            List<Usuario> users = new List<Usuario>();
            for (int i = 0; i < 20; i++)
            {
                users.Add(new Usuario { Nome = "Teste"+i, Login = "Teste."+i, Senha = "123", Email = "TEste"+i+"@gmail.com" });

            }
            foreach (var item in users)
            {
                conn.Usuarios.Add(item);
            }
            conn.SaveChanges();
        }
    }
}
