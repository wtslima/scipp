using System;
using System.Collections.Generic;
using System.IO;
using INMETRO.CIPP.DOMINIO.Modelos;
using INMETRO.CIPP.SHARED.Interfaces;
using Renci.SshNet;
using Renci.SshNet.Sftp;

namespace INMETRO.CIPP.SHARED.Servicos
{
    public class GerenciarSftp : IGerenciarSftp
    {
        public string[] ObterArquvosNoDiretorioRemotoSftp(FTPInfo sftp)
        {
            var tmpFiles = new List<string>();
            int Port = 22;
            try
            {
               

                SftpClient tmpClient;

               
                if (!string.IsNullOrEmpty(sftp.Senha))
                {
                    tmpClient = new SftpClient(sftp.HostURI,Port, sftp.Usuario, sftp.Senha);
                   
                }
                else
                {
                    
                    tmpClient = new SftpClient(sftp.HostURI, Port, sftp.Usuario, sftp.ChavePrivada);
                }

                // was new SftpClient(this.HostURI, this.UserName, this.PrivateKey)
                using (SftpClient client = tmpClient)
                {
                    client.Connect();

                    // was "/out"
                    IEnumerable<Renci.SshNet.Sftp.SftpFile> results = client.ListDirectory(sftp.DiretorioInspecao);
                    foreach (var file in results)
                    {
                        if (!file.IsDirectory)
                        {
                            tmpFiles.Add(file.Name);
                        }
                    }

                    client.Disconnect();
                }
                return tmpFiles.Count > 0 ? tmpFiles.ToArray() : new string[] { };
               
            }
            catch (Exception e)
            {

                throw new Exception($"Erro ao Obter Inspeções no servidor SFTP para o Organismo {sftp.OrganismoId}, Exceção :"+e.Message);
            }
            
        }

        public bool DownloadArquivo(string file, string diretorioLocal, FTPInfo sftpInfo)
        {
            bool success = true;
            int Port = 22;
            try
            {
                SftpClient tmpClient;
                if (!string.IsNullOrEmpty(sftpInfo.Senha))
                {
                    tmpClient = new SftpClient(sftpInfo.HostURI, Port,sftpInfo.Usuario, sftpInfo.Senha);
                }
                else
                {
                    tmpClient = new SftpClient(sftpInfo.HostURI, Port, sftpInfo.Usuario, sftpInfo.ChavePrivada);
                }

                // was new SftpClient(this.HostURI, this.UserName, this.PrivateKey)
                using (SftpClient client = tmpClient)
                {
                    client.Connect();

                    using (FileStream outputStream = new FileStream(diretorioLocal, FileMode.Create))
                    {
                        client.DownloadFile(sftpInfo.DiretorioInspecao + file, outputStream);
                    }

                    client.Disconnect();
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao fazer Download das Inspeções no servidor FTP para o Organismo {sftpInfo.OrganismoId}, Exceção :" + e.Message);
               
            }
            return success;
        }

        //// They want us to delete the files that we've successfully processed. Use this.
        public bool DeleteFile( string file, string diretorioLocal, FTPInfo sftpInfo)
        {
            bool success = true;
            try
            {
                SftpClient tmpClient;
                if (sftpInfo.Senha != "")
                {
                    tmpClient = new SftpClient(sftpInfo.HostURI, sftpInfo.Usuario, sftpInfo.Senha);
                }
                else
                {
                    tmpClient = new SftpClient(sftpInfo.HostURI, sftpInfo.Usuario, sftpInfo.ChavePrivada);
                }

                // was new SftpClient(this.HostURI, this.UserName, this.PrivateKey)
                using (SftpClient client = tmpClient)
                {
                    client.Connect();

                    client.DeleteFile(diretorioLocal + file);
                    

                    client.Disconnect();
                }
            }
            catch
            {
                success = false;
            }
            return success;
        }


        public bool UploadFile(string localFilePath, string remoteFilePath, FTPInfo info)
        {
            bool success = true;
            try
            {
                SftpClient tmpClient;
                if (info.Senha != "")
                {
                    tmpClient = new SftpClient(info.HostURI, info.Usuario, info.Senha);
                }
                else
                {
                    //todo: privateKey
                    tmpClient = new SftpClient(info.HostURI, info.Usuario, info.ChavePrivada);
                }

                // was new SftpClient(this.HostURI, this.UserName, this.PrivateKey)
                using (SftpClient client = tmpClient)
                {
                    client.Connect();

                    using (FileStream localFile = new FileStream(localFilePath, FileMode.Open))
                    {
                        client.UploadFile(localFile, remoteFilePath, true);
                    }

                    client.Disconnect();
                }
            }
            catch
            {
                success = false;
            }
            return success;
        }


        //public bool UploadFiles(string[] localFilePaths, string remoteFilePath)
        //{
        //    bool success = true;
        //    try
        //    {
        //        SftpClient tmpClient;
        //        if (this.Password != "")
        //        {
        //            tmpClient = new SftpClient(this.HostURI, this.UserName, this.Password);
        //        }
        //        else
        //        {
        //            tmpClient = new SftpClient(this.HostURI, this.UserName, this.PrivateKey);
        //        }

        //        // was new SftpClient(this.HostURI, this.UserName, this.PrivateKey)
        //        using (SftpClient client = tmpClient)
        //        {
        //            client.Connect();

        //            foreach (var localFilePath in localFilePaths)
        //            {
        //                using (FileStream localFile = new FileStream(localFilePath, FileMode.Open))
        //                {
        //                    client.UploadFile(localFile, remoteFilePath, true);
        //                }
        //            }

        //            client.Disconnect();
        //        }
        //    }
        //    catch
        //    {
        //        success = false;
        //    }
        //    return success;
        //}


        //// They want us to move the files that we've successfully processed. Use this.
        //// This SFTP library doesn't have a Move function so we are jury rigging one.
        //public bool MoveFile(string localFileName, string remoteFromPath, string remoteToPath)
        //{
        //    bool success = true;
        //    try
        //    {
        //        SftpClient tmpClient;
        //        if (this.Password != "")
        //        {
        //            tmpClient = new SftpClient(this.HostURI, this.UserName, this.Password);
        //        }
        //        else
        //        {
        //            tmpClient = new SftpClient(this.HostURI, this.UserName, this.PrivateKey);
        //        }

        //        // was new SftpClient(this.HostURI, this.UserName, this.PrivateKey)
        //        using (SftpClient client = tmpClient)
        //        {
        //            client.Connect();

        //            using (FileStream tmpFile = new FileStream(localFileName, FileMode.Open))
        //            {
        //                client.UploadFile(tmpFile, remoteToPath);
        //            }
        //            client.DeleteFile(remoteFromPath);

        //            client.Disconnect();
        //        }
        //    }
        //    catch
        //    {
        //        success = false;
        //    }
        //    return success;
        //}
    }
}