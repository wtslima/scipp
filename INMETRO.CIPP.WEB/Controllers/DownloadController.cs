using System.Web.Mvc;

using INMETRO.CIPP.SERVICOS.Interfaces;
using INMETRO.CIPP.WEB.Models;


namespace INMETRO.CIPP.WEB.Controllers
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
                   
                   var resultado =  _servico.DownloadInspecaoPorUsuario(model.CodigoOia, model.CodigoCipp);
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