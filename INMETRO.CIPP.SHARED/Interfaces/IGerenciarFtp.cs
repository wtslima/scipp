using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using INMETRO.CIPP.DOMINIO.Modelos;

namespace INMETRO.CIPP.SHARED.Interfaces
{
    public interface IGerenciarFtp
    {
        string[] ObterListaDiretoriosInspecoesFtp(FTPInfo ftpInfo);

        bool DownloadInspecaoFtp(string file, string diretorioLocal, FTPInfo ftpInfo);

        long ObterTamanhoArquivoInspecao(FTPInfo ftpInfo);

        DateTime ObterDataEntradaFtp(FTPInfo ftpInfo, string diretorioInspecaoRemoto);

        bool ExcluirInspecao(FTPInfo ftpInfo, string inspecao);
    }
}