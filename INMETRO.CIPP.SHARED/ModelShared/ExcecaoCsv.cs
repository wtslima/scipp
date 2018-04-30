using System;

namespace INMETRO.CIPP.SHARED.ModelShared
{
    public class ExcecaoCsv : Exception
    {
        public bool ExisteExcecao { get; set; }
        public string Mensagem { get; set; }
    }
}