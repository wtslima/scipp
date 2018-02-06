using System.Collections.Generic;
using INMETRO.CIPP.DOMINIO.Modelos;

namespace INMETRO.CIPP.DOMINIO.Interfaces
{
    public interface IHistoricoExclusao
    {
        bool AdicionarRegistroDeHistoricoDeExclusao(HistoricoExclusao historico);
        IEnumerable<HistoricoExclusao> BuscarRegistrosDeHistorico();
    }
}
