using System.Threading.Tasks;

namespace INMETRO.CIPP.SERVICOS.Interfaces
{
    public interface IDownloadServico
    {
        bool DownloadInspecao(string codigoOIA, string cipp);
        Task<bool> DownloadInspecoesPorRotinaAutomatica();

        Task<bool> ExcluirInspecaoPorRotinaAutomatica();
    }
}