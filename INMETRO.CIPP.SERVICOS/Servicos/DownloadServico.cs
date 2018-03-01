using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using INMETRO.CIPP.DOMINIO.Interfaces;
using INMETRO.CIPP.DOMINIO.Interfaces.Repositorios;
using INMETRO.CIPP.DOMINIO.Mensagens;
using INMETRO.CIPP.DOMINIO.Modelos;
using INMETRO.CIPP.DOMINIO.Servicos;
using INMETRO.CIPP.INFRA.REPOSITORIO.Repositorios;
using INMETRO.CIPP.SERVICOS.Interfaces;
using INMETRO.CIPP.SERVICOS.ModelService;
using INMETRO.CIPP.SHARED;
using INMETRO.CIPP.SHARED.Email;
using INMETRO.CIPP.SHARED.Interfaces;
using INMETRO.CIPP.SHARED.ModelShared;

namespace INMETRO.CIPP.SERVICOS.Servicos
{
    public class DownloadServico : IDownloadServico
    {
        private readonly IOrganismoDominioService __organismoDomainService;
        private readonly IGerenciarFtp _ftp;
        private readonly IGerenciarArquivoCompactado _descompactar;
        private readonly IGerenciarCsv _csv;
        private readonly IInspecaoDominioService _inspecaoServico;
        private readonly IHistorico _historicoServico;

        private readonly string _pathLocal = ConfigurationManager.AppSettings["LocalPath"];

        readonly List<InspecaoModelServico> _listaInspecoesParaEnvio = new List<InspecaoModelServico>();

        readonly List<ExcecaoService> _listaExcecao = new List<ExcecaoService>();


        public DownloadServico(IOrganismoDominioService organismoDomainService, IGerenciarFtp ftp, IGerenciarArquivoCompactado descompactar, IGerenciarCsv csv, IInspecaoDominioService inspecaoServico)
        {
            __organismoDomainService = organismoDomainService;
            _ftp = ftp;
            _descompactar = descompactar;
            _csv = csv;
            _inspecaoServico = inspecaoServico;
            IHistoricoRepositorio historicoRepositorio = new HistoricoDownloadRepositorio();
            _historicoServico = new HistoricoServico(historicoRepositorio);
          
        }
         

        #region Download Por CIPP

        public RetornoDownloadModel DownloadInspecaoPorUsuario(string codigoOia, string cipp, string usuario)
        {
            try
            {
                var organismo = __organismoDomainService.BuscarOrganismoPorId(codigoOia);

                var existeOrganismo = TemOrganismo(organismo);
                if (existeOrganismo.ExisteExcecao)
                    return existeOrganismo;
                if (!string.IsNullOrEmpty(cipp))
                {
                    var retorno = _inspecaoServico.ObterInspecaoParaCippECodigoOiaInformado(codigoOia, cipp);
                    var existeCippParaCodigoOia = TemCippParaOrganismoInformado(retorno);
                    if (existeCippParaCodigoOia.ExisteExcecao)
                        return existeCippParaCodigoOia;
                }

                var retornoDownload = VerificarFtpValido(organismo.FtpInfo, codigoOia);

                if (retornoDownload.ExisteExcecao)
                    return retornoDownload;

                var diretoriosCippRemoto = ObterListaDiretoriosPorOrganismo(organismo.FtpInfo);

                retornoDownload = VerificarDiretorios(diretoriosCippRemoto, codigoOia);

                if (!retornoDownload.ExisteExcecao)
                    return retornoDownload;


                foreach (var diretorioCippRemoto in diretoriosCippRemoto)
                {

                    try
                    {
                        var diretorioLocal = ObterDiretorioLocal(organismo.FtpInfo.DiretorioInspecaoLocal, diretorioCippRemoto);
                        if (TemInspecaoValida(diretorioCippRemoto)) continue;
                        if (TemCipp(cipp, diretorioCippRemoto))
                        {

                            DownloadInspecao(organismo.FtpInfo, diretorioLocal, diretorioCippRemoto, usuario);
                            retornoDownload.ExisteExcecao = false;
                            retornoDownload.Mensagem = string.Format(MensagemSistema.SucessoDownloadCodigoOiaeCipp,
                                codigoOia, cipp);
                            return retornoDownload;
                        }

                        DownloadInspecao(organismo.FtpInfo, diretorioLocal, diretorioCippRemoto, usuario);
                    }
                    catch (Exception e)
                    {
                        throw e;

                    }

                }
                retornoDownload.ExisteExcecao = false;
                retornoDownload.Mensagem = string.Format(MensagemSistema.SucessoDownloadCodigoOia, codigoOia);
                return retornoDownload;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion

        #region Download Por Rotina Automatica
        public async Task<bool> DownloadInspecoesPorRotinaAutomatica()
        {
            try
            {
                var organismos = await __organismoDomainService.BuscarTodosOrganismos();
                Notificacao enviar = new Notificacao();
                if (!organismos.GroupBy(f => f.FtpInfo).Any()) return false;

                foreach (var item in organismos.GroupBy(c => c.FtpInfo))
                {
                    try
                    {
                        var diretoriosCippRemoto = ObterListaDiretoriosPorOrganismo(item.Key);
                        if (diretoriosCippRemoto.Length <= 0) continue;
                        DownloadInspecaoAutomatica(item.Key, diretoriosCippRemoto);
                    }
                    catch (Exception e)
                    {
                        enviar.EnviarEmail("wslima@colaborador.inmetro.gov.br",  e.Message);
                    }


                }
               
                EnviarInspecoes(_listaInspecoesParaEnvio);
                return true;


            }
            catch (Exception e)
            {
                throw e;
            }

        }
        #endregion


        private string[] ObterListaDiretoriosPorOrganismo(FTPInfo ftpInfo)
        {
            try
            {
                string[] diretorios = { };
                var resultado = _ftp.ObterListaDiretoriosInspecoesFtp(ftpInfo);
                return resultado ?? diretorios;

            }
            catch (Exception e)
            {

                throw e;
            }

        }


        private void DownloadInspecaoAutomatica(FTPInfo ftpInfo, IEnumerable<string> diretorios)
        {

            foreach (var item in diretorios)
            {
                try
                {
                    if (TemInspecaoValida(item)) continue;
                    var diretorioLocal = ObterDiretorioLocal(ftpInfo.DiretorioInspecaoLocal, item);
                    if (!_ftp.DownloadInspecaoFtp(item, diretorioLocal, ftpInfo)) continue;
                    if (!_descompactar.DescompactarArquivo(diretorioLocal, item)) continue;
                    var inspecao = Conversao.ConverterParaModeloServico(_csv.ObterDadosInspecao(diretorioLocal));
                    if (!GravarInspecao(Conversao.ConverterParaDominio(inspecao))) continue;
                    _listaInspecoesParaEnvio.Add(inspecao);
                    if (!GravarHistoricoDownload(item, "Rotina Automática")) continue;
                    ExcluirArquivoCompactadoECsv(diretorioLocal, item);


                }
                catch (Exception e)
                {
                    throw e;
                }

            }

        }

        private void DownloadInspecao(FTPInfo ftpInfo, string diretorioLocal, string diretorioRemoto, string usuario)
        {
            try
            {

                if (!_ftp.DownloadInspecaoFtp(diretorioRemoto, diretorioLocal, ftpInfo)) return;
                if (!_descompactar.DescompactarArquivo(diretorioLocal, diretorioRemoto)) return;
                if (!GravarInspecao(Conversao.ConverterParaDominio(_csv.ObterDadosInspecao(diretorioLocal)))) return;
                if (!GravarHistoricoDownload(diretorioRemoto, usuario)) return;
                ExcluirArquivoCompactadoECsv(diretorioLocal, diretorioRemoto);

            }
            catch (Exception e)
            {
                throw e;
            }

        }

        private RetornoDownloadModel VerificarDiretorios(string[] diretorios, string codigo)
        {
            var retornoDownload = new RetornoDownloadModel();
            var inspecoes = ExisteInspecoesGravadas(diretorios);
            if (diretorios.Length != 0 && inspecoes > 0) return new RetornoDownloadModel();
            retornoDownload.ExisteExcecao = true;
            retornoDownload.Mensagem = string.Format(MensagemSistema.SemNovosDiretorios, codigo);
            return retornoDownload;
        }

        private static RetornoDownloadModel VerificarFtpValido(FTPInfo ftpInfos, string codigo)
        {
            var retornoDownload = new RetornoDownloadModel();

            if (ftpInfos != null) return new RetornoDownloadModel();
            retornoDownload.ExisteExcecao = true;
            retornoDownload.Mensagem = string.Format(MensagemSistema.FtpInvalido, codigo);
            return retornoDownload;
        }


        private bool GravarInspecao(Inspecao inspecao)
        {
            try
            {
                return _inspecaoServico.AdicionarDadosInspecao(inspecao);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        private bool GravarHistoricoDownload(string cipp, string usuario)
        {
            var retorno = Path.GetFileNameWithoutExtension(cipp);
            var inspecao = _inspecaoServico.ObterDadosInspecaoPorCipp(retorno);
            if (inspecao.Id <= 0) return false;
            var historicoModel = new HistoricoServiceModel
            {
                IdInspecao = inspecao.Id,
                Usuario = usuario,
                DataEntrada = DateTime.Now
            };

            return _historicoServico.AdicionarInspecao(Conversao.ConverterParaDominio(historicoModel));

        }

        private void ExcluirArquivoCompactadoECsv(string diretorioLocal, string file)
        {
            try
            {
                if (!_descompactar.ExcluirArquivoLocal(diretorioLocal, file)) return;
                _csv.ExcluirArquivoCippCsv(diretorioLocal);

            }
            catch (Exception e)
            {

                throw e;
            }

        }

        private static bool TemCipp(string cipp, string cippServer)
        {
            var cippServe = Path.GetFileNameWithoutExtension(cippServer);
            if (string.IsNullOrWhiteSpace(cipp)) return false;
            bool combinam = cipp.Equals(cippServe);
            if (combinam)
            {
                return true;
            }
            return false;
        }

        private string ObterDiretorioLocal(string localDiretorio, string fileCipp)
        {
            var cippServe = Path.GetFileNameWithoutExtension(fileCipp);
            var path = _pathLocal + localDiretorio + "\\" + cippServe + "\\";
            bool folderExists = Directory.Exists(path);

            if (!folderExists)
            {
                Directory.CreateDirectory(path);
                return path;
            }
            return path;
        }
        

        private bool TemInspecaoValida(string cipp)
        {
            var inspecao = Path.GetFileNameWithoutExtension(cipp);

            return _inspecaoServico.TemInspecao(inspecao);

        }

        private void EnviarInspecoes(IList<InspecaoModelServico> inspecoes)
        {
            var lista = new List<InspecaoCsvModel>();

            foreach (var item in inspecoes)
            {
                var inspecaoModelServico = new InspecaoCsvModel
                {
                    CodigoCipp = item.CodigoCipp,
                    CodigoOia = item.CodigoOia,
                    PlacaLicenca = item.Placa,
                    NumeroEquipamento = Convert.ToInt32(item.Equipamento),
                    DataInspecao = item.DataInspecao,
                    Responsavel = item.Responsavel
                };

                lista.Add(inspecaoModelServico);
            }

            _csv.CriarArquivoInspecoesAnexo(lista);
        }

        private int ExisteInspecoesGravadas(IEnumerable<string> listaInspecao)
        {
            List<string> inspecoesNaoGravadas = new List<string>();
            foreach (var item in listaInspecao)
            {
                if (!TemInspecaoValida(item))
                    inspecoesNaoGravadas.Add(item);
            }

            return inspecoesNaoGravadas.Count;
        }

        private RetornoDownloadModel TemOrganismo (Organismo organismo)
        {
            if (organismo.ExcecaoDominio.ExisteExcecao)
            {
                return new RetornoDownloadModel
                {
                    ExisteExcecao = organismo.ExcecaoDominio.ExisteExcecao,
                    Mensagem = organismo.ExcecaoDominio.Mensagem
                };
            }

            return new RetornoDownloadModel();
        }

        private RetornoDownloadModel TemCippParaOrganismoInformado(Inspecao inspecao)
        {
            if (!inspecao.ExcecaoDominio.ExisteExcecao)
            {
                return new RetornoDownloadModel
                {
                    ExisteExcecao =true,
                    Mensagem = inspecao.ExcecaoDominio.Mensagem
                };
            }

            return new RetornoDownloadModel();
        }

    }

}