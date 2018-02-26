using System;
using System.Collections.Generic;
using System.Linq;
using INMETRO.CIPP.DOMINIO.Interfaces;
using INMETRO.CIPP.SERVICOS.Interfaces;
using INMETRO.CIPP.SERVICOS.ModelService;

namespace INMETRO.CIPP.SERVICOS.Servicos
{
    public class InspecaoServico : IInspecaoServico
    {
        private readonly IInspecaoDominioService _inspecaoDominio;
        
        public InspecaoServico(IInspecaoDominioService inspecaoDominio)
        {
            _inspecaoDominio = inspecaoDominio;
        }

        public IEnumerable<InspecaoModelServico> ObterInspecoesPorCodigoInformado(string codigoOia, string cipp)
        {
            try
            {
                if (!string.IsNullOrEmpty(codigoOia) && !string.IsNullOrEmpty(cipp))
                {
                    var listaInspecao = new List<InspecaoModelServico>
                    {
                        Conversao.ConverterParaServico(_inspecaoDominio.ObterInspecaoParaCippECodigoOiaInformado(codigoOia, cipp))
                    };
                    return listaInspecao;
                }
                if (!string.IsNullOrEmpty(cipp))
                {
                    var inspecao = BuscarInspecaoPorCipp(cipp);
                    return inspecao;
                }

                return BuscarInspecoesPorCodigoOia(codigoOia);
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }

        public IEnumerable<InspecaoModelServico> ObterTodasInspecoes()
        {
            try
            {
                var lista = _inspecaoDominio.ObterTodasInspecoes().ToList();

                var listaInspecao = Conversao.ConverterListaParaModeloService(lista);

                return listaInspecao;
            }
            catch (Exception e)
            {
               
                throw e;
            }
        }

        private IEnumerable<InspecaoModelServico> BuscarInspecoesPorCodigoOia(string codigoOia)
        {
            try
            {
                var inspecoes = _inspecaoDominio.ObterInspecaoPorCodigoOia(codigoOia).ToList();

                return Conversao.ConverterListaParaModeloService(inspecoes);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private IEnumerable<InspecaoModelServico> BuscarInspecaoPorCipp(string cipp)
        {
            try
            {
                var listaInspecao = new List<InspecaoModelServico>();

                var inspecao = _inspecaoDominio.ObterDadosInspecaoPorCipp(cipp);

                listaInspecao.Add(Conversao.ConverterParaServico(inspecao));

                return listaInspecao;
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }

    }
}