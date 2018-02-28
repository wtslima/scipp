using System.Threading.Tasks;
using INMETRO.CIPP.SERVICOS.ModelService;

namespace INMETRO.CIPP.SERVICOS.Interfaces
{
    public interface IHistoricoInspecaoExcluidaServico
    {
        Task<bool> ExcluirInspecaoPorRotinaAutomatica();

        HistoricoDeExclusaoModelService ObterInspecoesExcluidasPorCodigoInformado(string codigoOia, string cipp);

        HistoricoDeExclusaoModelService ObterTodasInspecoesExcluidas();
    }
}