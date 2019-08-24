using Expoceep.DB;
using Expoceep.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Expoceep.Controllers
{

    public class HomeController : Controller
    {
        private ERPDatabaseContext context;
        public HomeController(ERPDatabaseContext contexto)
        {
            context = contexto;
        }
        public IActionResult Index()
        {
            Usuario usr = new Usuario() {Nome ="ADMIN",Cpf ="123456789",Email="teste",Login ="admin",Senha="admin" };
            new UsuarioDAO(context).AdicionarUsuario(usr);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
