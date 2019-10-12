using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expoceep.Bibliotecas;
using Expoceep.DAO.ClienteDAO;
using Expoceep.DAO.ProdutoDAO;
using Expoceep.DAO.VendaDAO;
using Expoceep.Models;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace Expoceep.Controllers
{
    public class VendasController : Controller
    {
        private LoginSession _login;
        private IProdutoDAO _produtoDAO;
        private IVendaDAO _vendaDAO;
        private IToastNotification _toastNotification;
        private IClienteDAO _clienteDAO;
        public VendasController(LoginSession login, IProdutoDAO prod, IVendaDAO vend, IToastNotification toas, IClienteDAO clienteDAO)
        {
            _login = login;
            _produtoDAO = prod;
            _vendaDAO = vend;
            _toastNotification = toas;
            _clienteDAO = clienteDAO;
        }
        public IActionResult Index()
        {
            if (_login.GetUsuarioSession() != null)
                return View();
            else
                return this.RedirectToAction("Login", "Login");
        }
        #region NovaVenda
        public IActionResult NovaVenda()
        {
            if (_login.GetUsuarioSession() != null)
                return View();
            else
                return this.RedirectToAction("Login", "Login");
        }

        #region GetSeletores
        [HttpPost]
        public string GetProdutos(string q)
        {
            Dictionary<long, string> prod = new Dictionary<long, string>();
            List<object> produto = new List<object>();
            Select2 select;
            string json;
            if (q != "" && q != null)
            {
                var lista = _produtoDAO.SelectProdutos().Where(pr => new Select2().DoesContain(q.ToLower().ToCharArray(),string.Format("{0} - {1}",pr.Codigo,pr.Nome.ToLower())));

                foreach (var item in lista)
                {
                    prod.Add(item.Id, string.Format("{0} - {1}",item.Codigo,item.Nome));

                }
                foreach (var item in prod.ToList())
                {
                    select = new Select2();
                    select.id = item.Key;
                    select.text = item.Value;
                    produto.Add(select);

                }
            }
            else
            {
                foreach (var item in _produtoDAO.SelectProdutos().ToList())
                {
                    prod.Add(item.Id, string.Format("{0} - {1}", item.Codigo, item.Nome));
                }
                foreach (var item in prod.ToList())
                {
                    select = new Select2();
                    select.id = item.Key;
                    select.text = item.Value;
                    produto.Add(select);

                }
            }
            json = new Select2().Select2json<object>(produto);

            return json;


        }
        [HttpPost]
        public string GetClientes(string q)
        {
            Dictionary<long, string> clie = new Dictionary<long, string>();
            List<object> cliente = new List<object>();
            Select2 select;
            string json;
            if (q != "" && q != null)
            {
                var lista = _clienteDAO.SelectClientes().Where(pr => new Select2().DoesContain(q.ToLower().ToCharArray(), string.Format("{0} - {1}", pr.Cpf.ToLower(),pr.Nome.ToLower())));

                foreach (var item in lista)
                {
                    clie.Add(item.Id, string.Format("{0} - {1}",item.Cpf,item.Nome));

                }
                foreach (var item in clie.ToList())
                {
                    select = new Select2();
                    select.id = item.Key;
                    select.text = item.Value;
                    cliente.Add(select);

                }
            }
            else
            {
                foreach (var item in _clienteDAO.SelectClientes().ToList())
                {
                    clie.Add(item.Id, string.Format("{0} - {1}",item.Cpf,item.Nome));
                }
                foreach (var item in clie.ToList())
                {
                    select = new Select2();
                    select.id = item.Key;
                    select.text = item.Value;
                    cliente.Add(select);

                }
            }
            json = new Select2().Select2json<object>(cliente);

            return json;


        }

        [HttpPost]
        public object GetProdutoCompleto(long idproduto)
        {
            return _produtoDAO.SelectProdutos().ToList().Where(p => p.Id == idproduto).FirstOrDefault();
        }
        #endregion
        #region CRUD

        [HttpPost]
        public string SalvarVenda(Venda venda)
        {
            string result = "OK";
            //venda.Usuario = _login.GetUsuarioSession();
            venda.UsuarioId = _login.GetUsuarioSession().Id;
            try
            {
                _vendaDAO.NovaVenda(venda);

            }
            catch (Exception e)
            {

                result = string.Format(@"ERRO|{0}",e.InnerException.Message);
            }
            return result;
        }

        #endregion
        #endregion
    }
}