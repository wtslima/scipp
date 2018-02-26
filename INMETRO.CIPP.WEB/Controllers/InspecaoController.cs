using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using INMETRO.CIPP.SERVICOS.Interfaces;
using INMETRO.CIPP.SERVICOS.ModelService;
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
                
                ViewData["DownloadModel"] = model;
                var retornoMensagem = new RetornoDownloadModel();
                var viewModel = new RetornoListaInspecoesViewModel();
                var pager = new Pager(0,page);
               
                    if (!string.IsNullOrWhiteSpace(model.CodigoOia) ||
                        !string.IsNullOrWhiteSpace(model.CodigoCipp))
                    {

                        var lista = ObterInspecaoPorCodigoInformado(model.CodigoOia,
                            model.CodigoCipp);
                        pager = new Pager(lista.Count(), page);
                        foreach (var item in lista)
                        {
                            if (!item.ExisteExcecao) continue;
                            retornoMensagem.Mensagem = item.Mensagem;
                            return PartialView("_BuscaInspecaoError", retornoMensagem);
                        }
                        viewModel = new RetornoListaInspecoesViewModel
                        {
                            Inspecoes = lista.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                            Pager = pager,
                            InspecaoModel = model
                        };

                        return View(viewModel);
                    }
                

                var resultado = ObterInspecoes();
                pager = new Pager(resultado.Count(), page);
                viewModel = new RetornoListaInspecoesViewModel
                {
                    Inspecoes = resultado.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                    Pager = pager,
                    InspecaoModel = model
                };
                return resultado.Count<=0 ? View() : View(viewModel);
            }
            catch (Exception e)
            {
                var exception = new ExceptionSystem();
                if (e.InnerException != null && e.InnerException.Message != null) exception.Mensagem = e.InnerException.Message;
                return PartialView("_Error", exception);
            }
           
        }

        private List<InspecaoModel> ObterInspecaoPorCodigoInformado(string codigoOia, string cipp)
        {
            try
            {
                var resultado = _servico.ObterInspecoesPorCodigoInformado(codigoOia, cipp).ToList();

                return Conversao.Converter.ConverterParaModelo(resultado);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private List<InspecaoModel> ObterInspecoes()
        {
            try
            {
                var resultado = _servico.ObterTodasInspecoes().ToList();

                return Conversao.Converter.ConverterParaModelo(resultado);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}