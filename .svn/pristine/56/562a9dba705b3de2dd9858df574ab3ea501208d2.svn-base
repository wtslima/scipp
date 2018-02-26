using System.Collections.Generic;
using INMETRO.CIPP.DOMINIO.Modelos;

namespace INMETRO.CIPP.DOMINIO.Interfaces.Repositorios
{
    public interface IHistoricoExclusaoRepositorio
    {
        bool AdicionarRegistroDeHistoricoDeExclusao(HistoricoExclusao historico);
        HistoricoExclusao ObterDadosInspecaoPorCipp(string cipp);
        IEnumerable<HistoricoExclusao> ObterInspecaoPorCodigoOia(string codigoOia);
        IEnumerable<HistoricoExclusao> BuscarRegistrosDeHistorico();
    }
}