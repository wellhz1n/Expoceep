using Expoceep.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expoceep.DAO.ClienteDAO
{
    public interface IClienteDAO
    {
        void AdicionarCliente(Cliente c);
        IEnumerable<Cliente> SelectClientes();
        void ApagarCliente(long id);
        void AtualizaCliente(Cliente c);
    }
}
