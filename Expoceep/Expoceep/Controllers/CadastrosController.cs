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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Expoceep.Controllers
{
    public class CadastrosController : Controller
    {
        private LoginSession _loginSession;
        private IUsuarioDAO _usuarioDAO;
        private readonly IToastNotification _toastNotification;

        // GET: /<controller>/
        public CadastrosController(LoginSession loginSession, IUsuarioDAO usuarioDAO,IToastNotification toast)
        {
            _loginSession = loginSession;
            _usuarioDAO = usuarioDAO;
            _toastNotification = toast;
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
             _usuarioDAO.AdicionarUsuario(usuario);
           return ( _usuarioDAO.SelectUsuarios().Where(u => u.Nome == usuario.Nome).FirstOrDefault() !=null);
        }
        [HttpPost]
        public string GetUsuariosTable()
        {
            string json = "{ \"data\" : "+ new ConversorDeObjetos().ConverterParaString(_usuarioDAO.SelectUsuarios()) + "}";
            return json;
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
        #endregion
        #region CategoriaProduto

        #endregion


    }
}
