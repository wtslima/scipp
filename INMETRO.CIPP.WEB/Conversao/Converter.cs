using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using INMETRO.CIPP.SERVICOS.ModelService;
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

        public static List<HistoricoDeExclusaoModel> ConverterParaModelo(List<HistoricoExclusaoServiceModel> list)
        {
            var lista = new List<HistoricoDeExclusaoModel>();


            foreach (var item in list)
            {
                var inspecao = new HistoricoDeExclusaoModel
                {
                    CodigoOia = item.CodigoOia,
                    Cipp = item.Cipp,
                    DataExclusao = item.DataExclusao.ToString(CultureInfo.InvariantCulture),
                    ExisteExcecao = item.ExisteExcecao,
                    Mensagem = item.Mensagem,
                    Retorno = new RetornoDownloadModel
                    {
                        ExisteExcecao = item.ExisteExcecao,
                        Mensagem = item.Mensagem
                    }
                };
                lista.Add(inspecao);
            }

            return lista.ToList();
        }


        public static InspecaoExcluidaModel ConverterParaModelo(HIstoricoDeExclusaoModelService value)
        {
            if (value == null) return new InspecaoExcluidaModel();

            return new InspecaoExcluidaModel
            {
                HistoricoInspecoesExcluidas = value.HistoricoExclusoes.Select(s => new HistoricoDeExclusaoModel
                {
                    CodigoOia = s.CodigoOia,
                    Cipp = s.Cipp,
                    DataExclusao = s.DataExclusao.ToString(CultureInfo.InvariantCulture)
                }).ToList(),
                Retorno = new RetornoDownloadModel()
                {
                    ExisteExcecao = value.Excecao.Excecao,
                    Mensagem = value.Excecao.Mensagem
                }

            };
           
        }
    }
}