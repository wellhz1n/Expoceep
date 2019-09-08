using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expoceep.Bibliotecas;
using Microsoft.AspNetCore.Mvc;

namespace Expoceep.Controllers
{
    public class ConfiguracoesController : Controller
    {
        private LoginSession _loginSession;
        private IBackupDataBase _bkp;
        public ConfiguracoesController(LoginSession loginSession, IBackupDataBase bkp)
        {
            _loginSession = loginSession;
            _bkp = bkp;
        }
        public IActionResult Index()
        {
            if (_loginSession.GetUsuarioSession() != null)
                return View();
            else
                return this.RedirectToAction("Login", "Login");
        }
        public bool GerarBackup()
        {
            bool result = _bkp.GerarBackup();
            return result;
        }
        public bool CarregarBackup()
        {
            bool result = _bkp.AplicarBackup();
            return result;
        }
    }
}