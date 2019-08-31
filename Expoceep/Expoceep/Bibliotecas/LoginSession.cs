using Expoceep.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expoceep.Bibliotecas
{
    public class LoginSession
    {
        private Sessao _sessao;
        private ConversorDeObjetos _converter = new ConversorDeObjetos();
        private string _chave = "Login.Usuario";
        public LoginSession(Sessao sessao)
        {
            _sessao = sessao;
        }
        public void Login(Usuario user)
        {
            user.Senha = "";
            _sessao.SalvarEmCache(_chave, _converter.ConverterParaString(user));
        }
        public Usuario GetUsuarioSession()
        {
            var busca = _converter.ConverterParaObject<Usuario>(_sessao.ConsultarCache(_chave));
            if (busca == new Usuario())
                 busca = new Usuario();
            return busca;
        }
        public void Logout()
        {
            _sessao.RemoverTudo();
        }
    }
}
