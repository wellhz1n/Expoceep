using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Expoceep.Controllers
{
    public class CadastrosController : Controller
    {
        // GET: /<controller>/
        public IActionResult MenuCadastro()
        {
            try
            {
                if (bool.Parse(HttpContext.Session.GetString("LOGIN")))
                    return this.View();
                else
                    return this.RedirectToActionPermanent("Login", "Login");
            }
            catch
            {
                return this.RedirectToActionPermanent("Login", "Login");
            }
        }
    }
}
