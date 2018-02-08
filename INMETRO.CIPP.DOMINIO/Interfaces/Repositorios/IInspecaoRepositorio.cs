using System.Collections.Generic;
using INMETRO.CIPP.DOMINIO.Modelos;

namespace INMETRO.CIPP.DOMINIO.Interfaces.Repositorios
{
    public interface IInspecaoRepositorio
    {
        bool AdicionarDadosInspecao(Inspecao inspecao);
        Inspecao ObterDadosInspecao(string Cipp);
        bool AtualizarDadosInspecao(Inspecao inspecao);
        bool BuscarInspecaoPorCodigoCipp(string cipp);
        IEnumerable<Inspecao> ObterInspecaosPorCodigoOia(string codigoOia);
    }
}