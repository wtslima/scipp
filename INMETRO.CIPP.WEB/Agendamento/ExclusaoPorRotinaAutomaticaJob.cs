using INMETRO.CIPP.DOMINIO.Servicos;
using INMETRO.CIPP.INFRA.REPOSITORIO.Repositorios;
using INMETRO.CIPP.SERVICOS.Servicos;
using INMETRO.CIPP.SHARED.Servicos;
using Quartz;

namespace INMETRO.CIPP.WEB.Agendamento
{
    public class ExclusaoPorRotinaAutomaticaJob : IJob
    {
       
        private readonly OrganismoRepositorio _repositorio;
        private readonly GerenciarFtp _ftp;
        public HistoricoExclusaoServico HistoricoInspecao { get; }


        public ExclusaoPorRotinaAutomaticaJob()
        {
            HistoricoInspecao = new HistoricoExclusaoServico(null, _repositorio);
            _repositorio = new OrganismoRepositorio();
            _ftp = new GerenciarFtp();
            
        }

        public void Execute(IJobExecutionContext context)
        {
            var servico = new InspecaoExcluidaService(HistoricoInspecao, _repositorio, _ftp);
            servico.ExcluirInspecaoPorRotinaAutomatica().ConfigureAwait(true);
        }
    }
}