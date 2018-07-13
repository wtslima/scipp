using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using FluentFTP;
using INMETRO.CIPP.DOMINIO.Modelos;
using INMETRO.CIPP.SHARED.Interfaces;

namespace INMETRO.CIPP.SHARED.Servicos
{
    public class GerenciarFtp : IGerenciarFtp
    {
        public  string[] ObterListaDiretoriosInspecoesFtp(IntegracaoInfos ftp)
        {
            var tmpFiles = new List<string>();
            try
            {
                var request = (FtpWebRequest)WebRequest.Create(ftp.HostURI.Trim()+":"+ftp.Porta.Trim()+"//"+ftp.DiretorioInspecao.Trim()+"//");

                request.Method = WebRequestMethods.Ftp.ListDirectory; 
                request.Credentials = new NetworkCredential(ftp.Usuario.Trim(), ftp.Senha.Trim());
              
               
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
            catch (FtpException e)
            {
                throw e;
            }

        }

        public bool DownloadInspecaoFtp(string file, string diretorioLocal, IntegracaoInfos ftpInfo)
        {
            bool success = true;
            try
            {
                string localPath = diretorioLocal + file;
                var host = ftpInfo.HostURI.Trim() + ":" + ftpInfo.Porta.Trim() +"//" +ftpInfo.DiretorioInspecao.Trim() + "//"+file;
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(host.Trim());
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential(ftpInfo.Usuario.Trim(), ftpInfo.Senha.Trim());
               
                
                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    
                    using (Stream rs = response.GetResponseStream())
                    {
                        //todo:Colocar Local Inspecao
                        using (FileStream ws = new FileStream(localPath.Trim(), FileMode.Create))
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

        public long ObterTamanhoArquivoInspecao(IntegracaoInfos ftpInfo)
        {
            // Get the object used to communicate with the server.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpInfo.HostURI + ftpInfo.DiretorioInspecao);
            request.Method = WebRequestMethods.Ftp.GetFileSize;

            // Get network credentials.
            request.Credentials = new NetworkCredential(ftpInfo.Usuario.Trim(), ftpInfo.Senha.Trim());

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

        public DateTime ObterDataEntradaFtp(IntegracaoInfos ftpInfo, string diretorioInspecaoRemoto)
        {
            // Get the object used to communicate with the server.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpInfo.HostURI + ftpInfo.DiretorioInspecao + diretorioInspecaoRemoto);
            request.Method = WebRequestMethods.Ftp.GetDateTimestamp;

            // Get network credentials.
            request.Credentials =
                new NetworkCredential(ftpInfo.Usuario.Trim(), ftpInfo.Senha.Trim());

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

        public bool ExcluirInspecao(IntegracaoInfos ftp, string inspecao)
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

        public bool UploadFile(string path)
        {
            bool success = true;
            try
            {
                var filename = Path.GetFileName(path);
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://184.168.109.66:2121/update/" + filename);
                request.Method = WebRequestMethods.Ftp.UploadFile;

                // This example assumes the FTP site uses anonymous logon.
                request.Credentials = new NetworkCredential("zervita_ftp", "zvt@22F1");

                //Copy the contents of the file to the request stream.

                StreamReader sourceStream = new StreamReader(path);
                byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
                sourceStream.Close();
                request.ContentLength = fileContents.Length;

                Stream requestStream = request.GetRequestStream();
                requestStream.Write(fileContents, 0, fileContents.Length);
                requestStream.Close();

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                Console.WriteLine("Upload File Complete, status {0}", response.StatusDescription);

                response.Close();
            }
            catch
            {
                success = false;
            }
            return success;
        }




    }
}
