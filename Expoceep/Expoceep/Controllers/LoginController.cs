using Expoceep.DB;
using Expoceep.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Expoceep.DAO.UsuarioDAO;
using Expoceep.Bibliotecas;
using System.Collections.Generic;
using System.Linq;
using NToastNotify;

namespace Expoceep.Controllers
{

    public class LoginController : Controller
    {
        private IUsuarioDAO _UsuarioDAO;
        private LoginSession _loginSession;
        private Sessao _sessao;
        private IToastNotification _toastNotification;
        public LoginController(IUsuarioDAO usuarioDAO, LoginSession loginSession, Sessao sessao, IToastNotification toast)
        {
            _UsuarioDAO = usuarioDAO;
            _loginSession = loginSession;
            _sessao = sessao;
            _toastNotification = toast;
        }
        public IActionResult Login()
        {

            if (_loginSession.GetUsuarioSession() != null)
                return this.RedirectToAction("Home", "Inicio");
            else
                return View();



        }
        [HttpPost]
        public IActionResult Login(string login, string senha)
        {
            login = login.ToLower();
            bool logou = _UsuarioDAO.Login(login, senha);
            if (logou)
            {
                List<Usuario> usuario = (List<Usuario>)_UsuarioDAO.SelectUsuarios();
                _loginSession.Login(usuario.Where(u => u.Login == login).First());
                _sessao.SalvarEmCache("USER", login);
                _sessao.SalvarEmCache("UsuarioSession", new ConversorDeObjetos().ConverterParaString(_loginSession.GetUsuarioSession()));
                return this.RedirectToAction("Home", "Inicio");
            }
            else
            {
                _toastNotification.AddErrorToastMessage("Usuario ou Senha Incorreta", new ToastrOptions { Title = "Login", TimeOut = 2500, ProgressBar = true, PreventDuplicates = true });
                return View();

            }
        }

        [HttpPost]
        public bool Logout()
        {

            _loginSession.Logout();
            return true;
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
