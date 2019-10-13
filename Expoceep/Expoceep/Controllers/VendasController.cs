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
                var lista = _produtoDAO.SelectProdutos().Where(pr => new Select2().DoesContain(q.ToLower().ToCharArray(), string.Format("{0} - {1}", pr.Codigo, pr.Nome.ToLower())));

                foreach (var item in lista)
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
                var lista = _clienteDAO.SelectClientes().Where(pr => new Select2().DoesContain(q.ToLower().ToCharArray(), string.Format("{0} - {1}", pr.Cpf.ToLower(), pr.Nome.ToLower())));

                foreach (var item in lista)
                {
                    clie.Add(item.Id, string.Format("{0} - {1}", item.Cpf, item.Nome));

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
                    clie.Add(item.Id, string.Format("{0} - {1}", item.Cpf, item.Nome));
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

                result = string.Format(@"ERRO|{0}", e.InnerException.Message);
            }
            return result;
        }

        #endregion
        #endregion
        #region Venda
        public IActionResult Venda()
        {
            if (_login.GetUsuarioSession() != null)
                return View();
            else
                return this.RedirectToAction("Login", "Login");
        }
        public int[] GetVendasParaGrafico(DateTime datainicio, DateTime datafim)
        {
            var result = new List<int>();
            List<int> meses = new List<int>();
            var lista = _vendaDAO.GetVenda().Where(v => v.DataDaVenda.Month <= datafim.Month && v.DataDaVenda.Month >= datainicio.Month).ToList();
            for (int i = datainicio.Month; i <= datafim.Month; i++)
            {
                meses.Add(i);
            }

            foreach (var mes in meses)
            {
                result.Add(lista.Where(l => l.DataDaVenda.Month == mes).ToList().Count);
            }
            return result.ToArray();
        }
        #endregion
        #region Estoque
        public IActionResult Estoque()
        {
            if (_login.GetUsuarioSession() != null)
                return View();
            else
                return this.RedirectToAction("Login", "Login");
        }

        #region GetProdutos
        public Chart GetProdutosParaGrafico(long?[] produtoId, tamanho? t)
        {
            Chart result = new Chart();
            var listaLabels = new List<string>();
            var listaValues = new List<int>();
            var list = new List<Produto>();
            if (produtoId[0] != null)
            {

                for (int i = 0; i < produtoId.Length; i++)
                {
                    list.Add(_produtoDAO.SelectProdutos().Where(p => p.Id == produtoId[i]).First());
                    list.Where(pc => pc.Id == produtoId[i])
                        .First().Propriedades = _produtoDAO.SelectProdutosPropriedades()
                        .Where(pcp => pcp.ProdutoId == produtoId[i])
                        .ToList();
                }
            }
            else
            {
                foreach (var item in _produtoDAO.SelectProdutos())
                {
                    item.Propriedades = _produtoDAO.SelectProdutosPropriedades().Where(prod => prod.ProdutoId == item.Id).ToList();
                    list.Add(item);
                }
            }
            list.ForEach(c =>
            {
              listaLabels.Add(string.Format("{0}-{1}", c.Codigo, c.Nome));
            });

            if (t == null)
            {
                int soma = 0;
                foreach (var prod in list)
                {
                    foreach (var prodpriets in prod.Propriedades)
                        soma += prodpriets.Unidades;
                    listaValues.Add(soma);
                    soma = 0;
                }
            }
            else
            {
                list.ForEach(p =>
                {
                   listaValues.Add(p.Propriedades.Where(pp => pp.Tamanho == t).First().Unidades);
                });
            }
            result.Labels = listaLabels;
            result.Values = listaValues;
            return result;
        }

        #endregion

        #endregion
    }
}