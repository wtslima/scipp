﻿using System.Web.Mvc;
using INMETRO.CIPP.API.Models;
using INMETRO.CIPP.SERVICOS.Interfaces;

namespace INMETRO.CIPP.API.Controllers
{
    public class DownloadController : Controller
    {
        private readonly IDownloadServico _servico;

        //public DownloadServico _servico;

        public DownloadController(IDownloadServico servico)
        {
            _servico = servico;
        }

        // GET: Download
        public ActionResult Download()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Download(DownloadModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var resultado = _servico.DownloadInspecaoPorUsuario(model.CodigoOia, model.CodigoCipp, "Wellington Silva Lima");
                    return RedirectToAction("Index", "Home");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

    }
}