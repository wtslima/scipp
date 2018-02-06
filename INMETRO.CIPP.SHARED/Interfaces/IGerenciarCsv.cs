using INMETRO.CIPP.DOMINIO.Modelos;
using INMETRO.CIPP.SHARED.ModelShared;

namespace INMETRO.CIPP.SHARED.Interfaces
{
    public interface IGerenciarCsv
    {
        InspecaoCsvModel ObterDadosInspecao(string diretorio);

        bool ExcluirArquivoCippCsv(string diretorio);
    }
}