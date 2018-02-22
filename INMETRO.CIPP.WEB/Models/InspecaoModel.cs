using System;

namespace INMETRO.CIPP.WEB.Models
{
    public class InspecaoModel
    {
        public string CodigoOia { get; set; }
        public string CodigoCipp { get; set; }
        public string Responsavel { get; set; }
        public string Equipamento { get; set; }
        public string Placa { get; set; }
        public string DataInspecao { get; set; }
        public string Mensagem { get; set; }
       
    }
}