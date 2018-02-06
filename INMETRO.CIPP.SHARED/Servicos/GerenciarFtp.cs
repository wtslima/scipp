using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using INMETRO.CIPP.DOMINIO.Modelos;
using INMETRO.CIPP.SHARED.Interfaces;

namespace INMETRO.CIPP.SHARED.Servicos
{
    public class GerenciarFtp : IGerenciarFtp
    {
        public  string[] ObterListaDiretoriosInspecoesFtp(FTPInfo ftp)
        {
            var tmpFiles = new List<string>();
            try
            {
                var request = (FtpWebRequest)WebRequest.Create(ftp.HostURI + ftp.DiretorioInspecao);

                request.Method = WebRequestMethods.Ftp.ListDirectory; // Details
                request.Credentials = new NetworkCredential(ftp.Usuario, ftp.Senha);

                using (var response = (FtpWebResponse)request.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    {

                        using (var reader = new StreamReader(stream, true))
                        {
                            while (!reader.EndOfStream)
                            {

                                tmpFiles.Add(reader.ReadLine());
                            }
                        }
                    }
                }
                return tmpFiles.Count > 0 ? tmpFiles.ToArray() : new string[] { };
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public bool DownloadInspecaoFtp(string file, string diretorioLocal, FTPInfo ftpInfo)
        {
            bool success = true;
            try
            {
                string localPath = diretorioLocal + file;
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpInfo.HostURI + ftpInfo.DiretorioInspecao + file);
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential(ftpInfo.Usuario, ftpInfo.Senha);
                request.UseBinary = true;

                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    using (Stream rs = response.GetResponseStream())
                    {
                        //todo:Colocar Local Inspecao
                        using (FileStream ws = new FileStream(localPath, FileMode.Create))
                        {
                            byte[] buffer = new byte[2048];
                            int bytesRead = rs.Read(buffer, 0, buffer.Length);

                            while (bytesRead > 0)
                            {
                                ws.Write(buffer, 0, bytesRead);
                                bytesRead = rs.Read(buffer, 0, buffer.Length);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                success = false;
            }

            return success;
        }

        public long ObterTamanhoArquivoInspecao(FTPInfo ftpInfo)
        {
            // Get the object used to communicate with the server.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpInfo.HostURI + ftpInfo.DiretorioInspecao);
            request.Method = WebRequestMethods.Ftp.GetFileSize;

            // Get network credentials.
            request.Credentials = new NetworkCredential(ftpInfo.Usuario, ftpInfo.Senha);

            try
            {
                using (FtpWebResponse response =
                    (FtpWebResponse)request.GetResponse())
                {
                    // Return the size.
                    return response.ContentLength;
                }
            }
            catch (Exception ex)
            {
                // If the file doesn't exist, return -1.
                // Otherwise rethrow the error.
                if (ex.Message.Contains("Arquivo de Inspeção não disponível.")) return -1;
                throw;
            }
        }

        public DateTime ObterDataEntradaFtp(FTPInfo ftpInfo, string diretorioInspecaoRemoto)
        {
            // Get the object used to communicate with the server.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpInfo.HostURI + ftpInfo.DiretorioInspecao + diretorioInspecaoRemoto);
            request.Method = WebRequestMethods.Ftp.GetDateTimestamp;

            // Get network credentials.
            request.Credentials =
                new NetworkCredential(ftpInfo.Usuario, ftpInfo.Senha);

            try
            {
                using (FtpWebResponse response =
                    (FtpWebResponse)request.GetResponse())
                {
                    // Return the size.
                    return response.LastModified;
                }
            }
            catch (Exception ex)
            {
                // If the file doesn't exist, return Jan 1, 3000.
                // Otherwise rethrow the error.
                if (ex.Message.Contains("Arquivo de Inspeção não disponível."))
                    return new DateTime(3000, 1, 1);
                throw;
            }
        }

        public bool ExcluirInspecao(FTPInfo ftp, string inspecao)
        {
            bool success = true;
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftp.HostURI + ftp.DiretorioInspecao + inspecao);
                request.Credentials = new NetworkCredential(ftp.Usuario, ftp.Senha);
                request.Method = WebRequestMethods.Ftp.DeleteFile;

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                //Console.WriteLine("Delete status: {0}", response.StatusDescription);
                response.Close();
            }
            catch (Exception e)
            {
                success = false;
            }
            return success;
        }


    }
}
