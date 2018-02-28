using INMETRO.CIPP.SERVICOS.ModelService;

namespace INMETRO.CIPP.WEB.Models
{
    public class HistoricoDeExclusaoModel
    {
        public string Cipp { get; set; }
        public string CodigoOia { get; set; }
        public string DataExclusao { get; set; }
       
        public Pager Pagination { get; set; }

        public MensagemModel MensagemModel { get; set; }
    }
}