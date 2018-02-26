using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INMETRO.CIPP.WEB.Models
{
    public class RetornoListaInspecoesViewModel
    {
        public IEnumerable<InspecaoModel> Inspecoes { get; set; }
        public Pager Pager { get; set; }
        public DownloadModel InspecaoModel { get; set; }

    }
}