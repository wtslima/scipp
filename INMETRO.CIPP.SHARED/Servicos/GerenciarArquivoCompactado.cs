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
                string fileExt = Path.GetExtension(file);
                if (!fileExt.Equals(".rar"))
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

        private void ExtrairZip(string diretorio, string file)
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
                
                throw e;
            }


        }

        private void ExtrairRar(string diretorio, string file)
        {
            string fileNamePath = diretorio + file;

            RarArchive ArchiveRar = RarArchive.Open(fileNamePath);
            
            foreach (RarArchiveEntry entry in ArchiveRar.Entries)
            {
                try
                {
                    string fileName = Path.GetFileName(entry.FilePath);
                    string rootToFile = Path.GetFullPath(entry.FilePath).Replace(fileName, fileNamePath);

                    if (!Directory.Exists(diretorio))
                    {
                        Directory.CreateDirectory(rootToFile);
                    }
                    entry.WriteToFile(diretorio + fileName, ExtractOptions.ExtractFullPath | ExtractOptions.Overwrite);
                    
                }
                catch (Exception e)
                {

                    throw e;
                }

                
            }
        }


    }
}