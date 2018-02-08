using System;
using INMETRO.CIPP.DOMINIO.Modelos;
using INMETRO.CIPP.SERVICOS.ModelService;
using INMETRO.CIPP.SHARED.ModelShared;

namespace INMETRO.CIPP.SERVICOS
{
    public class Conversao
    {
        public static Inspecao ConverterParaDominio(InspecaoCsvModel value)
        {
            if(value == null) return new Inspecao();

            return new Inspecao
            {
                CodigoCIPP = value.CodigoCipp,
                CodigoOIA = value.CodigoOia,
                PlacaLicenca = value.PlacaLicenca,
                NumeroEquipamento = value.NumeroEquipamento,
                DataInspecao = value.DataInspecao,
                ResponsavelTecnico = value.Responsavel
            };
        }

        public static InspecaoCsvModel ConverterParaModeloServico(Inspecao value)
        {
            if (value == null) return new InspecaoCsvModel();

            return new InspecaoCsvModel()
            {
                CodigoCipp = value.CodigoCIPP,
                CodigoOia= value.CodigoOIA,
                PlacaLicenca = value.PlacaLicenca,
                NumeroEquipamento = value.NumeroEquipamento,
                DataInspecao = value.DataInspecao,
                Responsavel = value.ResponsavelTecnico
            };
        }

        public static Historico ConverterParaDominio(HistoricoServiceModel value)
        {
            if (value == null) return new Historico();

            return new Historico
            {
                InspecaoId = value.IdInspecao,
                Usuario = value.Usuario,
                DataDownload = value.DataEntrada
                
            };
        }

        public static HistoricoServiceModel ConverterParaModeloServico(Historico value)
        {
            if (value == null) return new HistoricoServiceModel();

            return new HistoricoServiceModel()
            {
                IdInspecao = value.InspecaoId,
                Usuario = value.Usuario,
                DataEntrada = value.DataDownload

            };
        }

        public static HistoricoExclusao ConverterParaDominio(HistoricoExclusaoServiceModel value)
        {
            if (value == null) return new HistoricoExclusao();

            return new HistoricoExclusao()
            {
                CodigoOia = value.CodigoOia,
                Cipp = value.Cipp,
                DataExclusao = value.DataExclusao

            };
        }

        public static HistoricoExclusaoServiceModel ConverterParaModeloServico(HistoricoExclusao value)
        {
            if (value == null) return new HistoricoExclusaoServiceModel();

            return new HistoricoExclusaoServiceModel()
            {
                CodigoOia = value.CodigoOia,
                Cipp = value.Cipp,
                DataExclusao = value.DataExclusao

            };
        }


        public static InspecaoModelServico ConverterParaServico(Inspecao value)
        {
            if (value == null) return new InspecaoModelServico();

            return new InspecaoModelServico()
            {
                CodigoCipp = value.CodigoCIPP,
                CodigoOia = value.CodigoOIA,
                Placa = value.PlacaLicenca,
                Equipamento = value.NumeroEquipamento.ToString(),
                DataInspecao = value.DataInspecao,
                Responsavel = value.ResponsavelTecnico
            };
        }

        public static Inspecao ConverterParaDominio(InspecaoModelServico value)
        {
            if (value == null) return new Inspecao();

            return new Inspecao
            {
                CodigoCIPP = value.CodigoCipp,
                CodigoOIA = value.CodigoOia,
                PlacaLicenca = value.Placa,
                NumeroEquipamento = Convert.ToInt32(value.Equipamento),
                DataInspecao = value.DataInspecao,
                ResponsavelTecnico = value.Responsavel
            };
        }
    }
}