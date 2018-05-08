using System;
using System.Collections.Generic;
using System.Linq;
using INMETRO.CIPP.DOMINIO.Modelos;
using INMETRO.CIPP.SERVICOS.ModelService;
using INMETRO.CIPP.SHARED.ModelShared;

namespace INMETRO.CIPP.SERVICOS
{
    public class Conversao
    {
        public static Inspecao ConverterParaDominio(InspecaoCsvModel value)
        {
            if (value == null) return new Inspecao();

            return new Inspecao
            {
                CodigoCipp = value.CodigoCipp,
                CodigoOIA = value.CodigoOia,
                PlacaLicenca = value.PlacaLicenca,
                NumeroEquipamento = value.NumeroEquipamento,
                DataInspecao = value.DataInspecao.Date
               
            };
        }

        public static InspecaoCsvModel ConverterParaModeloServico(Inspecao value)
        {
            if (value == null) return new InspecaoCsvModel();

            return new InspecaoCsvModel()
            {
                CodigoCipp = value.CodigoCipp,
                CodigoOia = value.CodigoOIA.ToString(),
                PlacaLicenca = value.PlacaLicenca,
                NumeroEquipamento = value.NumeroEquipamento.ToString(),
                DataInspecao = value.DataInspecao
              
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

        public static HistoricoDeExclusaoModelService ConverterParaModeloServico(HistoricoExclusao value)
        {

            return new HistoricoDeExclusaoModelService
            {
               HistoricoExclusoes = new List<HistoricoExclusaoServiceModel>
               {
                   new HistoricoExclusaoServiceModel
                   {
                       CodigoOia = value.CodigoOia,
                       Cipp = value.Cipp,
                       DataExclusao = value.DataExclusao
                   }
               },
               Excecao = new ExcecaoService
               {
                   Mensagem = value.Mensagem,
                   ExisteExcecao = value.ExisteExcecao
               }

            };
        }


        public static InspecoesGravadasModelServico ConverterParaServico(Inspecao value)
        {
            return new InspecoesGravadasModelServico
            {
                InspecoesGravadas = new List<InspecaoModelServico>
                {
                    new InspecaoModelServico
                    {
                        CodigoOia = value.CodigoOIA.ToString(),
                        CodigoCipp = value.CodigoCipp,
                        Equipamento = value.NumeroEquipamento,
                        Placa = value.PlacaLicenca,
                        DataInspecao = value.DataInspecao.Date
                        
                    }
                },
                Excecao = new ExcecaoService
                {
                    ExisteExcecao = value.ExcecaoDominio.ExisteExcecao,
                    Mensagem = value.ExcecaoDominio.Mensagem
                }
            };
        }

        public static Inspecao ConverterParaDominio(InspecaoModelServico value)
        {
            if (value == null) return new Inspecao();

            return new Inspecao
            {
                CodigoCipp = value.CodigoCipp,
                CodigoOIA = value.CodigoOia,
                PlacaLicenca = value.Placa,
                NumeroEquipamento = value.Equipamento,
                DataInspecao = value.DataInspecao.Date
                
            };
        }

        public static InspecaoModelServico ConverterParaModeloServico(InspecaoCsvModel value)
        {
            if (value == null) return new InspecaoModelServico();

            return new InspecaoModelServico
            {
                CodigoCipp = value.CodigoCipp,
                CodigoOia = value.CodigoOia,
                Placa = value.PlacaLicenca,
                Equipamento = value.NumeroEquipamento,
                DataInspecao = value.DataInspecao.Date
               
            };
        }

        public static InspecaoCsvModel ConverterParaModeloCsv(InspecaoModelServico value)
        {
            if (value == null) return new InspecaoCsvModel();

            return new InspecaoCsvModel()
            {
                CodigoCipp = value.CodigoCipp,
                CodigoOia = value.CodigoOia,
                PlacaLicenca = value.Placa,
                NumeroEquipamento =value.Equipamento,
                DataInspecao = value.DataInspecao.Date
              
            };
        }

        public static List<InspecaoModelServico> ConverterListaParaModeloService(List<Inspecao> list)
        {
            var lista = new List<InspecaoModelServico>();
            foreach (var item in list)
            {
                var inspecao = new InspecaoModelServico
                {
                    CodigoOia = item.CodigoOIA.ToString(),
                    CodigoCipp = item.CodigoCipp,
                    Placa = item.PlacaLicenca,
                    Equipamento = item.NumeroEquipamento.ToString(),
                    DataInspecao = item.DataInspecao.Date
                    };
                lista.Add(inspecao);
            }

            return lista;
        }

        public static List<HistoricoExclusaoServiceModel> ConverterListaParaModeloService(List<HistoricoExclusao> list)
        {
            var lista = new List<HistoricoExclusaoServiceModel>();
            foreach (var item in list)
            {
                var inspecao = new HistoricoExclusaoServiceModel
                {
                    CodigoOia = item.CodigoOia,
                    Cipp = item.Cipp,
                    DataExclusao = item.DataExclusao,
                    ExisteExcecao = item.ExisteExcecao,
                    Mensagem = item.Mensagem

                };
                lista.Add(inspecao);
            }

            return lista;
        }

        public static HistoricoDeExclusaoModelService ConverterParaModelService(HistoricoDeInspecoesExcluidas value)
        {
            if (value == null) return new HistoricoDeExclusaoModelService();

            return new HistoricoDeExclusaoModelService
            {
                HistoricoExclusoes = value.InspecoesExcluidas.Select(s => new HistoricoExclusaoServiceModel
                {
                    CodigoOia = s.CodigoOia,
                    Cipp = s.Cipp,
                    DataExclusao = s.DataExclusao
                }),
                Excecao = new ExcecaoService
                {
                    ExisteExcecao = value.ExcecaoDominio.ExisteExcecao,
                    Mensagem = value.ExcecaoDominio.Mensagem
                }

            };
        }

        public static InspecoesGravadasModelServico ConverterParaModelService(InspecoesGravadas value)
        {
            if (value == null) return new InspecoesGravadasModelServico();

            var list = new InspecoesGravadasModelServico
            {
                InspecoesGravadas = value.Inspecoes.Select(s => new InspecaoModelServico()
                {
                    CodigoOia =  !string.IsNullOrEmpty(s.CodigoOIA) ? s.CodigoOIA : string.Empty,
                    CodigoCipp = !string.IsNullOrEmpty(s.CodigoCipp) ? s.CodigoCipp : string.Empty,
                    DataInspecao = s.DataInspecao.Date,
                    Equipamento = !string.IsNullOrEmpty(s.NumeroEquipamento) ? s.NumeroEquipamento : string.Empty,
                    Placa = !string.IsNullOrEmpty(s.PlacaLicenca) ? s.PlacaLicenca : string.Empty

                }),
                Excecao = new ExcecaoService
                {
                    ExisteExcecao = value.ExcecaoDominio.ExisteExcecao,
                    Mensagem = value.ExcecaoDominio.Mensagem
                }

            };

            return list;
        }
    }
}