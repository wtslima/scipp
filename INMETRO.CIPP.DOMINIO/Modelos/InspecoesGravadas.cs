using System.Collections.Generic;

namespace INMETRO.CIPP.DOMINIO.Modelos
{
    public class InspecoesGravadas
    {
        public IEnumerable<Inspecao> Inspecoes { get; set; }
        public ExcecaoDominio ExcecaoDominio { get; set; }
    }
}
