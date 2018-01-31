using INMETRO.CIPP.DOMINIO.Modelos;

namespace INMETRO.CIPP.DOMINIO.Interfaces
{
    public interface IInspecao
    {
        bool AdicionarDadosInspecao(Inspecao inspecao);
        Inspecao ObterDadosInspecao(string Cipp);

    }
}