﻿using INMETRO.CIPP.SERVICOS.ModelService;
using INMETRO.CIPP.WEB.ControleAcesso;

namespace INMETRO.CIPP.WEB.Models
{
    public class HistoricoDeExclusaoModel
    {
        public string Cipp { get; set; }
        public string CodigoOia { get; set; }
        public string DataExclusao { get; set; }
        public string Mensagem { get; set; }
        public bool ExisteExcecao { get; set; }
        public Pager Pagination { get; set; }

        public RetornoDownloadModel Retorno { get; set; }
    }
}