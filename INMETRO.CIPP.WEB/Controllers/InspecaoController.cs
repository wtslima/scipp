using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using INMETRO.CIPP.SERVICOS.Interfaces;
using INMETRO.CIPP.WEB.Models;

namespace INMETRO.CIPP.WEB.Controllers
{
    public class InspecaoController : Controller
    {

        private readonly IDownloadServico _servico;
        // GET: Inspecao
        public ActionResult ConsultaInspecao()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ConsultaInspecao(DownloadModel model)
        {
            if (ModelState.IsValid)
            {
                
            }
            return View("Index", "Home");
        }
}
}