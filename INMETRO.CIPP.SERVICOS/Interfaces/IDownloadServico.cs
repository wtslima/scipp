using System.Threading.Tasks;

namespace INMETRO.CIPP.SERVICOS.Interfaces
{
    public interface IDownloadServico
    {
        bool DownloadInspecaoPorUsuario(string codigoOIA, string cipp);
        Task<bool> DownloadInspecoesPorRotinaAutomatica();

        Task<bool> ExcluirInspecaoPorRotinaAutomatica();
    }
}