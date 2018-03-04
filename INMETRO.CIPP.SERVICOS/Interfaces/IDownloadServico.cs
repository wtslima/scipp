using System.Threading.Tasks;
using INMETRO.CIPP.SERVICOS.ModelService;

namespace INMETRO.CIPP.SERVICOS.Interfaces
{
    public interface IDownloadServico
    {
        InspecoesGravadasModelServico DownloadInspecaoPorUsuario(string codigoOIA, string cipp, string usuario);
        Task<bool> DownloadInspecoesPorRotinaAutomatica();
       
    }
}