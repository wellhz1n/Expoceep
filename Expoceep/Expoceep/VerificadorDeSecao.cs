using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expoceep
{
    public class VerificadorDeSecao
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public VerificadorDeSecao(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public bool VerificaLogin()
        {

           
            try
            {
                if (bool.Parse(_session.GetString("LOGIN")))
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
