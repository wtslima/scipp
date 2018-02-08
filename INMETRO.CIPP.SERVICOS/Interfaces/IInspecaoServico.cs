using System.Collections.Generic;
using INMETRO.CIPP.SERVICOS.ModelService;
using Microsoft.SqlServer.Server;

namespace INMETRO.CIPP.SERVICOS.Interfaces
{
    public interface IInspecaoServico
    {

        IEnumerable<InspecaoModelServico> ObterInspecoesPorCodigoOia(string codigoOia);
        InspecaoModelServico ObterInspecaoModelServico(string cipp);
    }
}