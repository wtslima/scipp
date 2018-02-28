using INMETRO.CIPP.DOMINIO.Modelos;

namespace INMETRO.CIPP.DOMINIO.Interfaces
{
    public interface IInspecaoDominioService
    {
        bool AdicionarDadosInspecao(Inspecao inspecao);

        Inspecao ObterDadosInspecaoPorCipp(string cipp);

        bool TemInspecao(string cipp);

        Inspecao ObterInspecaoParaCippECodigoOiaInformado(string codigoOia,string cipp);

        InspecoesGravadas ObterInspecaoPorCodigoOia(string codigoOia);

        InspecoesGravadas ObterTodasInspecoes();

    }
}