using System.Collections.Generic;
using INMETRO.CIPP.SERVICOS.ModelService;

namespace INMETRO.CIPP.WEB.Models
{
    public class InspecaoExcluidaModel
    {
        public IEnumerable<HistoricoDeExclusaoModel> HistoricoInspecoesExcluidas { get; set; }
        public Pager Pager { get; set; }
        public RetornoDownloadModel RetornoDownloadModel { get; set; }
    }
}