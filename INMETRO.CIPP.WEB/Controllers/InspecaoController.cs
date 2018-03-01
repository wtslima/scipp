using System;
using System.Linq;
using System.Web.Mvc;
using INMETRO.CIPP.SERVICOS.Interfaces;
using INMETRO.CIPP.WEB.Models;

namespace INMETRO.CIPP.WEB.Controllers
{
    public class InspecaoController : Controller
    {
        private readonly IInspecaoServico _servico;

        public InspecaoController(IInspecaoServico servico)
        {
            _servico = servico;
        }

        // GET: Inspecao
        public ActionResult ConsultaInspecao()
        {
            var user = HttpContext.Session["Usuario"];
            if (user == null)
                return RedirectToAction("Login", "Login");
            return View();
        }

        [HttpPost]
        public ActionResult ConsultaInspecao(DownloadModel model, int? page)
        {
            try
            {
                var pager = new Pager(0, page);

                if (!string.IsNullOrWhiteSpace(model.CodigoOia) || !string.IsNullOrWhiteSpace(model.CodigoCipp))
                {

                    var inspecoesPorCodigo = ObterInspecaoPorCodigoInformado(model.CodigoOia, model.CodigoCipp);
                    pager = new Pager(inspecoesPorCodigo.Inspecoes.ToList().Count, page);
                    if (inspecoesPorCodigo.Mensagem.ExisteExcecao)
                    {
                        return View(inspecoesPorCodigo);
                    }
                    inspecoesPorCodigo.Inspecoes.ToList().Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize);
                    inspecoesPorCodigo.Pager = pager;
                    return View(inspecoesPorCodigo);
                }


                var todasInspecoes = ObterInspecoes();
                pager = new Pager(todasInspecoes.Inspecoes.Count(), page);
                todasInspecoes.Inspecoes.ToList().Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize);
                todasInspecoes.Pager = pager;

                return View(todasInspecoes);
            }
            catch (Exception e)
            {
                var exception = new ExceptionSystem();
                if (e.InnerException != null && e.InnerException.Message != null) exception.Mensagem = e.InnerException.Message;
                return PartialView("_Error", exception);
            }

        }

        private InspecoesGravadasModel ObterInspecaoPorCodigoInformado(string codigoOia, string cipp)
        {
            try
            {
                var resultado = _servico.ObterInspecoesPorCodigoInformado(codigoOia, cipp);

                return Conversao.Converter.ConverterParaModelo(resultado);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private InspecoesGravadasModel ObterInspecoes()
        {
            try
            {
                var resultado = _servico.ObterTodasInspecoes();

                return Conversao.Converter.ConverterParaModelo(resultado);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        
    }
}