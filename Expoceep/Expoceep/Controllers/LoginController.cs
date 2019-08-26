using Expoceep.DB;
using Expoceep.Models;
using Expoceep.Regras;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
namespace Expoceep.Controllers
{

    public class LoginController : Controller
    {
        private ERPDatabaseContext context;
        public LoginController(ERPDatabaseContext contexto)
        {
            context = contexto;
        }
        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Login(string login, string senha)
        {
            bool logou = new UsuarioBO(context).LoginSucesso(login, senha);
            if (logou)
            {
                return this.RedirectToActionPermanent("Home","Inicio");
            }
            else
                return RedirectToAction(nameof(Login));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
