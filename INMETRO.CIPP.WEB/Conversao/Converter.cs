using System.Collections.Generic;
using System.Globalization;
using INMETRO.CIPP.SERVICOS.ModelService;
using INMETRO.CIPP.SHARED.ModelShared;
using INMETRO.CIPP.WEB.Models;

namespace INMETRO.CIPP.WEB.Conversao
{
    public class Converter
    {
        public static List<InspecaoModel>  ConverterParaModelo(List<InspecaoModelServico> list)
        {
            var lista = new List<InspecaoModel>();
            foreach (var item in list)
            {
                var inspecao = new InspecaoModel
                {
                    CodigoOia = item.CodigoOia,
                    CodigoCipp = item.CodigoCipp,
                    Placa = item.Placa,
                    Equipamento = item.Equipamento,
                    Responsavel = item.Responsavel,
                    DataInspecao = item.DataInspecao.ToString(CultureInfo.InvariantCulture),
                    ExisteExcecao = item.ExisteExcecao,
                    Mensagem = item.Mensagem
                    
                };
                lista.Add(inspecao);
            }

            return lista;
        }
    }
}