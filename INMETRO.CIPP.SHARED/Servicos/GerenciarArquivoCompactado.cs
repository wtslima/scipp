using System;
using System.IO;
using System.IO.Compression;
using INMETRO.CIPP.SHARED.Interfaces;
using NUnrar.Archive;
using NUnrar.Common;

namespace INMETRO.CIPP.SHARED.Servicos
{
    public class GerenciarArquivoCompactado : IGerenciarArquivoCompactado
    {

        public bool DescompactarArquivo(string diretorio, string file)
        {
            string fileExt = Path.GetExtension(file);
            try
            {
                if (fileExt != null && !fileExt.Equals(".rar"))
                {
                    ExtrairZip(diretorio, file);
                    return true;
                }
                ExtrairRar(diretorio, file);
                return true;
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public bool ExcluirArquivoLocal(string diretorio, string file)
        {
            try
            {
                string fileExtensao = Path.GetExtension(file);
                if (fileExtensao != null && !fileExtensao.Equals(".rar"))
                {
                    string fileNamePath = diretorio + file;
                    if (File.Exists(fileNamePath))
                    {
                        File.Delete(fileNamePath);
                        return true;
                    }
                }

            }
            catch (Exception e)
            {

                throw e;
            }

            return false;
        }

        public void ExcluirArquivoCasoExista(string diretorio, string file)
        {
            try
            {
                string fileExtensao = Path.GetExtension(file);
                if (fileExtensao != null)
                {
                    string fileNamePath = diretorio + file;
                    if (File.Exists(fileNamePath))
                    {
                        File.Delete(fileNamePath);
                       
                    }
                }

            }
            catch (Exception e)
            {

                throw e;
            }
        }

        private static void ExtrairZip(string diretorio, string file)
        {

            string fileNamePath = diretorio + file;
            try
            {
                using (ZipArchive archive = ZipFile.OpenRead(fileNamePath))
                {

                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {

                        if (entry.FullName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
                        {
                            entry.ExtractToFile(Path.Combine(diretorio, entry.FullName));
                            //todo: Salvar csv
                        }
                        if (entry.FullName.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase))
                        {
                            entry.ExtractToFile(Path.Combine(diretorio, entry.FullName));
                        }
                        if (entry.FullName.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase))
                        {
                            entry.ExtractToFile(Path.Combine(diretorio, entry.FullName));
                        }


                    }
                }
            }
            catch (Exception e)
            {
                var arquivo = Path.GetFileNameWithoutExtension(file);
                throw new Exception(string.Format("Erro ao descompactar o arquivo {0} Zip. Arquivo corrompido.", arquivo));

            }


        }

        //todo: checar para fechar a conexao após extrair arquivos
        //todo: não está exluindo os arquivos com a conexão aberta
        private static void ExtrairRar(string diretorio, string file)
        {
            var fileNamePath = diretorio + file;


                 RarArchive archiveRar = RarArchive.Open(fileNamePath);

            try
            {
                if (archiveRar.Entries.Count <= 0)
                {
                    foreach (RarArchiveEntry entry in archiveRar.Entries)
                    {
                        try
                        {
                            var fileName = Path.GetFileName(entry.FilePath);
                            var rootToFile = Path.GetFullPath(entry.FilePath).Replace(fileName ?? throw new InvalidOperationException(), fileNamePath);

                            if (!Directory.Exists(diretorio))
                            {
                                Directory.CreateDirectory(rootToFile);
                            }
                            entry.WriteToFile(diretorio + fileName, ExtractOptions.ExtractFullPath | ExtractOptions.Overwrite);


                        }
                        catch (Exception e)
                        {
                            throw new Exception(string.Format(MensagemSistema.ErroDescompactarTipoRar, diretorio, file, e.Message));
                        }


                    }
                }
                throw new Exception(string.Format(MensagemSistema.ErroNoArquivoCompactadoTipoRar, diretorio, file));
            }
            catch(Exception e)
            {
                var arquivo = Path.GetFileNameWithoutExtension(file);
                throw new Exception(string.Format(MensagemSistema.ErroDescompactarTipoRar, arquivo, e.Message));
            }
        }

    }
}