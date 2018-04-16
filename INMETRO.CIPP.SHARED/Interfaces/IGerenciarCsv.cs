using System.Collections.Generic;
using INMETRO.CIPP.DOMINIO.Modelos;
using INMETRO.CIPP.SHARED.ModelShared;

namespace INMETRO.CIPP.SHARED.Interfaces
{
    public interface IGerenciarCsv
    {
        InspecaoCsvModel ObterDadosInspecao(string diretorio, FtpInfo ftpInfo);

        bool ExcluirArquivoCippCsv(string diretorio);

        string CriarArquivoInspecoesAnexo(IList<InspecaoCsvModel> inspecoes);
    }
}