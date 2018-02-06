﻿using System;
using System.ComponentModel;

namespace INMETRO.CIPP.SHARED.ModelShared
{
    public class InspecaoCsvModel
    {
        public string CodigoOia { get; set; }
        public string CodigoCipp { get; set; }
        public string PlacaLicenca { get; set; }
        public int NumeroEquipamento { get; set; }
        public DateTime DataInspecao { get; set; }
        public string Responsavel { get; set; }

        public InspecaoCsvModel()
        {
            
        }

        public InspecaoCsvModel(string codigoOia, string cipp, string placa, int equipamento, DateTime dataInspecao, string responsavel)
        {
            CodigoOia = codigoOia;
            CodigoCipp = cipp;
            PlacaLicenca = placa;
            NumeroEquipamento = equipamento;
            DataInspecao = dataInspecao;
            Responsavel = responsavel;
        }
    }
}