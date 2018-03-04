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
            var model = new InspecoesGravadasModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Download(InspecoesGravadasModel model)
        {
            try
            {
                if (!ModelState.IsValid) return View(model);

                var usuario = HttpContext.Session["Usuario"].ToString();

                var resultado = _servico.DownloadInspecaoPorUsuario(model.DownloadModel.CodigoOia, model.DownloadModel.CodigoCipp, usuario);
                
                var resultModel = Conversao.Converter.ConverterParaModelo(resultado);
                resultModel.DownloadModel = new DownloadModel{CodigoOia = model.DownloadModel.CodigoOia};
                return View(resultModel);
               
            }
            catch (Exception e)
            {
                var exception = new ExceptionSystem();
                if (e.Message != null) exception.Mensagem = e.Message;
                return PartialView("_Error", exception);
            }
        }

    }
}