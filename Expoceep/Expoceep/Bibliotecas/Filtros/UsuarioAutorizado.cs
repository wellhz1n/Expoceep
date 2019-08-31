using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Expoceep.Bibliotecas.Filtros
{
    public class UsuarioAutorizado : Attribute, IAuthorizationFilter
    {
        LoginSession _loginSession;
        public void OnAuthorization(AuthorizationFilterContext context)
        {
           //_loginSession = (LoginSession)context.HttpContext.RequestServices.GetService(typeof(LoginSession));
           // if (_loginSession.GetUsuarioSession() != null)
           //     context.Result = ;
           // else
           //     return this.RedirectToActionPermanent("Login", "Login");
        }
    }
}
