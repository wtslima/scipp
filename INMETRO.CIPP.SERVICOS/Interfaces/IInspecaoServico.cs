using System.Collections.Generic;
using INMETRO.CIPP.SERVICOS.ModelService;

namespace INMETRO.CIPP.SERVICOS.Interfaces
{
    public interface IInspecaoServico
    {
        IEnumerable<InspecaoModelServico> ObterInspecoes(string codigoOia, string cipp);

        IEnumerable<InspecaoModelServico> ObterTodasInspecoes();

    }
}