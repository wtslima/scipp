using System.Threading.Tasks;
using INMETRO.CIPP.SERVICOS.ModelService;

namespace INMETRO.CIPP.SERVICOS.Interfaces
{
    public interface IDownloadServico
    {
        RetornoDownloadModel DownloadInspecaoPorUsuario(string codigoOIA, string cipp, string usuario);
        Task<bool> DownloadInspecoesPorRotinaAutomatica();

        Task<bool> ExcluirInspecaoPorRotinaAutomatica();
    }
}