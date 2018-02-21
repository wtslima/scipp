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
        [HttpGet]
        public ActionResult Download(string usuario)
        {
            var user = HttpContext.Session["Usuario"];
            if (user == null)
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

                var usuario = HttpContext.Session["Usuario"].ToString();

                var resultado = _servico.DownloadInspecaoPorUsuario(model.CodigoOia, model.CodigoCipp, usuario);
                if (resultado != null)
                {
                    model.IsSuccess = resultado.ExisteExcecao;
                    model.Mensagem = resultado.Mensagem;

                }
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