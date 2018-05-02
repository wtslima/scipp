using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using INMETRO.CIPP.DOMINIO.Modelos;
using INMETRO.CIPP.SHARED.Interfaces;
using INMETRO.CIPP.SHARED.ModelShared;
using Renci.SshNet;
using Renci.SshNet.Common;
using Renci.SshNet.Sftp;

namespace INMETRO.CIPP.SHARED.Servicos
{
    public class GerenciarSftp : IGerenciarSftp
    {
        public string[] ObterArquivosNoDiretorioRemotoSftp(IntegracaoInfos sftp)
        {
            var tmpFiles = new List<string>();
            var porta = Convert.ToInt32(sftp.Porta);
            try
            {
                IPAddress ips;

                ips = Dns.GetHostAddresses(sftp.HostURI).FirstOrDefault();

                var host = ips;

                SftpClient tmpClient;


                if (!string.IsNullOrEmpty(sftp.Senha))
                {
                    tmpClient = new SftpClient(sftp.HostURI, porta, sftp.Usuario, sftp.Senha);

                }
                else
                {

                    var sftpModelShared = new SftpModel(sftp.DiretorioInspecaoLocal, sftp.HostURI, sftp.Usuario,
                        sftp.PrivateKey, sftp.Senha);
                    tmpClient = new SftpClient(sftpModelShared.HostURI, porta, sftp.Usuario, sftpModelShared.PrivateKey);

                }

                // was new SftpClient(this.HostURI, this.UserName, this.PrivateKey)
                using (SftpClient client = tmpClient)
                {
                    client.Connect();

                    // was "/out"
                    IEnumerable<SftpFile> results = client.ListDirectory("//"+sftp.DiretorioInspecao+"//");
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

                throw new Exception($"Erro ao Obter Inspeções no servidor SFTP para o Organismo , Exceção :" + e.Message);
            }

        }

        public bool DownloadArquivo(string file, string diretorioLocal, IntegracaoInfos sftpInfo)
        {
            bool success = true;
            var port = Convert.ToInt32(sftpInfo.Porta);
            try
            {
                SftpClient tmpClient;
                if (!string.IsNullOrEmpty(sftpInfo.Senha))
                {
                    tmpClient = new SftpClient(sftpInfo.HostURI, port, sftpInfo.Usuario, sftpInfo.Senha);
                }
                else
                {
                    var sftpModelShared = new SftpModel(sftpInfo.DiretorioInspecaoLocal, sftpInfo.HostURI, sftpInfo.Usuario,
                        sftpInfo.PrivateKey, sftpInfo.Senha);
                    tmpClient = new SftpClient(sftpModelShared.HostURI, port, sftpModelShared.Usuario, sftpModelShared.PrivateKey);
                }

                // was new SftpClient(this.HostURI, this.UserName, this.PrivateKey)
                using (SftpClient client = tmpClient)
                {
                    client.Connect();

                    using (FileStream outputStream = new FileStream(diretorioLocal, FileMode.Create))
                    {
                        client.DownloadFile("/"+sftpInfo.DiretorioInspecao+"/"+ file, outputStream);
                    }

                    client.Disconnect();
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao fazer Download das Inspeções no servidor FTP para o Organismo, Exceção :" + e.Message);

            }
            return success;
        }

        //// They want us to delete the files that we've successfully processed. Use this.
        public bool DeleteFile(string file, string diretorioLocal, IntegracaoInfos sftpInfo)
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
                    var sftpModelShared = new SftpModel(sftpInfo.DiretorioInspecaoLocal, sftpInfo.HostURI, sftpInfo.Usuario,
                        sftpInfo.PrivateKey, sftpInfo.Senha);
                    tmpClient = new SftpClient(sftpModelShared.HostURI, sftpModelShared.Usuario, sftpModelShared.PrivateKey);
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

        public bool UploadFile(string localFilePath, IntegracaoInfos sftp)

        {
            bool success = true;
            try
            {

                SftpClient tmpClient;
                if (sftp.Senha != "")
                {
                    tmpClient = new SftpClient(sftp.HostURI, sftp.Usuario, sftp.Senha);
                }
                else
                {
                    //todo: privateKey
                    tmpClient = new SftpClient(sftp.HostURI, sftp.Usuario, sftp.PrivateKey);
                }

                // was new SftpClient(this.HostURI, this.UserName, this.PrivateKey)
                using (SftpClient client = tmpClient)
                {
                    client.Connect();

                    using (FileStream localFile = new FileStream(localFilePath, FileMode.Open))
                    {
                        client.UploadFile(localFile, sftp.DiretorioInspecao + "Log", true);
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

        public void CreateDirectory(IntegracaoInfos sftpInfo)
        {

            bool success = true;
            int Port = 22;
            string path = "Log";

            SftpClient tmpClient;
                if (!string.IsNullOrEmpty(sftpInfo.Senha))
                {
                    tmpClient = new SftpClient(sftpInfo.HostURI, Port, sftpInfo.Usuario, sftpInfo.Senha);
                }
                else
                {
                    var sftpModelShared = new SftpModel(sftpInfo.DiretorioInspecaoLocal, sftpInfo.HostURI,
                        sftpInfo.Usuario,
                        sftpInfo.PrivateKey, sftpInfo.Senha);
                    tmpClient = new SftpClient(sftpModelShared.HostURI, Port, sftpModelShared.Usuario,
                        sftpModelShared.PrivateKey);
                }

                string current = sftpInfo.DiretorioInspecao + path;
                using (SftpClient client = tmpClient)
                {
                    try
                    {

                        client.Connect();

                        SftpFileAttributes attrs = client.GetAttributes(current);
                        if (!attrs.IsDirectory)
                        {
                            throw new Exception("not directory");
                        }
                        client.CreateDirectory(current);


                      
                    }

                    catch (SftpPathNotFoundException)
                    {
                        client.CreateDirectory(current);
                    }

                    client.Disconnect();
                }
            
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