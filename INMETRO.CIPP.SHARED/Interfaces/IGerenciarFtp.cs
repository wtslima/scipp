using System;
using INMETRO.CIPP.DOMINIO.Modelos;

namespace INMETRO.CIPP.SHARED.Interfaces
{
    public interface IGerenciarFtp
    {
        string[] ObterListaDiretoriosInspecoesFtp(IntegracaoInfos ftpInfo);

        bool DownloadInspecaoFtp(string file, string diretorioLocal, IntegracaoInfos ftpInfo);

        long ObterTamanhoArquivoInspecao(IntegracaoInfos ftpInfo);

        DateTime ObterDataEntradaFtp(IntegracaoInfos ftpInfo, string diretorioInspecaoRemoto);

        bool ExcluirInspecao(IntegracaoInfos ftpInfo, string inspecao);

        void CreateDirectory(IntegracaoInfos ftpInfo);
        bool UploadFile(string path, IntegracaoInfos ftp);
    }
}