using System.Collections.Generic;
using INMETRO.CIPP.DOMINIO.Modelos;

namespace INMETRO.CIPP.DOMINIO.Interfaces
{
    public interface IInspecaoDominioService
    {
        bool AdicionarDadosInspecao(Inspecao inspecao);

        Inspecao ObterDadosInspecao(string cipp);

        bool TemInspecao(string cipp);
        bool ExisteCodigoOia(string codigoOia);

        IEnumerable<Inspecao> ObterInspecaoPorCodigoOia(string codigoOia);
        IEnumerable<Inspecao> ObterInspecaoPorPlacaLicenca(string placa);
        IEnumerable<Inspecao> ObterTodasInspecoes();

    }
}