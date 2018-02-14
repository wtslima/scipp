using System;
using System.Web.Mvc;

using INMETRO.CIPP.SERVICOS.Interfaces;
using INMETRO.CIPP.WEB.Models;


namespace INMETRO.CIPP.WEB.Controllers
{

    public class DownloadController : Controller
    {
        private readonly IDownloadServico _servico;

        public DownloadController(IDownloadServico servico)
        {
            _servico = servico;
        }

        // GET: Download
        public ActionResult Download()
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("Login", "Login");
            DownloadModel model = new DownloadModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Download(DownloadModel model)
        {
            try
            {
                if (!ModelState.IsValid) return View(model);

                model.IsSuccess = _servico.DownloadInspecaoPorUsuario(model.CodigoOia, model.CodigoCipp);
                if (!model.IsSuccess)
                {
                    return View(model);
                }
                return View(model);


            }
            catch (Exception )
            {
                return PartialView("_Error");
            }
        }

    }
}