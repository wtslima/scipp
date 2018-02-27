using System;
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

        public InspecoesGravadasModelServico ObterInspecoesPorCodigoInformado(string codigoOia, string cipp)
        {
            try
            {
                if (!string.IsNullOrEmpty(codigoOia) && !string.IsNullOrEmpty(cipp))
                {
                        return Conversao.ConverterParaServico(_inspecaoDominio.ObterInspecaoParaCippECodigoOiaInformado(codigoOia, cipp));
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

        public InspecoesGravadasModelServico ObterTodasInspecoes()
        {
            try
            {
                var inspecooesGravadas = _inspecaoDominio.ObterTodasInspecoes();

                var listaInspecao = Conversao.ConverterParaModelService(inspecooesGravadas);

                return listaInspecao;
            }
            catch (Exception e)
            {
               
                throw e;
            }
        }

        private InspecoesGravadasModelServico BuscarInspecoesPorCodigoOia(string codigoOia)
        {
            try
            {
                var inspecoes = _inspecaoDominio.ObterInspecaoPorCodigoOia(codigoOia);

                return Conversao.ConverterParaModelService(inspecoes);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private InspecoesGravadasModelServico BuscarInspecaoPorCipp(string cipp)
        {
            try
            {
                var inspecao = _inspecaoDominio.ObterDadosInspecaoPorCipp(cipp);

                return Conversao.ConverterParaServico(inspecao);
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }

    }
}