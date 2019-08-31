using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expoceep.Bibliotecas;
using Expoceep.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Expoceep.Controllers
{
    public class InicioController : Controller
    {
        private ERPDatabaseContext context;
        private LoginSession _loginSession;

        public InicioController(ERPDatabaseContext contexto, LoginSession loginSession)
        {
            context = contexto;
            _loginSession = loginSession;

        }

        public IActionResult Home()
        {

            if (_loginSession.GetUsuarioSession() != null)
                return View();
                else
                    return this.RedirectToAction("Login", "Login");
            
        
            //var vl = new VerificadorDeSecao(new HttpContextAccessor()).VerificaLogin();
            //if (vl)
            //    return View();
            //else
            //    return RedirectToActionPermanent("Login", "Login");

        }
       
    }
}

