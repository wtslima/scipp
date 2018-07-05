using System.Collections.Generic;
using INMETRO.CIPP.DOMINIO.Servicos;
using INMETRO.CIPP.INFRA.REPOSITORIO.Repositorios;
using INMETRO.CIPP.SERVICOS.Servicos;
using INMETRO.CIPP.SHARED.Email;
using INMETRO.CIPP.SHARED.Servicos;
using NLog;
using Quartz;

namespace INMETRO.CIPP.WEB.Agendamento
{
    public class DownloadPorRotinaAutomaticaJob : IJob
    {

        private readonly OrganismoRepositorio _repositorio;
        private readonly OrganismoDominioServico _domainService;
        private readonly GerenciarArquivoCompactado _descompactar;
        private readonly GerenciarFtp _ftp;
        private readonly GerenciarCsv _csv;
        private readonly InspecaoDominioServico _inspecao;
        private readonly GerenciarSftp _sftp;



        public DownloadPorRotinaAutomaticaJob()
        {
            var inspecaoRepositorio = new InspecaoRepositorio();
            _repositorio = new OrganismoRepositorio();
            _domainService = new OrganismoDominioServico(_repositorio);
            _inspecao = new InspecaoDominioServico(inspecaoRepositorio, _repositorio);
            _descompactar = new GerenciarArquivoCompactado();
            _ftp = new GerenciarFtp();
            _csv = new GerenciarCsv();
            _sftp = new GerenciarSftp();

        }

        public void Execute(IJobExecutionContext context)
        {
            
             Notificacao _enviar = new Notificacao();
            _enviar.EnviarEmailErroDownloadAutomatico("wslima@colaborador.inmetro.gov.br",new List<string>());
            var servico = new DownloadServico(_domainService, _ftp, _descompactar, _csv, _inspecao, _sftp);
            servico.DownloadInspecoesPorRotinaAutomatica().ConfigureAwait(true);
        }

    }
}