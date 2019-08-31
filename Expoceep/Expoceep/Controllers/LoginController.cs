using Expoceep.DB;
using Expoceep.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Expoceep.DAO.UsuarioDAO;
using Expoceep.Bibliotecas;
using System.Collections.Generic;
using System.Linq;

namespace Expoceep.Controllers
{

    public class LoginController : Controller
    {
        private IUsuarioDAO _UsuarioDAO;
        private LoginSession _loginSession;
        private Sessao _sessao;
        public LoginController(IUsuarioDAO usuarioDAO, LoginSession loginSession,Sessao sessao)
        {
            _UsuarioDAO = usuarioDAO;
            _loginSession = loginSession;
            _sessao = sessao;
        }
        public IActionResult Login()
        {
           
                if (_loginSession.GetUsuarioSession() != null)
                    return this.RedirectToAction("Home","Inicio");
                else
                    return View();
            
           
            
        }
        [HttpPost]
        public IActionResult Login(string login, string senha)
        {
            bool logou = _UsuarioDAO.Login(login,senha);
            if (logou)
            {
                List<Usuario> usuario = (List<Usuario>)_UsuarioDAO.SelectUsuarios();
                _loginSession.Login(usuario.Where(u=> u.Login == login).First());
                _sessao.SalvarEmCache("USER", login);
                return this.RedirectToAction("Home", "Inicio");
            }
            else
                return View();
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
