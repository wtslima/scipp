using System;
using System.Linq;
using System.Web.Mvc;
using INMETRO.CIPP.SERVICOS.Interfaces;
using INMETRO.CIPP.WEB.Models;

namespace INMETRO.CIPP.WEB.Controllers
{
    public class HistoricoInspecaoExcluidaController : Controller
    {
        private readonly IHistoricoInspecaoExcluidaServico _inspecaoExcluidaServico;

        public HistoricoInspecaoExcluidaController(IHistoricoInspecaoExcluidaServico inspecaoExcluidaServico)
        {
            _inspecaoExcluidaServico = inspecaoExcluidaServico;
        }

        // GET: HistoricoInspecaoExcluida
        public ActionResult ConsultaInspecoesExcluida()
        {
            var user = HttpContext.Session["Usuario"];
            if (user == null)
                return RedirectToAction("Login", "Login");
            return View();
        }

        [HttpPost]
        public ActionResult ConsultaInspecoesExcluida(DownloadModel model, int? page)
        {
            try
            {

                Pager pager;

                if (!string.IsNullOrWhiteSpace(model.CodigoOia) ||
                    !string.IsNullOrWhiteSpace(model.CodigoCipp))
                {

                    var inspecoesPorCodigo = ObterInspecaoExcluidaPorCodigoInformado(model.CodigoOia,
                        model.CodigoCipp);
                    pager = new Pager(inspecoesPorCodigo.HistoricoInspecoesExcluidas.Count(), page);

                    if (inspecoesPorCodigo.Mensagem.ExisteExcecao)
                    {
                        return View(inspecoesPorCodigo);
                    }
                    inspecoesPorCodigo.HistoricoInspecoesExcluidas.Skip((pager.CurrentPage - 1) * pager.PageSize)
                        .Take(pager.PageSize);
                    inspecoesPorCodigo.Pager = pager;

                    return View(inspecoesPorCodigo);
                }


                var resultado = ObterInspecoesExcluidas();
                pager = new Pager(resultado.HistoricoInspecoesExcluidas.Count(), page);
                if (resultado.Mensagem.ExisteExcecao) return View(resultado);

                resultado.HistoricoInspecoesExcluidas.Skip((pager.CurrentPage - 1) * pager.PageSize)
                    .Take(pager.PageSize);
                 resultado.Pager = pager;

                return View(resultado);
            }
            catch (Exception e)
            {
                var exception = new ExceptionSystem();
                if (e.InnerException != null && e.InnerException.Message != null) exception.Mensagem = e.InnerException.Message;
                return PartialView("_Error", exception);
            }
        }

        private InspecaoExcluidaModel ObterInspecaoExcluidaPorCodigoInformado(string codigoOia, string cipp)
        {
            try
            {
                var resultado = Conversao.Converter.ConverterParaModelo(_inspecaoExcluidaServico.ObterInspecoesExcluidasPorCodigoInformado(codigoOia, cipp));

                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private InspecaoExcluidaModel ObterInspecoesExcluidas()
        {
            try
            {
                var resultado = Conversao.Converter.ConverterParaModelo(_inspecaoExcluidaServico.ObterTodasInspecoesExcluidas());

                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


    }
}