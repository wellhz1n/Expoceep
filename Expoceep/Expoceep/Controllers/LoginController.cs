using Expoceep.DB;
using Expoceep.Models;
using Expoceep.Regras;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Expoceep.DAO.UsuarioDAO;

namespace Expoceep.Controllers
{

    public class LoginController : Controller
    {
        private IUsuarioDAO _UsuarioDAO;
        public LoginController(IUsuarioDAO usuarioDAO)
        {
            _UsuarioDAO = usuarioDAO;
        }
        public IActionResult Login()
        {
            try
            {
                if (bool.Parse(HttpContext.Session.GetString("LOGIN")))
                    return this.RedirectToActionPermanent("Home", "Inicio");
                else
                    return View();
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public IActionResult Login(string login, string senha)
        {
            bool logou = _UsuarioDAO.Login(login,senha);
            if (logou)
            {
                HttpContext.Session.SetString("LOGIN", "true");
                HttpContext.Session.SetString("USER", login);

                return this.RedirectToActionPermanent("Home", "Inicio");
            }
            else
                return this.RedirectToActionPermanent("Login", "Login");
        }

        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
