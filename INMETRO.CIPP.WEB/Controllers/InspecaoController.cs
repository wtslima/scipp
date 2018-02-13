using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
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

            return View();
        }

        [HttpPost]
        public ActionResult ConsultaInspecao(DownloadModel model)
        {
           
            var lista = new List<InspecaoModel>();
            if (ModelState.IsValid)
            {
                var resultado = _servico.ObterInspecoes(model.CodigoOia, model.CodigoCipp);

                foreach (var item in resultado)
                {
                    var inspecao = new InspecaoModel
                    {
                        CodigoOia = item.CodigoOia,
                        CodigoCipp = item.CodigoCipp,
                        Placa = item.Placa,
                        Equipamento = item.Equipamento,
                        Responsavel = item.Responsavel,
                        DataInspecao = item.DataInspecao.ToString(CultureInfo.InvariantCulture)
                    };

                    lista.Add(inspecao);
                }

            }
            return View(lista);
        }

        //private List<InspecaoModel> ListaInspecaoModel()
        //{
        //    var lista = new List<InspecaoModel>();

        //    lista.Add(new InspecaoModel{CodigoOia = "0001",CodigoCipp = "12345", Placa="1234567", Equipamento = "124354", Responsavel = "Wellington", DataInspecao = new DateTime(2018 - 02 - 01)});
        //    lista.Add(new InspecaoModel { CodigoOia = "0001", CodigoCipp = "12345", Placa = "1234567", Equipamento = "124354", Responsavel = "Wellington", DataInspecao = new DateTime(2018 - 02 - 01) });
        //    lista.Add(new InspecaoModel { CodigoOia = "0001", CodigoCipp = "12345", Placa = "1234567", Equipamento = "124354", Responsavel = "Wellington", DataInspecao = new DateTime(2018 - 02 - 01) });
        //    lista.Add(new InspecaoModel { CodigoOia = "0001", CodigoCipp = "12345", Placa = "1234567", Equipamento = "124354", Responsavel = "Wellington", DataInspecao = new DateTime(2018 - 02 - 01) });
        //    lista.Add(new InspecaoModel { CodigoOia = "0001", CodigoCipp = "12345", Placa = "1234567", Equipamento = "124354", Responsavel = "Wellington", DataInspecao = new DateTime(2018 - 02 - 01) });
        //    lista.Add(new InspecaoModel { CodigoOia = "0001", CodigoCipp = "12345", Placa = "1234567", Equipamento = "124354", Responsavel = "Wellington", DataInspecao = new DateTime(2018 - 02 - 01) });

        //    return lista;
        //}

      
    }
}