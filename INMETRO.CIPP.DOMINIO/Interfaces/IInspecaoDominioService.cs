using System.Collections.Generic;
using INMETRO.CIPP.DOMINIO.Modelos;

namespace INMETRO.CIPP.DOMINIO.Interfaces
{
    public interface IInspecaoDominioService
    {
        bool AdicionarDadosInspecao(Inspecao inspecao);

        Inspecao ObterDadosInspecaoPorCipp(string cipp);

        bool TemInspecao(string cipp);
        Inspecao ObterInspecaoParaCippECodigoOiaInformado(string codigoOia,string cipp);

        IEnumerable<Inspecao> ObterInspecaoPorCodigoOia(string codigoOia);
        IEnumerable<Inspecao> ObterTodasInspecoes();

    }
}