using INMETRO.CIPP.DOMINIO.Modelos;

namespace INMETRO.CIPP.SHARED.Interfaces
{
    public interface IGerenciarSftp
    {
        string[] ObterArquivosNoDiretorioRemotoSftp(IntegracaoInfos sftp);
        bool DownloadArquivo(string file, string diretorioLocal, IntegracaoInfos sftpInfo);
        bool UploadFile(string file, IntegracaoInfos sftpInfo);
        void CreateDirectory(IntegracaoInfos sFtpInfo);
    }
}