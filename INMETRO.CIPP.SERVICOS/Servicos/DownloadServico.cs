using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using INMETRO.CIPP.DOMINIO.Interfaces;
using INMETRO.CIPP.DOMINIO.Interfaces.Repositorios;
using INMETRO.CIPP.DOMINIO.Modelos;
using INMETRO.CIPP.DOMINIO.Servicos;
using INMETRO.CIPP.INFRA.REPOSITORIO.Repositorios;
using INMETRO.CIPP.SERVICOS.Interfaces;
using INMETRO.CIPP.SERVICOS.ModelService;
using INMETRO.CIPP.SHARED.Interfaces;
using INMETRO.CIPP.SHARED.ModelShared;

namespace INMETRO.CIPP.SERVICOS.Servicos
{
    public class DownloadServico : IDownloadServico
    {
        private readonly IOrganismoRepositorio _organismoRepositorio;
        private readonly IGerenciarFtp _ftp;
        private readonly IGerenciarArquivoCompactado _descompactar;
        private readonly string _pathLocal = ConfigurationManager.AppSettings["LocalPath"];
        private readonly IGerenciarCsv _csv;
        private readonly IInspecaoDominioService _inspecaoServico;
        private readonly IHistorico _historicoServico;
        private readonly IHistoricoExclusao _historicoExclusaoServico;

        readonly List<InspecaoModelServico> _listaInspecoesParaEnvio = new List<InspecaoModelServico>();

        public DownloadServico(IOrganismoRepositorio organismoRepositorio, IGerenciarFtp ftp, IGerenciarArquivoCompactado descompactar, IGerenciarCsv csv, IInspecaoDominioService inspecaoServico)
        {
            _organismoRepositorio = organismoRepositorio;
            _ftp = ftp;
            _descompactar = descompactar;
            _csv = csv;
            _inspecaoServico = inspecaoServico;
            IHistoricoRepositorio historicoRepositorio = new HistoricoDownloadRepositorio();
            IHistoricoExclusaoRepositorio historicoExclusaoRepositorio = new HistoricoExclusaoRepositorio();
            _historicoServico = new HistoricoServico(historicoRepositorio);
            _historicoExclusaoServico = new HistoricoExclusaoServico(historicoExclusaoRepositorio);
        }

        
        #region Download Por CIPP

        public bool DownloadInspecaoPorUsuario(string codigoOia, string cipp)
        {
            try
            {
                var ftpInfos = _organismoRepositorio.BuscarOrganismoPorId(codigoOia).FtpInfo;

                if (ftpInfos == null) return false;

                var diretoriosCippRemoto = ObterListaDiretoriosPorOrganismo(ftpInfos);

                foreach (var diretorioCippRemoto in diretoriosCippRemoto)
                {

                    try
                    {
                        if (TemInspecaoValida(diretorioCippRemoto)) continue;
                        if (!TemCipp(cipp, diretorioCippRemoto)) continue;
                        var diretorioLocal = ObterDiretorioLocal(ftpInfos.DiretorioInspecaoLocal, diretorioCippRemoto);
                        DownloadInspecao(ftpInfos, diretorioLocal, diretorioCippRemoto);

                       
                        break;
                    }
                    catch (Exception e)
                    {
                        throw e;

                    }

                }

                return true;
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
                var organismos = await _organismoRepositorio.BuscarTodosOrganismos();

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
                        continue;

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

        #region Excluir Inspecao Automatica

        public async Task<bool> ExcluirInspecaoPorRotinaAutomatica()
        {
            try
            {
                var organismos = await _organismoRepositorio.BuscarTodosOrganismos();

                if (organismos.Count <= 0) return false;
                foreach (var item in organismos.GroupBy(c => c.FtpInfo))
                {
                    var lista = ObterInspecoesComMaisDeTrintaDias(item.Key);

                    if (lista.Count > 0)
                    {
                        RemoverInspecaoComMaisDe30Dias(lista.ToList(), item.Key);
                    }
                }
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
                var inspecao = new InspecaoModelServico();

                try
                {
                    if (TemInspecaoValida(item)) continue;
                    var diretorioLocal = ObterDiretorioLocal(ftpInfo.DiretorioInspecaoLocal, item);
                    if (!_ftp.DownloadInspecaoFtp(item, diretorioLocal, ftpInfo)) continue;
                    if (!_descompactar.DescompactarArquivo(diretorioLocal, item)) continue;
                    inspecao = Conversao.ConverterParaModeloServico(_csv.ObterDadosInspecao(diretorioLocal));
                    if (!GravarInspecao(Conversao.ConverterParaDominio(inspecao))) continue;
                    _listaInspecoesParaEnvio.Add(inspecao);
                    if (!GravarHistoricoDownload(item)) continue;
                    ExcluirArquivoCompactadoECsv(diretorioLocal, item);


                }
                catch (Exception e)
                {

                    continue;
                }

            }

           

        }


        private void DownloadInspecao(FTPInfo ftpInfo, string diretorioLocal, string diretorioRemoto)
        {
            try
            {

                if (!_ftp.DownloadInspecaoFtp(diretorioRemoto, diretorioLocal, ftpInfo)) return;
                if (!_descompactar.DescompactarArquivo(diretorioLocal, diretorioRemoto)) return;
                if (!GravarInspecao(Conversao.ConverterParaDominio(_csv.ObterDadosInspecao(diretorioLocal)))) return;
                if (!GravarHistoricoDownload(diretorioRemoto)) return;
                ExcluirArquivoCompactadoECsv(diretorioLocal, diretorioRemoto);
            }
            catch (Exception e)
            {
                throw e;
            }

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

        private bool GravarHistoricoDownload(string cipp)
        {
            var value = Path.GetFileNameWithoutExtension(cipp);
            var inspecao = _inspecaoServico.ObterDadosInspecao(value);
            if (inspecao.Id <= 0) return false;
            var historicoModel = new HistoricoServiceModel
            {
                IdInspecao = inspecao.Id,
                Usuario = "wtslima",
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
            bool combinam = cipp.Equals(cippServe);
            if (!string.IsNullOrWhiteSpace(cipp) && combinam)
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

        public IList<string> ObterInspecoesComMaisDeTrintaDias(FTPInfo ftpInfo)
        {
            var listaDiretoriosParaExclusaoComMais30Dias = new List<string>();

            var inspecoesDiretorios = _ftp.ObterListaDiretoriosInspecoesFtp(ftpInfo);


            if (inspecoesDiretorios.Length <= 0) return new List<string>();

            foreach (var inspecao in inspecoesDiretorios)
            {
                var dataDiretorioRemoto = _ftp.ObterDataEntradaFtp(ftpInfo, inspecao);
                if (!TemMaisDe30Dias(dataDiretorioRemoto)) continue;
                listaDiretoriosParaExclusaoComMais30Dias.Add(inspecao);
            }

            return listaDiretoriosParaExclusaoComMais30Dias;

        }

        public bool TemMaisDe30Dias(DateTime fileDate)
        {
            var limite = fileDate.AddDays(30);
            return DateTime.Now > limite;
        }

        public void RemoverInspecaoComMaisDe30Dias(List<string> lista, FTPInfo ftpInfo)
        {
            try
            {
                foreach (var item in lista)
                {
                    if (!_ftp.ExcluirInspecao(ftpInfo, item)) continue;
                    AddRegistrosExclusao(ftpInfo.Organismo.CodigoOIA, item);
                }

            }
            catch (Exception e)
            {

                throw e;
            }

        }

        private bool TemInspecaoValida(string cipp)
        {
            var inspecao = Path.GetFileNameWithoutExtension(cipp);

            return _inspecaoServico.TemInspecao(inspecao);

        }

        private void AddRegistrosExclusao(string codigoOia, string diretorio)
        {
            try
            {
                var registroExclusao = new HistoricoExclusaoServiceModel
                {
                    CodigoOia = codigoOia,
                    Cipp = diretorio,
                    DataExclusao = DateTime.Now
                };
                _historicoExclusaoServico.AdicionarRegistroDeHistoricoDeExclusao(
                    Conversao.ConverterParaDominio(registroExclusao));
            }
            catch (Exception e)
            {
                throw e;
            }
            
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


    }

}