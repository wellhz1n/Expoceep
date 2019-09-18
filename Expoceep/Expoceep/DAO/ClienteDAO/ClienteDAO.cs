using Expoceep.DB;
using Expoceep.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expoceep.DAO.ClienteDAO
{
    public class ClienteDAO : IClienteDAO
    {
        private ERPDatabaseContext _conn;
        public ClienteDAO(ERPDatabaseContext conn)
        {
            _conn = conn;
        }
        public void AdicionarCliente(Cliente c)
        {
          var cpfemuso =_conn.Clientes.ToList().Where(cli => cli.Cpf == c.Cpf).FirstOrDefault() != null ? true :false ;
            if (cpfemuso)
                throw new Exception("Cpf Em Uso");
            else
                _conn.Clientes.Add(c);
            _conn.SaveChanges();


        }

        public void ApagarCliente(long id)
        {
            _conn.Clientes.Remove(SelectClientes().Where(c => c.Id == id).First());
        }

        public void AtualizaCliente(Cliente c)
        {
            var cliente = SelectClientes().Where(cli=> cli.Id == c.Id).First();
            _conn.Entry(cliente).OriginalValues.SetValues(c);
            _conn.SaveChanges();
        }

        public IEnumerable<Cliente> SelectClientes()
        {
            return _conn.Clientes.ToList();
        }
    }
}
