using INMETRO.CIPP.DOMINIO.Modelos;

namespace INMETRO.CIPP.SHARED.Interfaces
{
    public interface IGerenciarSftp
    {
        string[] ObterArquvosNoDiretorioRemotoSftp(FTPInfo ftpInfo);
        bool DownloadArquivo(string file, string diretorioLocal, FTPInfo sftpInfo);
    }
}