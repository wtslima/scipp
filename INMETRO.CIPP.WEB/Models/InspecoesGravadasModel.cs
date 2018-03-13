using System.Collections.Generic;

namespace INMETRO.CIPP.WEB.Models
{
    public class InspecoesGravadasModel
    {
        public IEnumerable<InspecaoModel> Inspecoes { get; set; }
        public Pager Pager { get; set; }
        public MensagemModel Mensagem { get; set; }
        public DownloadModel DownloadModel { get; set; }
      
    }
}