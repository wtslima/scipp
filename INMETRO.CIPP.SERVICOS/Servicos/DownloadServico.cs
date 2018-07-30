using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
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
        readonly List<string> _listExcecao = new List<string>();


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
                        existeExcecaoInspecao.DiretoriosValidos.FirstOrDefault(s => s.Contains(cipp)), usuario);

                }
              
                return DownloadInspecoaPorCodigoOiaInformado(organismo, existeExcecaoInspecao.DiretoriosValidos,
                    usuario);

            }

            catch (Exception exec)
            {

                _enviar.EnviarEmail(Configurations.EmailAdministrador(), _listExcecao, codigoOia);

                throw new Exception($"Erro ao fazer download. {_listExcecao[0]}");
            }

        }

        private InspecoesGravadasModelServico DownloadInspecoaPorCippInformado(Organismo organismo, string diretorioRemoto, string usuario)
        {
            var diretorioLocal = ObterDiretorioLocal(organismo.IntegracaoInfo.DiretorioInspecaoLocal,
                diretorioRemoto);
            DownloadInspecao(organismo.IntegracaoInfo, diretorioLocal, diretorioRemoto, usuario);

            var listaErros = _listExcecao;

            if (listaErros.Count > 0)
            {
                _enviar.EnviarEmail(Configurations.EmailAdministrador(), _listExcecao, organismo.CodigoOIA);
                return new InspecoesGravadasModelServico
                {
                    InspecoesGravadas = _listaInspecoesParaEnvio,
                    Excecao = new ExcecaoService
                    {
                        ExisteExcecao = true,
                        Mensagem = string.Format(_listExcecao[0],
                       organismo.CodigoOIA, diretorioRemoto)
                    }

                };

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

            }
            if (_listExcecao.Count > 0)
            {
                _enviar.EnviarEmail(Configurations.EmailAdministrador(), _listExcecao, organismo.CodigoOIA);
            }
            //if (organismo.IntegracaoInfo.TipoIntegracao == 1)
            //{
            //    _ftp.GetHashCode();

            //}
            //var log = CriarArquivoDeLog(_listExcecao);
            //_sftp.UploadFile(log, organismo.IntegracaoInfo);

            //if (_listaInspecoesParaEnvio.Count > 0)
            //{
            //    EnviarInspecoes(_listaInspecoesParaEnvio);
            //}

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
                // Logger.Trace("Trace Agendamento Funcionando ...");
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
                        _listExcecao.Add($"Erro Download durante Inspeção automática para o OIA-{item.Key.DiretorioInspecaoLocal} - {name.ToUpper()} \n Exceção: {e.Message}");

                    }
                }
                _enviar.EnviarEmailErroDownloadAutomatico(Configurations.EmailAdministrador(), _listExcecao);
                //EnviarLogParaOrganismo(CriarArquivoDeLog(_listExcecao);

                EnviarInspecoes(_listaInspecoesParaEnvio);
                return true;

            }
            catch (Exception e)
            {
                EnviarInspecoes(_listaInspecoesParaEnvio);
                _listExcecao.Add($"Erro Download durante Inspeção automática. Exceção: {e.Message}");
                _enviar.EnviarEmailErroDownloadAutomatico(Configurations.EmailAdministrador(), _listExcecao);
                return false;
            }

        }
        #endregion


        private string[] ObterListaDiretoriosPorOrganismo(IntegracaoInfos ftpInfo)
        {
            List<string> listaDiretoriosValidos = new List<string>();
            try
            {
                //ftps ou ftp
                if (ftpInfo.TipoIntegracao == 1)
                {
                    var diretorios = _ftp.ObterListaDiretoriosInspecoesFtp(ftpInfo);
                    if (diretorios.Length > 0)
                    {
                        foreach (var item in diretorios)
                        {
                            var fileName = Path.GetFileNameWithoutExtension(item);
                            if (fileName.Length >= 4)
                            {
                                var fileExtension = Path.GetExtension(item);
                                if (fileExtension.Equals(".rar") || fileExtension.Equals(".zip"))
                                {
                                    listaDiretoriosValidos.Add(item);
                                }
                            }
                        }
                    }
                    return listaDiretoriosValidos.ToArray();
                }

                return _sftp.ObterArquivosNoDiretorioRemotoSftp(ftpInfo);

            }
            catch (Exception e)
            {
                // string mensagem = string.Format(MensagemSistema.DiretoriosInvalidos, e.Message);
                _listExcecao.Add(e.Message);
                throw e;
            }
        }


        private void DownloadInspecaoAutomatica(IntegracaoInfos ftpInfo, IEnumerable<string> diretorios)
        {
            var diretorioLocal = string.Empty;
            var diretoriosValidos = new List<string>();
            var inspecao = new InspecaoCsvModel();
            if (diretorios != null)
            {
                diretoriosValidos = ObterSomenteDiretoriosValidos(diretorios);
            }
            foreach (var item in diretoriosValidos)
            {
                try
                {
                    diretorioLocal = ObterDiretorioLocal(ftpInfo.DiretorioInspecaoLocal, item);

                    _descompactar.ExcluirArquivoCasoExista(diretorioLocal, item);
                    DeletarDiretorioLocalInspecao(diretorioLocal);
                    if (!DownloadArquivo(item, diretorioLocal, ftpInfo)) continue;
                    if (!_descompactar.DescompactarArquivo(diretorioLocal, item)) continue;
                    inspecao = _csv.ObterDadosInspecao(diretorioLocal, ftpInfo);
                    if (inspecao.Excecao == null)
                    {
                        if (GravarInspecaoObtidaNoArquivoCsv(inspecao, diretorioLocal))
                             if (!GravarHistoricoDownload(item, "Rotina Automática")) continue;
                    }

                    ExcluirArquivoCompactadoECsv(diretorioLocal, item);
                    if (inspecao.Excecao != null)
                    {
                        _listExcecao.Add(inspecao.Excecao.Mensagem);
                    }
                    
                }
                catch (Exception e)
                {
                    if (inspecao.Excecao == null)
                    {
                        var erro = new InspecoesGravadasModelServico
                        {

                            Excecao = new ExcecaoService
                            {
                                ExisteExcecao = true,
                                Mensagem = string.Format(e.Message)
                            }

                        };

                        _listExcecao.Add(erro.Excecao.Mensagem);
                    }
                    else
                    {
                        _listExcecao.Add(e.Message);
                    }
                  
                }
            }

            if (_listExcecao.Any())
            {
                if (ftpInfo.TipoIntegracao == 1)
                {
                    _ftp.CreateDirectory(ftpInfo);
                 }
                else
                {
                    _sftp.CreateDirectory(ftpInfo);
                }
                EscreverArquivoDeLog(_listExcecao, ftpInfo);

            }

        }

        private void DownloadInspecao(IntegracaoInfos ftpInfo, string diretorioLocal, string diretorioRemoto, string usuario)
        {
            var inspecao = new InspecaoCsvModel();
            try
            {
                _descompactar.ExcluirArquivoCasoExista(diretorioLocal, diretorioRemoto);
                DeletarDiretorioLocalInspecao(diretorioLocal);
                if (!DownloadArquivo(diretorioRemoto, diretorioLocal, ftpInfo)) return;
                if (!_descompactar.DescompactarArquivo(diretorioLocal, diretorioRemoto)) return;
                inspecao = _csv.ObterDadosInspecao(diretorioLocal, ftpInfo);
                if (inspecao.Excecao == null)
                {
                    if (!GravarInspecaoObtidaNoArquivoCsv(inspecao, diretorioLocal)) return;
                    if (!GravarHistoricoDownload(diretorioRemoto, usuario)) return;
                    ExcluirArquivoCompactadoECsv(diretorioLocal, diretorioRemoto);
                }
                else
                {
                    ExcluirArquivoCompactadoECsv(diretorioLocal, diretorioRemoto);
                    DeletarDiretorioLocalInspecao(diretorioLocal);
                    _listExcecao.Add(inspecao.Excecao.Mensagem);
                }

               
                
            }
            catch (Exception e)
            {
                if (inspecao.Excecao == null)
                {
                    var erro = new InspecoesGravadasModelServico
                    {

                        Excecao = new ExcecaoService
                        {
                            ExisteExcecao = true,
                            Mensagem = string.Format(e.Message)
                        }

                    };

                    _listExcecao.Add(erro.Excecao.Mensagem);
                }



                _listExcecao.Add(e.InnerException.ToString());

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

        private bool GravarInspecaoObtidaNoArquivoCsv(InspecaoCsvModel inspecaoCsv, string diretorioLocal)
        {
            if (inspecaoCsv.Excecao != null && inspecaoCsv.Excecao.ExisteExcecao)
            {
                _listExcecao.Add(inspecaoCsv.Excecao.Mensagem);
                DeletarDiretorioLocalInspecao(diretorioLocal);
                return false;
            }
            var inspecao = Conversao.ConverterParaModeloServico(inspecaoCsv);

            var result = AddInspecao(Conversao.ConverterParaDominio(inspecao));

            if (result)
            {
                _listaInspecoesParaEnvio.Add(inspecao);
            }

            return result;
        }

        private bool AddInspecao(Inspecao inspecao)
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

        private void EscreverArquivoDeLog(List<string> excecoes, IntegracaoInfos info)
        {
            string filePhisical = Configurations.DiretorioRaiz();
            string fileName = ".txt";
            var date = DateTime.Now.ToString("yyyy-MM-dd_HH-mm", CultureInfo.InvariantCulture);

            var path = filePhisical + "Log-SCIPP -"+info.DiretorioInspecaoLocal+"-"+ date + fileName;
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(path))
            {
                foreach (string line in excecoes)
                {
                    
                        file.WriteLine(line);
                    
                }
                file.Close();
                if (info.TipoIntegracao == 1)
                {
                    _ftp.UploadFile(path, info);
                }
                else
                {
                    _sftp.UploadFile(path, info);
                }
               
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

        private string CriarArquivoDeLog(List<string> erros)
        {
            var dataLog = DateTime.Now.ToString("yyyy-MM-dd_HH-mm", CultureInfo.InvariantCulture);
            string diretorioTemporario = "D:\\LOG_Aplicacao\\SCIPP\\";
            string fileName = diretorioTemporario + "Log" + dataLog + ".txt";

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


        private string ObterDiretorioLocal(string localDiretorio, string fileCipp)
        {
            var path = string.Empty;
            try
            {
                var cippServe = Path.GetFileNameWithoutExtension(fileCipp);

                path = Configurations.DiretorioRaiz() + localDiretorio + "\\" + cippServe + "\\";
                bool folderExists = Directory.Exists(path);

                if (!folderExists)
                {
                    Directory.CreateDirectory(path);
                    return path;
                }
                return path;
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao criar diretório no servidor {path} arquivo. Exceção {e.InnerException}");

            }

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
                _enviar.EnviarEmail(Configurations.EmailAdministrador(), _listExcecao, "");
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
                var value = Path.GetFileNameWithoutExtension(item);
                int checkNum;
                
                if (int.TryParse(value, out checkNum))
                { 

                    var extension = Path.GetExtension(item);
                    if (extension != null && (extension.Equals(".zip") || extension.Equals(".rar")))
                    {

                        diretoriosValidos.Add(item);
                    }
                }

            }

            return diretoriosValidos;

        }

        private void DeletarDiretorioLocalInspecao(string diretorioLocal)
        {
            var di = new DirectoryInfo(diretorioLocal);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();

            }

        }





    }

}