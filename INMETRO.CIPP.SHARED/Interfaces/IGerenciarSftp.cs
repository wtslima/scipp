using INMETRO.CIPP.DOMINIO.Modelos;

namespace INMETRO.CIPP.SHARED.Interfaces
{
    public interface IGerenciarSftp
    {
        string[] ObterArquvosNoDiretorioRemotoSftp(FtpInfo sftp);
        bool DownloadArquivo(string file, string diretorioLocal, FtpInfo sftpInfo);
        bool UploadFile(string file, FtpInfo sftpInfo);
    }
}