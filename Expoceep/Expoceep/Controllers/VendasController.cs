﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expoceep.Bibliotecas;
using Expoceep.DAO.ProdutoDAO;
using Expoceep.DAO.VendaDAO;
using Expoceep.Models;
using Microsoft.AspNetCore.Mvc;

namespace Expoceep.Controllers
{
    public class VendasController : Controller
    {
        private LoginSession _login;
        private IProdutoDAO _produtoDAO;
        private IVendaDAO _vendaDAO;
        public VendasController(LoginSession login, IProdutoDAO prod, IVendaDAO vend)
        {
            _login = login;
            _produtoDAO = prod;
            _vendaDAO = vend;
        }
        public IActionResult Index()
        {
            if (_login.GetUsuarioSession() != null)
                return View();
            else
                return this.RedirectToAction("Login", "Login");
        }
        #region NovaVenda
        public IActionResult NovaVenda()
        {
            if (_login.GetUsuarioSession() != null)
                return View();
            else
                return this.RedirectToAction("Login", "Login");
        }

        #region GetSeletores
        [HttpPost]
        public string GetProdutos(string q)
        {
            Dictionary<long, string> prod = new Dictionary<long, string>();
            List<object> produto = new List<object>();
            Select2 select;
            string json;
            if (q != "" && q != null)
            {

                foreach (var item in _produtoDAO.SelectProdutos().Where(p => p.Nome.ToLower().Any(pn => pn.ToString().Contains(q.ToLower()))).ToList())
                {
                    prod.Add(item.Id, item.Nome);

                }
                foreach (var item in prod.ToList())
                {
                    select = new Select2();
                    select.id = item.Key;
                    select.text = item.Value;
                    produto.Add(select);

                }
            }
            else
            {
                foreach (var item in _produtoDAO.SelectProdutos().ToList())
                {
                    prod.Add(item.Id, item.Nome);
                }
                foreach (var item in prod.ToList())
                {
                    select = new Select2();
                    select.id = item.Key;
                    select.text = item.Value;
                    produto.Add(select);

                }
            }
            json = "{ \"results\" : " + new ConversorDeObjetos().ConverterParaString(produto) + "}";

            return json;


        }
        [HttpPost]
        public object GetProdutoCompleto(long idproduto)
        {
            return _produtoDAO.SelectProdutos().ToList().Where(p => p.Id == idproduto).FirstOrDefault();
        }
        #endregion
        #region CRUD

        [HttpPost]
        public void SalvarVenda(Venda venda)
        {
            
        }

        #endregion
        #endregion
    }
}