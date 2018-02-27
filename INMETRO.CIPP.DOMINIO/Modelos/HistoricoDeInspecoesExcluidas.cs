using System.Collections.Generic;

namespace INMETRO.CIPP.DOMINIO.Modelos
{
    public class HistoricoDeInspecoesExcluidas
    {
        public IEnumerable<HistoricoExclusao> InspecoesExcluidas { get; set; }
        public ExcecaoDominio ExcecaoDominio { get; set; }

    }
}