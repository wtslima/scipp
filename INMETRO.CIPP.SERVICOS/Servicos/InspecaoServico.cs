using System;
using System.Collections.Generic;
using INMETRO.CIPP.DOMINIO.Interfaces;
using INMETRO.CIPP.DOMINIO.Servicos;
using INMETRO.CIPP.SERVICOS.Interfaces;
using INMETRO.CIPP.SERVICOS.ModelService;

namespace INMETRO.CIPP.SERVICOS.Servicos
{
    public class InspecaoServico : IInspecaoServico
    {
        private readonly IInspecao _inspecaoDominio;
        public InspecaoServico(IInspecao inspecaoDominio)
        {
            _inspecaoDominio = inspecaoDominio;
        }

        public IEnumerable<InspecaoModelServico> ObterInspecoesPorCodigoOia(string codigoOia)
        {
           if(string.IsNullOrEmpty(codigoOia)) return new List<InspecaoModelServico>();

           var listaInspecao =  new List<InspecaoModelServico>();

            var lista = _inspecaoDominio.ObterInspecaoPorCodigoOia(codigoOia);

            foreach (var item in lista)
            {
                var inspecaoModelServico = new InspecaoModelServico();

                inspecaoModelServico.CodigoCipp = item.CodigoCIPP;
                inspecaoModelServico.CodigoOia = item.CodigoOIA;
                inspecaoModelServico.Equipamento = item.NumeroEquipamento.ToString();
                inspecaoModelServico.Placa = item.PlacaLicenca;
                inspecaoModelServico.DataInspecao = item.DataInspecao;
                inspecaoModelServico.Responsavel = item.ResponsavelTecnico;

                listaInspecao.Add(inspecaoModelServico);


            }

            return listaInspecao;
        }

        public InspecaoModelServico ObterInspecaoModelServico(string cipp)
        {
            var resultado = Conversao.ConverterParaServico(_inspecaoDominio.ObterDadosInspecao(cipp));

            return resultado;
        }

        public static implicit operator InspecaoServico(InspecaoDominioServico v)
        {
            throw new NotImplementedException();
        }
    }
}