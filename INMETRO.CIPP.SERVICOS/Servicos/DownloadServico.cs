﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using INMETRO.CIPP.DOMINIO.Interfaces;
using INMETRO.CIPP.DOMINIO.Interfaces.Repositorios;
using INMETRO.CIPP.DOMINIO.Mensagens;
using INMETRO.CIPP.DOMINIO.Modelos;
using INMETRO.CIPP.DOMINIO.Servicos;
using INMETRO.CIPP.INFRA.REPOSITORIO.Repositorios;
using INMETRO.CIPP.SERVICOS.ExcecaoServico;
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
        private readonly IGerenciarSftp _sftp;


        private readonly List<InspecaoModelServico> _listaInspecoesParaEnvio = new List<InspecaoModelServico>();

        readonly Notificacao _enviar = new Notificacao();
        List<Exception> _listExcecao = new List<Exception>();

        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public DownloadServico(IOrganismoDominioService organismoDomainService, IGerenciarFtp ftp, IGerenciarArquivoCompactado descompactar, IGerenciarCsv csv, IInspecaoDominioService inspecaoServico, IGerenciarSftp sftp)
        {
            _organismoDomainService = organismoDomainService;
            _ftp = ftp;
            _descompactar = descompactar;
            _csv = csv;
            _inspecaoServico = inspecaoServico;
            _sftp = sftp;
            IHistoricoRepositorio historicoRepositorio = new HistoricoDownloadRepositorio();
            _historicoServico = new HistoricoServico(historicoRepositorio);

        }

        #region Download Por CIPP

        public InspecoesGravadasModelServico DownloadInspecaoPorUsuario(string codigoOia, string cipp, string usuario)
        {
            try
            {
                //var resultado = ObterCodigoOia(codigoOia);

                var organismo = _organismoDomainService.BuscarOrganismoPorId(codigoOia);

              

                var existeExcecaoInspecao = TemOrganismo(organismo);
                if (existeExcecaoInspecao.Excecao.ExisteExcecao)
                    return existeExcecaoInspecao;


                existeExcecaoInspecao = VerificarFtpValido(organismo.IntegracaoInfo, codigoOia);

                if (existeExcecaoInspecao.Excecao.ExisteExcecao)
                    return existeExcecaoInspecao;

                if (!string.IsNullOrEmpty(cipp))
                {
                    var retorno = _inspecaoServico.ObterInspecaoParaCippECodigoOiaInformado(codigoOia, cipp);
                    var existeCippParaCodigoOia = TemCippParaOrganismoInformado(retorno);
                    if (existeCippParaCodigoOia.Excecao.ExisteExcecao)
                        return existeCippParaCodigoOia;
                }


                var diretoriosCippRemoto = ObterListaDiretoriosPorOrganismo(organismo.IntegracaoInfo);

                existeExcecaoInspecao = VerificarDiretorios(diretoriosCippRemoto, codigoOia, cipp);
                if (existeExcecaoInspecao.Excecao.ExisteExcecao)
                    return existeExcecaoInspecao;

                if (!string.IsNullOrEmpty(cipp))
                {
                    return DownloadInspecoaPorCippInformado(organismo,
                        existeExcecaoInspecao.DiretoriosValidos.FirstOrDefault(), usuario);
                }

                return DownloadInspecoaPorCodigoOiaInformado(organismo, existeExcecaoInspecao.DiretoriosValidos,
                    usuario);


            }
         
            catch (Exception exec)
            {
                 
                _enviar.EnviarEmail(Configurations.EmailAdministrador(), _listExcecao, codigoOia);

                throw new Exception($"Erro no Download de Inspeção pelo usuário {usuario}. Exceção {exec.Message}");
            }
           
        }

        private int ObterCodigoOia(string valor)
        {
            string pattern = "-";            // Split on hyphens

            string[] substrings = Regex.Split(valor, pattern);
            foreach (string match in substrings)
            {
                return Convert.ToInt32(match);
            }

            return 0;
        }
        private InspecoesGravadasModelServico DownloadInspecoaPorCippInformado(Organismo organismo, string diretorioRemoto, string usuario)
        {
            var diretorioLocal = ObterDiretorioLocal(organismo.IntegracaoInfo.DiretorioInspecaoLocal,
                diretorioRemoto);
            DownloadInspecao(organismo.IntegracaoInfo, diretorioLocal, diretorioRemoto, usuario);

            var listaErros = _listExcecao;

            if (listaErros.Count > 0)
            {
                _enviar.EnviarEmail(Configurations.EmailAdministrador(), _listExcecao, organismo.CodigoOIA.ToString());
            }
                

            return new InspecoesGravadasModelServico
            {
                InspecoesGravadas = _listaInspecoesParaEnvio,
                Excecao = new ExcecaoService
                {
                    ExisteExcecao = false,
                    Mensagem = string.Format(MensagemSistema.SucessoDownloadCodigoOiaeCipp,
                        organismo.CodigoOIA, diretorioRemoto)
                }

            };
        }
        private InspecoesGravadasModelServico DownloadInspecoaPorCodigoOiaInformado(Organismo organismo, List<string> diretoriosRemotoValidos, string usuario)
        {
            foreach (var diretorioRemoto in diretoriosRemotoValidos)
            {
                var diretorioLocal = ObterDiretorioLocal(organismo.IntegracaoInfo.DiretorioInspecaoLocal,
                    diretorioRemoto);
                DownloadInspecao(organismo.IntegracaoInfo, diretorioLocal, diretorioRemoto, usuario);
                var listaErros = _listExcecao;
                if (listaErros.Count > 0)
                {
                    _enviar.EnviarEmail(Configurations.EmailAdministrador(), _listExcecao, organismo.CodigoOIA.ToString());
                }

            }
            return new InspecoesGravadasModelServico
            {
                InspecoesGravadas = _listaInspecoesParaEnvio,
                Excecao = new ExcecaoService
                {
                    ExisteExcecao = false,
                    Mensagem = string.Format(MensagemSistema.SucessoDownloadCodigoOia,
                        organismo.CodigoOIA)
                }

            };
        }

        #endregion


        #region Download Por Rotina Automatica
        public async Task<bool> DownloadInspecoesPorRotinaAutomatica()
        {
            try
            {
                var organismos = await _organismoDomainService.BuscarTodosOrganismos();

                if (!organismos.GroupBy(f => f.IntegracaoInfo).Any()) return false;
                foreach (var item in organismos.GroupBy(c => c.IntegracaoInfo))
                {
                    var cod = item.Key.DiretorioInspecaoLocal.Replace("\\", "");
                     var name = _organismoDomainService.BuscarOrganismoPorId(cod).Nome;
                    try
                    {
                        var diretoriosCippRemoto = ObterListaDiretoriosPorOrganismo(item.Key);
                        var existeExcecaoInspecao = VerificarDiretorios(diretoriosCippRemoto, "", "");
                        if (existeExcecaoInspecao.DiretoriosValidos.Count <= 0) continue;

                        DownloadInspecaoAutomatica(item.Key, existeExcecaoInspecao.DiretoriosValidos);
                    }
                    catch (Exception e)
                    {
                        _listExcecao.Add(e);
                        _enviar.EnviarEmail(Configurations.EmailAdministrador(), _listExcecao, name);
                         EnviarLogParaOrganismo(CriarArquivoDeLog(_listExcecao), item.Key);
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


        private string[] ObterListaDiretoriosPorOrganismo(IntegracaoInfos ftpInfo)
        {
            try
            {
               
                //ftps ou ftp
                if (ftpInfo.TipoIntegracao == 1)
                {
                    var diretorios = _ftp.ObterListaDiretoriosInspecoesFtp(ftpInfo);
                    return diretorios;
                }

                return _sftp.ObterArquivosNoDiretorioRemotoSftp(ftpInfo);

            }
            catch (Exception e)
            {
                _listExcecao.Add(e);
                throw e;
            }

        }


        private void DownloadInspecaoAutomatica(IntegracaoInfos ftpInfo, IEnumerable<string> diretorios)
        {
            var diretoriosValidos = new List<string>();
            if (diretorios != null)
            {
                 diretoriosValidos = ObterSomenteDiretoriosValidos(diretorios);
            }
            foreach (var item in diretoriosValidos)
            {
                try
                {
                    
                    var diretorioLocal = ObterDiretorioLocal(ftpInfo.DiretorioInspecaoLocal, item);
                    if (!DownloadArquivo(item, diretorioLocal, ftpInfo)) continue;
                    if (!_descompactar.DescompactarArquivo(diretorioLocal, item)) continue;
                    var inspecao = Conversao.ConverterParaModeloServico(_csv.ObterDadosInspecao(diretorioLocal, ftpInfo));
                    if (!GravarInspecao(Conversao.ConverterParaDominio(inspecao))) return;
                    _listaInspecoesParaEnvio.Add(inspecao);
                    if (!GravarHistoricoDownload(item, "Rotina Automática")) continue;
                    ExcluirArquivoCompactadoECsv(diretorioLocal, item);

                }
                catch (Exception e)
                {
                    _listExcecao.Add(e);
                   
                    EnviarLogParaOrganismo(CriarArquivoDeLog(_listExcecao), ftpInfo);
                }

            }

            
        }

        private void DownloadInspecao(IntegracaoInfos ftpInfo, string diretorioLocal, string diretorioRemoto, string usuario)
        {
            try
            {
                if (!DownloadArquivo(diretorioRemoto, diretorioLocal, ftpInfo)) return;
                if (!_descompactar.DescompactarArquivo(diretorioLocal, diretorioRemoto)) return;
                var inspecao = Conversao.ConverterParaModeloServico(_csv.ObterDadosInspecao(diretorioLocal, ftpInfo));
                if (!GravarInspecao(Conversao.ConverterParaDominio(inspecao))) return;
                _listaInspecoesParaEnvio.Add(inspecao);
                if (!GravarHistoricoDownload(diretorioRemoto, usuario)) return;
                ExcluirArquivoCompactadoECsv(diretorioLocal, diretorioRemoto);

            }
            catch (Exception e)
            {
               _listExcecao.Add(e);
            }

        }

        private InspecoesGravadasModelServico VerificarDiretorios(IReadOnlyCollection<string> diretorios, string codigo, string cipp)
        {
            if (diretorios.Count == 0)
                return new InspecoesGravadasModelServico
                {
                    InspecoesGravadas = new List<InspecaoModelServico>(),
                    Excecao = new ExcecaoService
                    {
                        ExisteExcecao = true,
                        Mensagem = string.Format(MensagemSistema.SemNovosDiretorios, codigo)
                    }
                };
            var diretoriosValidos = ObterSomenteDiretoriosValidos(diretorios);
            if (diretoriosValidos.Count <= 0)
                return new InspecoesGravadasModelServico
                {
                    InspecoesGravadas = new List<InspecaoModelServico>(),
                    Excecao = new ExcecaoService
                    {
                        ExisteExcecao = true,
                        Mensagem = string.Format(MensagemSistema.DiretoriosInvalidos, codigo)
                    }
                };


            var inspecoes = ObterInspecoesNaoGravadas(diretoriosValidos).ToList();

            if (string.IsNullOrEmpty(cipp))
            {
               return DownloadExcecaoService.ObterInspecaoValidaParaCodigoOiaInformado(inspecoes, codigo);
            }

            return DownloadExcecaoService.ObterInspecaoValidaParaCippInformado(inspecoes, cipp, codigo);

        }



        private static InspecoesGravadasModelServico VerificarFtpValido(IntegracaoInfos ftpInfos, string codigo)
        {


            if (ftpInfos != null) return new InspecoesGravadasModelServico
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
                throw new Exception($"Erro ao gravar Inspeção. Exceção {e}");
            }

        }

        private bool GravarHistoricoDownload(string cipp, string usuario)
        {
            try
            {
                var cippGravado = Path.GetFileNameWithoutExtension(cipp);
                var inspecao = _inspecaoServico.ObterDadosInspecaoPorCipp(cippGravado);
                if (inspecao.Id <= 0) return false;
                var historicoModel = new HistoricoServiceModel
                {
                    IdInspecao = inspecao.Id,
                    Usuario = usuario,
                    DataEntrada = DateTime.Now
                };

                return _historicoServico.AdicionarInspecao(Conversao.ConverterParaDominio(historicoModel));
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao gravar Historico. Exceção {e}");
            }


        }

        private string CriarArquivoDeLog(List<Exception> erros)
        {
            var dataLog = DateTime.Now.ToString("yyyy-MM-dd_HH-mm", CultureInfo.InvariantCulture);
            string diretorioTemporario = Environment.GetEnvironmentVariable("TEMP");
            string fileName = diretorioTemporario + "Log" + dataLog  +".txt";

            FileInfo file = new FileInfo(fileName);

            if (file.Exists)
            {
                file.Delete();
            }
            
            using (StreamWriter sw = new StreamWriter(fileName, true))
            {
                foreach (var item in erros)
                {
                    sw.WriteLine("Data e hora {0}", DateTime.Now.ToString("yyyy-MM-dd_HH-mm", CultureInfo.InvariantCulture));
                    sw.WriteLine("Erro : {0}", item.Message);
                }
               
            }

            return fileName;
        }

        private void EnviarLogParaOrganismo(string fileName, IntegracaoInfos sftp)
        {
            if (sftp.TipoIntegracao != 1)
            {
                _sftp.CreateDirectory(sftp);
                _sftp.UploadFile(fileName, sftp);
            }
        }

        private void ExcluirArquivoCompactadoECsv(string diretorioLocal, string file)
        {
            try
            {
                _csv.ExcluirArquivoCippCsv(diretorioLocal);
                if (!_descompactar.ExcluirArquivoLocal(diretorioLocal, file)) return;
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao Excluir arquivo. Exceção {e}");
            }

        }

        private InspecoesGravadasModelServico TemCipp(string cipp, string cippServer)
        {
            var cippServe = Path.GetFileNameWithoutExtension(cippServer);
            bool combinam = cipp.Equals(cippServe);
            if (combinam)
            {
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
            return new InspecoesGravadasModelServico
            {
                InspecoesGravadas = new List<InspecaoModelServico>(),
                Excecao = new ExcecaoService
                {
                    ExisteExcecao = true,
                    Mensagem = string.Format(MensagemSistema.NenhumInspecaoEncontradoParaCodigoCipp, cipp)
                }
            };
        }

        private string ObterDiretorioLocal(string localDiretorio, string fileCipp)
        {
            var cippServe = Path.GetFileNameWithoutExtension(fileCipp);
            var path = Configurations.DiretorioRaiz() + localDiretorio + "\\" + cippServe + "\\";
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
                    NumeroEquipamento = item.Equipamento,
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
                _enviar.EnviarEmail(Configurations.EmailAdministrador(), _listExcecao,"");
            }

        }

        private IEnumerable<string> ObterInspecoesNaoGravadas(IEnumerable<string> listaInspecao)
        {
            var inspecoesNaoGravadas = new List<string>();
            foreach (var item in listaInspecao)
            {
                if (!TemInspecaoValida(item))
                {
                    inspecoesNaoGravadas.Add(item);
                }
            }

            return inspecoesNaoGravadas.ToList();
        }

        private InspecoesGravadasModelServico TemOrganismo(Organismo organismo)
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
            if (inspecao.ExcecaoDominio.ExisteExcecao)
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

        private bool DownloadArquivo(string file, string diretorioLocal, IntegracaoInfos integracao)
        {
            if (integracao.TipoIntegracao != 1) return _sftp.DownloadArquivo(file, diretorioLocal + file, integracao);
            return _ftp.DownloadInspecaoFtp(file, diretorioLocal, integracao);
        }

        private List<string> ObterSomenteDiretoriosValidos(IEnumerable<string> diretorios)
        {
            var diretoriosValidos = new List<string>();
            foreach (var item in diretorios)
            {
                var extension = Path.GetExtension(item);
                if (extension != null && (extension.Equals(".zip") || extension.Equals(".rar")))
                {

                    diretoriosValidos.Add(item);
                }
            }

            return diretoriosValidos;

        }

    }

}