﻿using System;
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
        public ActionResult ConsultaInspecao(InspecoesGravadasModel model)
        {
            try
            {
                
                var retorno = RetornarInspecoes(model.DownloadModel);
                
                var pager = new Pager(retorno.Inspecoes.Count(), model.Page);


                var viewModel = new InspecoesGravadasModel()
                {
                    Inspecoes = retorno.Inspecoes.Skip(((pager.CurrentPage - 1) * pager.PageSize)).Take(pager.PageSize),
                    Pager = pager,
                    Mensagem = new MensagemModel
                    {
                        ExisteExcecao = retorno.Mensagem.ExisteExcecao,
                        Mensagem = retorno.Mensagem.Mensagem
                    },
                    DownloadModel = new DownloadModel
                    {
                        CodigoOia = model.DownloadModel.CodigoOia,
                        CodigoCipp = model.DownloadModel.CodigoCipp,
                        DataInspecao = model.DownloadModel.DataInspecao,
                        PlacaLicenca = model.DownloadModel.PlacaLicenca
                    },
                    Page = model.Page
                };
                
                    return View(viewModel);
               

            }
            catch (Exception e)
            {
                ModelState.Clear();
                var exception = new ExceptionSystem();
                if (e.Message != null) exception.Mensagem = e.Message;
                return PartialView("_Error", exception);
            }

        }
       

        private InspecoesGravadasModel RetornarInspecoes(DownloadModel model)
        {
            if (model != null)
            {
                if (!string.IsNullOrEmpty(model.DataInspecao))
                    return ObterInspecoesPorData(model.DataInspecao);
                if (!string.IsNullOrEmpty(model.PlacaLicenca))
                    return ObterInspecoesPorPlaca(model.PlacaLicenca);
                if (!string.IsNullOrEmpty(model.CodigoOia) || !string.IsNullOrEmpty(model.CodigoCipp))
                    return ObterInspecaoPorCodigoInformado(model.CodigoOia, model.CodigoCipp);
            }
          

            return ObterInspecoes();

        }

        private InspecoesGravadasModel ObterInspecoesPorData(string data)
        {
            try
            {
                var resultado = _servico.ObterInspecaoPorDataInspecao(data);

                return Conversao.Converter.ConverterParaModelo(resultado);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private InspecoesGravadasModel ObterInspecoesPorPlaca(string placa)
        {
            try
            {
                var resultado = _servico.ObterInspecoesPorPlacaLicenca(placa);

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

    }
}