using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expoceep.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Expoceep.Controllers
{
    public class InicioController : Controller
    {
        private ERPDatabaseContext context;
        public InicioController(ERPDatabaseContext contexto)
        {
            context = contexto;
        }

        public IActionResult Home()
        {
            try
            {
                if (bool.Parse(HttpContext.Session.GetString("LOGIN")))
                    return View();
                else
                    return this.RedirectToActionPermanent("Login", "Login");
            }
            catch
            {
                return this.RedirectToActionPermanent("Login", "Login");
            }
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToActionPermanent("Login", "Login");
        }
    }
}
