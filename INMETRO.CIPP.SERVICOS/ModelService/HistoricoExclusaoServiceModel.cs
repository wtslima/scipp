﻿using System;

namespace INMETRO.CIPP.SERVICOS.ModelService
{
    public class HistoricoExclusaoServiceModel
    {
        public string Cipp { get; set; }
        public string CodigoOia { get; set; }
        public DateTime DataExclusao { get; set; }
        public string Mensagem { get; set; }
        public bool ExisteExcecao { get; set; }
    }
}