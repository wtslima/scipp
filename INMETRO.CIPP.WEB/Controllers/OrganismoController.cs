﻿using INMETRO.CIPP.DOMINIO.Interfaces;
using INMETRO.CIPP.DOMINIO.Modelos;
using INMETRO.CIPP.WEB.Models;
using System.Web.Mvc;

namespace INMETRO.CIPP.WEB.Controllers
{
    public class OrganismoController : Controller
    {
        private readonly IOrganismoDominioService _servico;

        public OrganismoController(IOrganismoDominioService servico)
        {
            _servico = servico;
        }
        // GET: Organismo
        public ActionResult Index()
        {
            var user = HttpContext.Session["Usuario"];
            if (user == null)
                return RedirectToAction("Login", "Login");
            var organismos =  _servico.BuscarTodos();
            //var convertido = Conversao.Converter.ConverterParaModelo(organismos);
            return View(organismos);
        }
       
        public ActionResult Adicionar()
        {
            var user = HttpContext.Session["Usuario"];
            if (user == null)
                return RedirectToAction("Login", "Login");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Adicionar(OrganismoModel organismo)
        {
            if (!ModelState.IsValid) {
                organismo.Mensagem = new MensagemModel { ExisteExcecao = false, Mensagem = "Um erro ocorreu ao gravar organismo." };
            return View(organismo);
            }

            var o = new Organismo
            {
                Nome = organismo.Nome,
                CodigoOIA = organismo.Codigo+"-"+organismo.LI,
                EhAtivo = true

            };

            var resultado = _servico.Adicionar(o);
            //if (resultado)
            //    return RedirectToAction("Index");
            if (resultado)
            {
                organismo.Mensagem = new MensagemModel { ExisteExcecao = resultado, Mensagem = "Organismo gravado com sucesso." };
                return View(organismo);
            }

            organismo.Mensagem = new MensagemModel { ExisteExcecao = resultado, Mensagem = "Um erro ocorreu ao gravar organismo." };
            return View(organismo);

        }

        public ActionResult Editar(Organismo model)
        {
            var user = HttpContext.Session["Usuario"];
            if (user == null)
                return RedirectToAction("Login", "Login");

            var organismo = _servico.ObetrPorId(model.Id);

            var result = organismo.CodigoOIA.Substring(organismo.CodigoOIA.LastIndexOf('-') + 1);
            
            var  input = organismo.CodigoOIA.Substring(0, organismo.CodigoOIA.IndexOf("-") + 1);
            var chare = input.Replace("-","");
            var o = new OrganismoModel
            {
                Id = organismo.Id,
                Nome = organismo.Nome,
                Codigo = chare,
                LI = result, 
                Ativo = organismo.EhAtivo

            };


            return View(o);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(OrganismoModel organismo)
        {
            if (!ModelState.IsValid)
            {
                organismo.Mensagem = new MensagemModel { ExisteExcecao = false, Mensagem = "Um erro ocorreu ao editar organismo." };
                return View(organismo);
            }


            var o = new Organismo
            {
                Id = organismo.Id,
                Nome = organismo.Nome,
                CodigoOIA = organismo.Codigo + "-" + organismo.LI,
                EhAtivo = organismo.Ativo

            };

            var resultado = _servico.Atualizar(o);
            if (resultado)
            {
                organismo.Mensagem = new MensagemModel { ExisteExcecao = resultado, Mensagem = "Organismo alterado com sucesso." };
                return View(organismo);
            }

            organismo.Mensagem = new MensagemModel { ExisteExcecao = resultado, Mensagem = "Um erro ocorreu ao alterar organismo." };
            return View(organismo);
        }

        public ActionResult Excluir(Organismo model)
        {

            var user = HttpContext.Session["Usuario"];
            if (user == null)
                return RedirectToAction("Login", "Login");

            var x = _servico.ObetrPorId(model.Id);
            var o = new OrganismoModel
            {
                Id = x.Id,
                Nome = x.Nome,
                Codigo = x.CodigoOIA
            };

            return View(o);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Excluir(OrganismoModel model)
        {
            var resultado = _servico.Excluir(model.Id);

            if (resultado)
            {
                return RedirectToAction("Index");
            }

            model.Mensagem = new MensagemModel { ExisteExcecao = resultado, Mensagem = "Um erro ocorreu ao excluir organismo." };
            return View(model);
        }

        public ActionResult Detalhar()
        {
            return View();
        }
              
    }
}