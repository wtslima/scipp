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
        //private readonly IOrganismo _organismoServico;
        private readonly IGerenciarFtp _ftp;
        private readonly IGerenciarArquivoCompactado _descompactar;
        private readonly string _pathLocal = ConfigurationManager.AppSettings["LocalPath"];
        private readonly IGerenciarCsv _csv;
        private readonly IInspecao _inspecaoServico;

        public DownloadServico(IOrganismoRepositorio organismoRepositorio, IGerenciarFtp ftp, IGerenciarArquivoCompactado descompactar, IGerenciarCsv csv, IInspecao inspecaoServico)
        {
            _organismoRepositorio = organismoRepositorio;
            //_organismoServico = organismoServico;
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

                if (organismo != null)
                {
                    var diretoriosCipp = ObterListaDiretoriosPorOrganismo(organismo);

                    foreach (var cippRemoto in diretoriosCipp)
                    {
                        if (TemCipp(cipp, cippRemoto))
                        {
                            DownloadInspecao(organismo, cippRemoto);
                            break;
                        }

                        if (string.IsNullOrWhiteSpace(cipp))
                        {
                            DownloadInspecao(organismo, cippRemoto);
                        }

                    }

                    return true;
                }
                return false;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        #endregion

        #region Download Por Rotina Automatica
        public async Task<bool> DownloadInspecoesPorRotinaAutomatica()
        {
            var organismos = await _organismoRepositorio.BuscarTodosOrganismos();
            if (organismos.Count > 0)
            {
                foreach (var item in organismos)
                {
                    if (item.FtpInfo.OrganismoId > 0)
                    {

                        var diretorios = ObterListaDiretoriosPorOrganismo(item);
                        if (diretorios.Length > 0)
                        {
                            var res1 = DownloadInspecaoAutomatica(item, diretorios);
                        }
                    }

                }
            }
            return true;
        }
        #endregion

        #region Excluir Inspecao Automatica

        public async Task<bool> ExcluirInspecaoPorRotinaAutomatica()
        {
            try
            {
                var organismos = await _organismoRepositorio.BuscarTodosOrganismos();
                if (organismos.Count > 0)
                {
                    foreach (var item in organismos)
                    {
                        var lista = ObterInspecoesComMaisDeTrintaDias(item);
                        if (lista.Count > 0)
                        {
                            RemoverInspecaoComMaisDe30Dias(lista.ToList(), item);
                        }
                    }
                }
                return false;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        #endregion



        private string[] ObterListaDiretoriosPorOrganismo(Organismo organismo)
        {
            try
            {
                string[] diretorios = new string[] { };
                var resultado = _ftp.ObterListaDiretoriosInspecoesFtp(organismo.FtpInfo);
                return resultado ?? diretorios;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }


        private async Task<bool> DownloadInspecaoAutomatica(Organismo organismo, string[] diretorios)
        {
            if (diretorios.Length > 0)
            {

                foreach (var item in diretorios)
                {
                    var diretorioLocal = ObterDiretorioLocal(organismo.FtpInfo.DiretorioInspecaoLocal, item);
                    if (_ftp.DownloadInspecaoFtp(item, diretorioLocal, organismo.FtpInfo))
                    {
                        if (_descompactar.DescompactarArquivo(diretorioLocal, item))
                        {
                            GravarInspecao(_csv.ObterDadosInspecao(diretorioLocal));

                            ExcluirArquivoCompactadoECsv(diretorioLocal, item);
                        }
                    }
                }
                return true;
            }
            return false;
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
                        GravarInspecao(_csv.ObterDadosInspecao(diretorioLocal));

                        ExcluirArquivoCompactadoECsv(diretorioLocal, file);
                    }
                }

            }
            catch (Exception e)
            {

                throw e;
            }

        }

        private void ExcluirArquivoCompactadoECsv(string diretorioLocal, string file)
        {
            _descompactar.DescompactarArquivo(diretorioLocal, file);
            _csv.ExcluirArquivoCippCsv(diretorioLocal);
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
            var resultado = _inspecaoServico.AdicionarDadosInspecao(inspecao);
            return resultado;
        }

        public IList<string> ObterInspecoesComMaisDeTrintaDias(Organismo organismo)
        {
            string[] inspecoesDiretorios =  { };
            List<string> listaDiretoriosParaExclusao = new List<string>();
            if (organismo.FtpInfo != null)
            {
                inspecoesDiretorios = _ftp.ObterListaDiretoriosInspecoesFtp(organismo.FtpInfo);
            }


            if (
                inspecoesDiretorios.Length > 0)
            {
                foreach (var inspecao in inspecoesDiretorios)
                {
                    var fileDate = _ftp.ObterDataEntradaFtp(organismo.FtpInfo, inspecao);
                    if (TemMaisDe30Dias(fileDate))
                    {
                        listaDiretoriosParaExclusao.Add(inspecao);
                    }

                }

                return listaDiretoriosParaExclusao;
            }
            return new List<string>();
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