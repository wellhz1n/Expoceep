using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expoceep.Bibliotecas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Expoceep.DAO.UsuarioDAO;
using Expoceep.Models;
using NToastNotify;
using Expoceep.DAO.ProdutoDAO;
using Expoceep.DAO.ClienteDAO;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Expoceep.Controllers
{
    public class CadastrosController : Controller
    {
        private LoginSession _loginSession;
        private IUsuarioDAO _usuarioDAO;
        private IProdutoDAO _produtoDAO;
        private readonly IToastNotification _toastNotification;
        private IClienteDAO _clienteDAO;

        // GET: /<controller>/
        public CadastrosController(LoginSession loginSession, IUsuarioDAO usuarioDAO, IProdutoDAO produtoDAO, IToastNotification toast, IClienteDAO cliente)
        {
            _loginSession = loginSession;
            _usuarioDAO = usuarioDAO;
            _produtoDAO = produtoDAO;
            _toastNotification = toast;
            _clienteDAO = cliente;
        }
        public IActionResult Index()
        {
            if (_loginSession.GetUsuarioSession() != null)
                return View();
            else
                return this.RedirectToAction("Login", "Login");
        }

        #region Produto
        public IActionResult ProdutoCadastrar()
        {
            if (_loginSession.GetUsuarioSession() != null)
                return View();
            else
                return this.RedirectToAction("Login", "Login");
        }
        #region CRUD
        [HttpPost]
        public bool SalvarProduto(Produto prod)
        {
            if (!prod.Editando)
            {
                try
                {
                    _produtoDAO.AdicionarProduto(prod);
                    return (_produtoDAO.SelectProdutos().Where(u => u.Nome == prod.Nome).FirstOrDefault() != null);
                }
                catch (Exception e)
                {
                    _toastNotification.AddErrorToastMessage(e.Message, new ToastrOptions { Title = "Ops!", TimeOut = 2000 });
                    return false;
                }
            }
            else
            {
                _produtoDAO.AtualizarProduto(prod);
                return (_produtoDAO.SelectProdutos().Where(u => u.Nome == prod.Nome).FirstOrDefault() != null);
            }
        }
        [HttpPost]
        public string GetProdutosTable()
        {
            var a = _produtoDAO.SelectProdutos().ToList();
            foreach (var item in a)
            {

                item.Propriedades.ToList().ForEach(i => i.TamanhoString = i.Tamanho.ToString());
            }
            var txt = new ConversorDeObjetos().ConverterParaString(a);

            string json = "{ \"data\" : " + txt + "}";
            return json;
        }
        public bool DeletarProduto(List<Produto> prod)
        {
            bool resultado = true;
            try
            {
                _produtoDAO.ApagarProduto(prod);
            }
            catch (Exception e)
            {
                _toastNotification.AddErrorToastMessage(e.Message, new ToastrOptions { Title = "Ops", TimeOut = 2000 });
                return false;
            }
            return resultado;
        }
        #endregion
        #endregion
        #region Usuario
        public IActionResult UsuarioCadastrar()
        {
            if (_loginSession.GetUsuarioSession() != null)
                return View();
            else
                return this.RedirectToAction("Login", "Login");
        }
        [HttpPost]
        public bool SalvarUsuario(Usuario usuario)
        {
            try
            {

                if (!usuario.Editando)
                {
                    _usuarioDAO.AdicionarUsuario(usuario);
                    return (_usuarioDAO.SelectUsuarios().Where(u => u.Nome == usuario.Nome).FirstOrDefault() != null);
                }
                else
                {
                    _usuarioDAO.AtualizaUsuario(usuario);
                    return (_usuarioDAO.SelectUsuarios().Where(u => u.Nome == usuario.Nome).FirstOrDefault() != null);
                }
            }
            catch (Exception e)
            {
                _toastNotification.AddErrorToastMessage(e.Message, new ToastrOptions { Title = "Ops!", TimeOut = 2000 });
                return false;
            }
        }
        [HttpPost]
        public string GetUsuariosTable()
        {
            string json = "{ \"data\" : " + new ConversorDeObjetos().ConverterParaString(_usuarioDAO.SelectUsuarios()) + "}";
            return json;
        }
        public bool DeletarUsuario(Usuario usuario)
        {
            bool resultado = true;
            var usr = _loginSession.GetUsuarioSession();
            if (usr.Id.ToString() == usuario.Id.ToString())
            {
                _toastNotification.AddWarningToastMessage("Não é possivel apagar um usuario logado", new ToastrOptions { Title = "Usuario em uso!", TimeOut = 2000 });
                return false;
            }
            try
            {
                _usuarioDAO.ApagarUsuario((int)usuario.Id);
            }
            catch (Exception e)
            {
                _toastNotification.AddErrorToastMessage(e.Message, new ToastrOptions { Title = "Ops", TimeOut = 2000 });
                return false;
            }
            return resultado;
        }
        [HttpGet]
        public void Popular()
        {
            _usuarioDAO.Popular();
            RedirectToAction(nameof(UsuarioCadastrar));
        }
        #endregion
        #region Materiais
        public IActionResult MateriaisCadastrar()
        {
            if (_loginSession.GetUsuarioSession() != null)
                return View();
            else
                return this.RedirectToAction("Login", "Login");
        }
        #endregion
        #region Cliente
        public IActionResult ClientesCadastrar()
        {
            if (_loginSession.GetUsuarioSession() != null)
                return View();
            else
                return this.RedirectToAction("Login", "Login");
        }
        [HttpPost]
        public bool SalvarCliente(Cliente cliente)
        {
            try
            {

                if (!cliente.Editando)
                {
                    _clienteDAO.AdicionarCliente(cliente);
                    return (_clienteDAO.SelectClientes().Where(u => u.Cpf == cliente.Cpf).FirstOrDefault() != null);
                }
                else
                {
                    _clienteDAO.AtualizaCliente(cliente);
                    return (_clienteDAO.SelectClientes().Where(u => u.Cpf == cliente.Cpf).FirstOrDefault() != null);

                }
            }
            catch (Exception e)
            {
                _toastNotification.AddErrorToastMessage(e.Message, new ToastrOptions { Title = "Ops!", TimeOut = 2000, ProgressBar = true });
                return false;
            }
        }
        [HttpPost]
        public string GetClientesTable()
        {
            string json = "{ \"data\" : " + new ConversorDeObjetos().ConverterParaString(_clienteDAO.SelectClientes()) + "}";
            return json;
        }
        public bool DeletarCliente(Cliente cliente)
        {
            bool resultado = true;
            try
            {
                _clienteDAO.ApagarCliente(cliente.Id);
            }
            catch (Exception e)
            {
                _toastNotification.AddErrorToastMessage(e.Message, new ToastrOptions { Title = "Ops", TimeOut = 2000 });
                return false;
            }
            return resultado;
        }
        #endregion
        #region CategoriaProduto

        #endregion


    }
}
