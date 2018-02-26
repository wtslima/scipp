using System.Threading.Tasks;

namespace INMETRO.CIPP.WEB.ServicosAutomatizados
{
    public interface IServicAgendado
    {
        Task<bool> DownloadPorRotinaAutomatica();
    }
}