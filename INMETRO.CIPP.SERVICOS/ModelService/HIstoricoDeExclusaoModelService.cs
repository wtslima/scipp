﻿using System.Collections.Generic;

namespace INMETRO.CIPP.SERVICOS.ModelService
{
    public class HistoricoDeExclusaoModelService
    {
        public IEnumerable<HistoricoExclusaoServiceModel> HistoricoExclusoes { get; set; }

        public ExcecaoService Excecao { get; set; }

    }
}