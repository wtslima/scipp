using System;
using System.Collections.Generic;
using System.Configuration;
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
        private readonly IOrganismoDominioService _organismoDomainService;
        private readonly IGerenciarFtp _ftp;
        private readonly IGerenciarArquivoCompactado _descompactar;
        private readonly IGerenciarCsv _csv;
        private readonly IInspecaoDominioService _inspecaoServico;
        private readonly IHistorico _historicoServico;

        private readonly string _pathLocal = ConfigurationManager.AppSettings["LocalPath"];

        private readonly List<InspecaoModelServico> _listaInspecoesParaEnvio = new List<InspecaoModelServico>();

        readonly Notificacao _enviar = new Notificacao();
        // readonly List<ExcecaoService> _listaExcecao = new List<ExcecaoService>();


        public DownloadServico(IOrganismoDominioService organismoDomainService, IGerenciarFtp ftp, IGerenciarArquivoCompactado descompactar, IGerenciarCsv csv, IInspecaoDominioService inspecaoServico)
        {
            _organismoDomainService = organismoDomainService;
            _ftp = ftp;
            _descompactar = descompactar;
            _csv = csv;
            _inspecaoServico = inspecaoServico;
            IHistoricoRepositorio historicoRepositorio = new HistoricoDownloadRepositorio();
            _historicoServico = new HistoricoServico(historicoRepositorio);
          
        }
         

        #region Download Por CIPP

        public InspecoesGravadasModelServico DownloadInspecaoPorUsuario(string codigoOia, string cipp, string usuario)
        {
            try
            {
                var organismo = _organismoDomainService.BuscarOrganismoPorId(codigoOia);

                var existeExcecaoInspecao = TemOrganismo(organismo);
                if (existeExcecaoInspecao.Excecao.ExisteExcecao)
                    return existeExcecaoInspecao;
                if (!string.IsNullOrEmpty(cipp))
                {
                    var retorno = _inspecaoServico.ObterInspecaoParaCippECodigoOiaInformado(codigoOia, cipp);
                    var existeCippParaCodigoOia = TemCippParaOrganismoInformado(retorno);
                    if (existeCippParaCodigoOia.Excecao.ExisteExcecao)
                        return existeCippParaCodigoOia;
                }

                var ftpValido = VerificarFtpValido(organismo.FtpInfo, codigoOia);

                if (ftpValido.Excecao.ExisteExcecao)
                    return ftpValido;

                var diretoriosCippRemoto = ObterListaDiretoriosPorOrganismo(organismo.FtpInfo);

                existeExcecaoInspecao = VerificarDiretorios(diretoriosCippRemoto, codigoOia);

                if (existeExcecaoInspecao.Excecao.ExisteExcecao)
                    return existeExcecaoInspecao;


                foreach (var diretorioCippRemoto in diretoriosCippRemoto)
                {

                    try
                    {
                        var diretorioLocal = ObterDiretorioLocal(organismo.FtpInfo.DiretorioInspecaoLocal, diretorioCippRemoto);
                        if (TemInspecaoValida(diretorioCippRemoto)) continue;
                        if (TemCipp(cipp, diretorioCippRemoto))
                        {

                            DownloadInspecao(organismo.FtpInfo, diretorioLocal, diretorioCippRemoto, usuario);
                            return new InspecoesGravadasModelServico
                            {
                                InspecoesGravadas = _listaInspecoesParaEnvio,
                                Excecao = new ExcecaoService
                                {
                                    ExisteExcecao = false,
                                    Mensagem = string.Format(MensagemSistema.SucessoDownloadCodigoOiaeCipp,
                                        codigoOia, cipp)
                                }
                               
                            };
                        }

                        DownloadInspecao(organismo.FtpInfo, diretorioLocal, diretorioCippRemoto, usuario);
                    }
                    catch (Exception e)
                    {
                        throw e;

                    }

                }
                return new InspecoesGravadasModelServico
                {
                    InspecoesGravadas = _listaInspecoesParaEnvio,
                    Excecao = new ExcecaoService
                    {
                        ExisteExcecao = false,
                        Mensagem = string.Format(MensagemSistema.SucessoDownloadCodigoOia, codigoOia)
                    }

                };
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
                var organismos = await _organismoDomainService.BuscarTodosOrganismos();
               
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
                        _enviar.EnviarEmail("wslima@colaborador.inmetro.gov.br",  e.Message);
                        _enviar.EnviarEmail("astrindade@colaborador.inmetro.gov.br", e.Message);
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
                var inspecao = Conversao.ConverterParaModeloServico(_csv.ObterDadosInspecao(diretorioLocal));
                if (!GravarInspecao(Conversao.ConverterParaDominio(inspecao))) return;
                _listaInspecoesParaEnvio.Add(inspecao);
                if (!GravarHistoricoDownload(diretorioRemoto, usuario)) return;
                ExcluirArquivoCompactadoECsv(diretorioLocal, diretorioRemoto);


            }
            catch (Exception e)
            {
                throw e;
            }

        }

        private InspecoesGravadasModelServico VerificarDiretorios(string[] diretorios, string codigo)
        {
           
            var inspecoes = ExisteInspecoesGravadas(diretorios);
            if (diretorios.Length != 0 && inspecoes > 0) return new InspecoesGravadasModelServico
            {
                InspecoesGravadas = new List<InspecaoModelServico>(),
                Excecao = new ExcecaoService
                {
                    ExisteExcecao = false,
                    Mensagem = string.Empty
                }
            };
            return new InspecoesGravadasModelServico
            {
                InspecoesGravadas = new List<InspecaoModelServico>(),
                Excecao = new ExcecaoService
                {
                    ExisteExcecao = true,
                    Mensagem = string.Format(MensagemSistema.SemNovosDiretorios, codigo)
                }
            };
        }

        private static InspecoesGravadasModelServico VerificarFtpValido(FTPInfo ftpInfos, string codigo)
        {
           

            if (ftpInfos != null)return new InspecoesGravadasModelServico
            {
                InspecoesGravadas = new List<InspecaoModelServico>(),
                Excecao = new ExcecaoService
                {
                    ExisteExcecao = false,
                    Mensagem = string.Empty
                }
            };
            return new InspecoesGravadasModelServico
            {
                InspecoesGravadas = new List<InspecaoModelServico>(),
                Excecao = new ExcecaoService
                {
                    ExisteExcecao = true,
                    Mensagem = string.Format(MensagemSistema.FtpInvalido, codigo)
                }
            };
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

        private void EnviarInspecoes(IEnumerable<InspecaoModelServico> inspecoes)
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
                    DataInspecao = item.DataInspecao
                 
                };

                lista.Add(inspecaoModelServico);
            }
            if (lista.Count > 0)
            {
                _csv.CriarArquivoInspecoesAnexo(lista);
            }
            else
            {
                _enviar.EnviarEmail("astrindade@colaborador.inmetro.gov.br", string.Format(MensagemSistema.NenhumArquivoEncontrado));
                _enviar.EnviarEmail("wslima@colaborador.inmetro.gov.br", string.Format(MensagemSistema.NenhumArquivoEncontrado));
            }
           


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

        private InspecoesGravadasModelServico TemOrganismo (Organismo organismo)
        {
            if (organismo.ExcecaoDominio.ExisteExcecao)
            {
                return new InspecoesGravadasModelServico
                {
                    InspecoesGravadas = new List<InspecaoModelServico>(),
                    Excecao = new ExcecaoService
                    {
                        ExisteExcecao = organismo.ExcecaoDominio.ExisteExcecao,
                        Mensagem = organismo.ExcecaoDominio.Mensagem
                    }
                };

            }

            return new InspecoesGravadasModelServico
            {
                InspecoesGravadas = new List<InspecaoModelServico>(),
                Excecao = new ExcecaoService
                {
                    ExisteExcecao = false,
                    Mensagem = string.Empty
                }
            };
        }

        private InspecoesGravadasModelServico TemCippParaOrganismoInformado(Inspecao inspecao)
        {
            if (!inspecao.ExcecaoDominio.ExisteExcecao)
            {
                return new InspecoesGravadasModelServico
                {
                    InspecoesGravadas = new List<InspecaoModelServico>(),
                    Excecao = new ExcecaoService
                    {
                        ExisteExcecao = true,
                        Mensagem = string.Format(MensagemNegocio.InspecaoJaGravadaParaCippEOia)
                    }
                };
            }

            return new InspecoesGravadasModelServico
            {
                InspecoesGravadas = new List<InspecaoModelServico>(),
                Excecao = new ExcecaoService
                {
                    ExisteExcecao = false,
                    Mensagem = string.Empty
                }
            };
        }

    }

}