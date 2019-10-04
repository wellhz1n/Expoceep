using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expoceep.DB;
using Expoceep.Models;

namespace Expoceep.DAO.VendaDAO
{
    public class VendaDAO : IVendaDAO
    {
        private ERPDatabaseContext conn;
        public VendaDAO(ERPDatabaseContext con)
        {
            conn = con;
        }
        public void NovaVenda(Venda venda)
        {
         
        }
    }
}
