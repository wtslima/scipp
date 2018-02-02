using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using INMETRO.CIPP.DOMINIO.Interfaces;
using INMETRO.CIPP.DOMINIO.Interfaces.Repositorios;
using INMETRO.CIPP.DOMINIO.Modelos;
using INMETRO.CIPP.SERVICOS.Interfaces;
using INMETRO.CIPP.SHARED.Interfaces;

namespace INMETRO.CIPP.SERVICOS.Servicos
{
    public class DownloadServico : IDownloadServico
    {
        private readonly IOrganismoRepositorio _organismoRepositorio;
        private readonly IGerenciarFtp _ftp;
        private readonly IGerenciarArquivoCompactado _descompactar;
        private readonly string _pathLocal = ConfigurationManager.AppSettings["LocalPath"];
        private readonly IGerenciarCsv _csv;
        private readonly IInspecao _inspecaoServico;

        public DownloadServico(IOrganismoRepositorio organismoRepositorio, IGerenciarFtp ftp, IGerenciarArquivoCompactado descompactar, IGerenciarCsv csv, IInspecao inspecaoServico)
        {
            _organismoRepositorio = organismoRepositorio;
            _ftp = ftp;
            _descompactar = descompactar;
            _csv = csv;
            _inspecaoServico = inspecaoServico;
        }


        #region Download Por CIPP

        public bool DownloadInspecao(string codigoOIA, string cipp)
        {
            try
            {
                var organismo = _organismoRepositorio.BuscarOrganismoPorId(codigoOIA);

                if (organismo?.FtpInfo == null) return false;

                var diretoriosCipp =    ObterListaDiretoriosPorOrganismoAsync(organismo).Result;

                foreach (var cippRemoto in diretoriosCipp)
                {
                    if (TemCipp(cipp, cippRemoto))
                    {
                        DownloadInspecao(organismo, cipp);
                        break;
                    }

                    if (string.IsNullOrWhiteSpace(cipp))
                    {
                        DownloadInspecao(organismo, cippRemoto);
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

                if (organismos.Count <= 0) return false;

                foreach (var item in organismos)
                {
                    if (item.FtpInfo != null)
                    {

                        var diretorios = await ObterListaDiretoriosPorOrganismoAsync(item);
                        if (diretorios.Length > 0)
                        {
                            await DownloadInspecaoAutomatica(item, diretorios);
                        }
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

        #region Excluir Inspecao Automatica

        public async Task<bool> ExcluirInspecaoPorRotinaAutomatica()
        {
            try
            {
                var organismos = await _organismoRepositorio.BuscarTodosOrganismos();
                if (organismos.Count <= 0) return false;
                foreach (var item in organismos)
                {
                    var lista = ObterInspecoesComMaisDeTrintaDias(item);

                    if (lista.Count > 0)
                    {
                        RemoverInspecaoComMaisDe30Dias(lista.ToList(), item);
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



        private async Task<string[]> ObterListaDiretoriosPorOrganismoAsync(Organismo organismo)
        {
            try
            {
                string[] diretorios = { };
                var resultado =  _ftp.ObterListaDiretoriosInspecoesFtp(organismo.FtpInfo);
                return resultado ?? diretorios;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }


        private async Task<bool> DownloadInspecaoAutomatica(Organismo organismo, IList<string> diretorios)
        {
            if (diretorios.Count <= 0) return false;
            foreach (var item in diretorios)
            {
                var diretorioLocal = ObterDiretorioLocal(organismo.FtpInfo.DiretorioInspecaoLocal, item);
                if (_ftp.DownloadInspecaoFtp(item, diretorioLocal, organismo.FtpInfo))
                {
                    if (_descompactar.DescompactarArquivo(diretorioLocal, item))
                    {
                        var inspecao = _csv.ObterDadosInspecao(diretorioLocal);
                        if (!_inspecaoServico.BuscarInspecaoPorCodigoCipp(inspecao.CodigoCIPP))
                            GravarInspecao(inspecao);

                        ExcluirArquivoCompactadoECsv(diretorioLocal, item);
                    }
                }
            }
            return true;
        }


        private void DownloadInspecao(Organismo organismo, string file)
        {
            try
            {
                var diretorioLocal = ObterDiretorioLocal(organismo.FtpInfo.DiretorioInspecaoLocal, file);

                if (_ftp.DownloadInspecaoFtp(file, diretorioLocal, organismo.FtpInfo))
                {
                    if (_descompactar.DescompactarArquivo(diretorioLocal, file))
                    {
                        if (GravarInspecao(_csv.ObterDadosInspecao(diretorioLocal)))
                        {
                            ExcluirArquivoCompactadoECsv(diretorioLocal, file);
                        }
                    }
                }
                
                }
            catch (Exception e)
            {
                throw e;
            }

        }

        private bool ExcluirArquivoCompactadoECsv(string diretorioLocal, string file)
        {
            try
            {
                if (!_descompactar.ExcluirArquivoLocal(diretorioLocal, file)) return false;
                if (!_csv.ExcluirArquivoCippCsv(diretorioLocal)) return false;
                return true;

            }
            catch (Exception e)
            {

                throw e;
            }

        }

        private bool TemCipp(string cipp, string cippServer)
        {
            var cippServe = Path.GetFileNameWithoutExtension(cippServer);
            bool Combinam = cipp.Equals(cippServe);
            if (!string.IsNullOrWhiteSpace(cipp) && Combinam)
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


        private bool GravarInspecao(Inspecao inspecao)
        {
            try
            {
                var resultado = _inspecaoServico.AdicionarDadosInspecao(inspecao);
                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public IList<string> ObterInspecoesComMaisDeTrintaDias(Organismo organismo)
        {
            string[] inspecoesDiretorios = { };
            var listaDiretoriosParaExclusaoComMais30Dias = new List<string>();

            if (organismo.FtpInfo != null)
            {
                inspecoesDiretorios = _ftp.ObterListaDiretoriosInspecoesFtp(organismo.FtpInfo);
            }


            if (inspecoesDiretorios.Length <= 0) return new List<string>();

            foreach (var inspecao in inspecoesDiretorios)
            {
                var fileDate = _ftp.ObterDataEntradaFtp(organismo.FtpInfo, inspecao);
                if (TemMaisDe30Dias(fileDate))
                {
                    listaDiretoriosParaExclusaoComMais30Dias.Add(inspecao);
                }

            }

            return listaDiretoriosParaExclusaoComMais30Dias;

        }

        public bool TemMaisDe30Dias(DateTime fileDate)
        {
            var limite = fileDate.AddDays(30);
            if (DateTime.Now > limite)
            {
                return true;
            }
            return false;

        }

        public bool RemoverInspecaoComMaisDe30Dias(List<string> lista, Organismo organismo)
        {
            try
            {
                foreach (var item in lista)
                {
                    _ftp.ExcluirInspecao(organismo.FtpInfo, item);
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}