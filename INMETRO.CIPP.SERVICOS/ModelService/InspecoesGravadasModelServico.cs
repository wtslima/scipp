using System.Collections.Generic;

namespace INMETRO.CIPP.SERVICOS.ModelService
{
    public class InspecoesGravadasModelServico 
    {
        public IEnumerable<InspecaoModelServico> InspecoesGravadas { get; set; }
        public ExcecaoService Excecao { get; set; }
        
    }
}