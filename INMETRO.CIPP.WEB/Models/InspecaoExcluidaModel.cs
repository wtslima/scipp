using System.Collections.Generic;


namespace INMETRO.CIPP.WEB.Models
{
    public class InspecaoExcluidaModel
    {
        public IEnumerable<HistoricoDeExclusaoModel> HistoricoInspecoesExcluidas { get; set; }
        public Pager Pager { get; set; }
        public MensagemModel Mensagem { get; set; }

    }
}