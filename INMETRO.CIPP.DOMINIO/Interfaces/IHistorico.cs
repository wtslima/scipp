using System.Collections.Generic;
using INMETRO.CIPP.DOMINIO.Modelos;

namespace INMETRO.CIPP.DOMINIO.Interfaces
{
    public interface IHistorico
    {
        bool AdicionarInspecao(Historico historico);

        IEnumerable<Historico> BuscarRegistrosDeHistorico();
    }
}