using Expoceep.DB;
using Expoceep.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expoceep
{
    public class UsuarioDAO 
    {
        private ERPDatabaseContext conn;
        public UsuarioDAO(ERPDatabaseContext connection)
        {
            conn = connection;
        }
       
        public  void AdicionarUsuario(Usuario u) {
            conn.Usuarios.Add(u);
            conn.SaveChanges();
        }
    }
}
