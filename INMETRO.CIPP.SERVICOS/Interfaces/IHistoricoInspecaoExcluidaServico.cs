using System.Collections.Generic;
using System.Threading.Tasks;
using INMETRO.CIPP.SERVICOS.ModelService;

namespace INMETRO.CIPP.SERVICOS.Interfaces
{
    public interface IHistoricoInspecaoExcluidaServico
    {
        Task<bool> ExcluirInspecaoPorRotinaAutomatica();

        IEnumerable<HistoricoExclusaoServiceModel> ObterInspecoesExcluidasPorCodigoInformado(string codigoOia, string cipp);

        HIstoricoDeExclusaoModelService ObterTodasInspecoesExcluidas();
    }
}