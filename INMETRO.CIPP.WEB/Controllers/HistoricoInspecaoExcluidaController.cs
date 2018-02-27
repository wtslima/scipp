using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using INMETRO.CIPP.SERVICOS.Interfaces;
using INMETRO.CIPP.SERVICOS.ModelService;
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

                ViewData["DownloadModel"] = model;
                var retornoMensagem = new RetornoDownloadModel();
                var viewModel = new InspecaoExcluidaModel();
                var pager = new Pager(0, page);

                if (!string.IsNullOrWhiteSpace(model.CodigoOia) ||
                    !string.IsNullOrWhiteSpace(model.CodigoCipp))
                {

                    var lista = ObterInspecaoExcluidaPorCodigoInformado(model.CodigoOia,
                        model.CodigoCipp);
                    pager = new Pager(lista.Count(), page);
                    foreach (var item in lista)
                    {
                        if (!item.ExisteExcecao) continue;
                        retornoMensagem.Mensagem = item.Mensagem;
                        return PartialView("_HistoricoExclusaoErro", retornoMensagem);
                    }
                    viewModel = new InspecaoExcluidaModel
                    {
                        HistoricoInspecoesExcluidas = lista.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                        Pager = pager

                    };

                    return View(viewModel);
                }


                var resultado = ObterInspecoesExcluidas();
                pager = new Pager(resultado.HistoricoInspecoesExcluidas.Count(), page);
                if (resultado.Retorno.ExisteExcecao) return PartialView("_HistoricoExclusaoErro", resultado.Retorno);
                viewModel = new InspecaoExcluidaModel
                {
                    HistoricoInspecoesExcluidas = resultado.HistoricoInspecoesExcluidas.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                    Pager = pager
                };

                return View(viewModel);
            }
            catch (Exception e)
            {
                var exception = new ExceptionSystem();
                if (e.InnerException != null && e.InnerException.Message != null) exception.Mensagem = e.InnerException.Message;
                return PartialView("_Error", exception);
            }
        }

        private List<HistoricoDeExclusaoModel> ObterInspecaoExcluidaPorCodigoInformado(string codigoOia, string cipp)
        {
            try
            {
                var resultado = Conversao.Converter.ConverterParaModelo(_inspecaoExcluidaServico.ObterInspecoesExcluidasPorCodigoInformado(codigoOia, cipp).ToList());

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