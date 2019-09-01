using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expoceep.Bibliotecas;
using Expoceep.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Expoceep.Controllers
{
    public class InicioController : Controller
    {
        private ERPDatabaseContext context;
        private LoginSession _loginSession;
        private Sessao sessao;
        private readonly IToastNotification _toastNotification;
        public InicioController(ERPDatabaseContext contexto, LoginSession loginSession, IToastNotification toast, Sessao s)
        {
            context = contexto;
            _loginSession = loginSession;
            _toastNotification = toast;
            sessao = s;

        }

        public IActionResult Home()
        {

            if (_loginSession.GetUsuarioSession() != null)
            {
                if (!sessao.Existe("LOGADOMSG"))
                    _toastNotification.AddSuccessToastMessage("Bem Vindo " + _loginSession.GetUsuarioSession().Nome, new ToastrOptions() { Title = "Logado", TimeOut = 2000 });
                sessao.SalvarEmCache("LOGADOMSG", "true");
                return View();
            }
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

