using System;
using INMETRO.CIPP.DOMINIO.Modelos;

namespace INMETRO.CIPP.SHARED.Interfaces
{
    public interface IGerenciarFtp
    {
        string[] ObterListaDiretoriosInspecoesFtp(FtpInfo ftpInfo);

        bool DownloadInspecaoFtp(string file, string diretorioLocal, FtpInfo ftpInfo);

        long ObterTamanhoArquivoInspecao(FtpInfo ftpInfo);

        DateTime ObterDataEntradaFtp(FtpInfo ftpInfo, string diretorioInspecaoRemoto);

        bool ExcluirInspecao(FtpInfo ftpInfo, string inspecao);
    }
}