using INMETRO.CIPP.DOMINIO.Interfaces;
using INMETRO.CIPP.DOMINIO.Interfaces.Repositorios;
using INMETRO.CIPP.SERVICOS.Interfaces;
using INMETRO.CIPP.SERVICOS.Servicos;
using INMETRO.CIPP.SHARED.Interfaces;
using System.Threading.Tasks;

namespace INMETRO.CIPP.WEB.ServicosAutomatizados
{
    public class ServicoGeradoPorRotinaAutomatica : IServicAgendado
    {
        IDownloadServico _servico;
        public ServicoGeradoPorRotinaAutomatica(IDownloadServico servico)
        {
            _servico = servico;
        }

        public Task<bool> ExlusaoPorRotinaAutomatica()
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DownloadPorRotinaAutomatica()
        {
          var result =  _servico.DownloadInspecoesPorRotinaAutomatica();

            return result;
        }
    }
}