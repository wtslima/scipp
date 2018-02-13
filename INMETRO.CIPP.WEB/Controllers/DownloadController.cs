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
                    return Json("Sucesso");
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