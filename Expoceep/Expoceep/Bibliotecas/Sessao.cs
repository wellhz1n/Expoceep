using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expoceep.Bibliotecas
{
    public class Sessao
    {
        private IHttpContextAccessor _contexto;
        public Sessao(IHttpContextAccessor contexto)
        {
            _contexto = contexto;
        }
        //CRUD SESSION METHODS 
        public void SalvarEmCache(string chave, string valor)
        {
            _contexto.HttpContext.Session.SetString(chave, valor);
        }
        public void AtualizarCache(string chave, string valor)
        {
            if (Existe(chave))
                _contexto.HttpContext.Session.Remove(chave);
            _contexto.HttpContext.Session.SetString(chave, valor);


        }
        public string ConsultarCache(string chave)
        {
            return _contexto.HttpContext.Session.GetString(chave);
        }
        public bool Existe(string chave)
        {
            if (_contexto.HttpContext.Session.GetString(chave) == null)
                return false;
            return true;
        }
        public void RemoverTudo()
        {
            _contexto.HttpContext.Session.Clear();
        }
        public void Remover(string chave)
        {
            _contexto.HttpContext.Session.Remove(chave);

        }
    }
}
