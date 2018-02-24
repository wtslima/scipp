using INMETRO.CIPP.INFRA.REPOSITORIO.Repositorios;
using INMETRO.CIPP.SERVICOS.Servicos;
using INMETRO.CIPP.SHARED.Servicos;
using Quartz;

namespace INMETRO.CIPP.WEB.Agendamento
{
    public class ExclusaoPorRotinaAutomaticaJob : IJob
    {
       
        private readonly OrganismoRepositorio _repositorio;
        private readonly GerenciarArquivoCompactado _descompactar;
        private readonly GerenciarFtp _ftp;
        private readonly GerenciarCsv _csv;
        public InspecaoServico Inspecao { get; }


        public ExclusaoPorRotinaAutomaticaJob()
        {
            var inspecaoRepositorio = new InspecaoRepositorio();
            _repositorio = new OrganismoRepositorio();
            _descompactar = new GerenciarArquivoCompactado();
            _ftp = new GerenciarFtp();
            _csv = new GerenciarCsv();
            Inspecao = new InspecaoServico(null);
        }

        public void Execute(IJobExecutionContext context)
        {
            var servico = new DownloadServico(_repositorio, _ftp, _descompactar, _csv, null);
            servico.ExcluirInspecaoPorRotinaAutomatica().ConfigureAwait(true);
        }
    }
}