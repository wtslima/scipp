using INMETRO.CIPP.DOMINIO.Modelos;

namespace INMETRO.CIPP.SHARED.Interfaces
{
    public interface IGerenciarSftp
    {
        string[] ObterArquvosNoDiretorioRemotoSftp(FTPInfo sftp);
        bool DownloadArquivo(string file, string diretorioLocal, FTPInfo sftpInfo);
        bool UploadFile(string file, FTPInfo sftpInfo);
    }
}