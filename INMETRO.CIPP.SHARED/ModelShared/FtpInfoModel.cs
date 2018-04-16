using Renci.SshNet;

namespace INMETRO.CIPP.SHARED.ModelShared
{
    public class FtpInfoModel
    {
        public string LocalPath { get; set; }
        // Load the private key from the local HD (actual key included with this code)
        public PrivateKeyFile PrivateKey { get; set; }
        public string HostURI { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }


        public FtpInfoModel()
        {
            LocalPath = "";    // @"C:\FLP\Downloads\";
            // PrivateKey = new PrivateKeyFile(@"C:\SSH Keys\FLP_private.key", "deliveryhawaii");
            HostURI = "";      // "dori.flpi.com";    // Just keep swimming, just keep swimming...
            Usuario = "";
        }

        public FtpInfoModel(string localPath, string hostUri, string usuario, string privateKeyPath, string privateKeyPassword)
        {
            LocalPath = localPath;
            PrivateKey = new PrivateKeyFile(privateKeyPath, privateKeyPassword); 
            HostURI = hostUri;
            Usuario = usuario;
        }

        public FtpInfoModel(string localPath, string hostUri, string usuario, string senha)
        {
            LocalPath = localPath;
            HostURI = hostUri;
            Usuario = usuario;
            Senha = senha;
        }
    }

   

}