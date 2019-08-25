using Expoceep.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expoceep.Regras
{
    public class UsuarioBO
    {
        private ERPDatabaseContext conection;
        private UsuarioDAO dao;
        public UsuarioBO(ERPDatabaseContext conect)
        {
            conection = conect;
            dao = new UsuarioDAO(conection);
        }

        public bool LoginSucesso(string login,string senha)
        {
            return dao.Login(login, senha);
        }
    }
}
