using System;
using INMETRO.CIPP.SERVICOS.ModelService;

namespace INMETRO.CIPP.SERVICOS.Interfaces
{
    public interface IInspecaoServico
    {
        InspecoesGravadasModelServico ObterInspecoesPorCodigoInformado(string codigoOia, string cipp);

        InspecoesGravadasModelServico ObterTodasInspecoes();

        InspecoesGravadasModelServico ObterInspecoesPorPlacaLicenca(string placa);

        InspecoesGravadasModelServico ObterInspecaoPorDataInspecao(string dataInspecao);

    }
}