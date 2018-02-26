using INMETRO.CIPP.DOMINIO.Servicos;
using INMETRO.CIPP.INFRA.REPOSITORIO.Repositorios;
using INMETRO.CIPP.SERVICOS.Servicos;
using INMETRO.CIPP.SHARED.Servicos;
using Quartz;

namespace INMETRO.CIPP.WEB.Agendamento
{
    public class DownloadPorRotinaAutomaticaJob : IJob
    {
       
        private readonly OrganismoRepositorio _repositorio;
        private readonly GerenciarArquivoCompactado _descompactar;
        private readonly GerenciarFtp _ftp;
        private readonly GerenciarCsv _csv;
        private readonly InspecaoDominioServico _inspecao;
       


        public DownloadPorRotinaAutomaticaJob()
        {
            var inspecaoRepositorio = new InspecaoRepositorio();
            _repositorio = new OrganismoRepositorio();
            _inspecao = new InspecaoDominioServico(inspecaoRepositorio, _repositorio);
            _descompactar = new GerenciarArquivoCompactado();
            _ftp = new GerenciarFtp();
            _csv = new GerenciarCsv();
          
        }

        public void Execute(IJobExecutionContext context)
        {
            var servico = new DownloadServico(_repositorio, _ftp, _descompactar, _csv, _inspecao);
            servico.DownloadInspecoesPorRotinaAutomatica().ConfigureAwait(true);
        }
    }
}