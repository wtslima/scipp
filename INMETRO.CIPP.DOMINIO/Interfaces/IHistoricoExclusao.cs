using System.Collections.Generic;
using INMETRO.CIPP.DOMINIO.Modelos;

namespace INMETRO.CIPP.DOMINIO.Interfaces
{
    public interface IHistoricoExclusao
    {
        bool AdicionarRegistroDeHistoricoDeExclusao(HistoricoExclusao historico);
        HistoricoExclusao ObterDadosInspecaoPorCipp(string cipp);
      
        HistoricoExclusao ObterInspecaoParaCippECodigoOiaInformado(string codigoOia, string cipp);

        IEnumerable<HistoricoExclusao> ObterInspecaoPorCodigoOia(string codigoOia);

        IEnumerable<HistoricoExclusao> BuscarRegistrosDeHistorico();
    }
}
