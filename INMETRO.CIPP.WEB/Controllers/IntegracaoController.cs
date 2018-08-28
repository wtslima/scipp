using INMETRO.CIPP.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace INMETRO.CIPP.WEB.Controllers
{
    public class IntegracaoController : Controller
    {
        // GET: Integracao
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Adicionar()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Adicionar(IntegracaoInfoModel model)
        {
            return View(model);
        }

        public ActionResult Editar()
        {
            return View();
        }
        [HttpPut]
        public ActionResult Editar(IntegracaoInfoModel model)
        {
            return View(model);
        }

        public ActionResult Excluir()
        {
            return View();
        }
        [HttpPut]
        public ActionResult Excluir(int  id)
        {
            return View();
        }

        public ActionResult Detalhar(int id)
        {
            return View();
        }




    }
}