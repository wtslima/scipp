using INMETRO.CIPP.DOMINIO.Modelos;

namespace INMETRO.CIPP.SHARED.Interfaces
{
    public interface IGerenciarCsv
    {
        Inspecao ObterDadosInspecao(string diretorio);

        bool ExcluirArquivoCippCsv(string diretorio);
    }
}