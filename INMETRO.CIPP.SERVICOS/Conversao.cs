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
                CodigoOia = value.CodigoOIA,
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
                DataExclusao = value.DataExclusao,
                ExisteExcecao = value.ExisteExcecao,
                Mensagem = value.Mensagem

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
                        CodigoOia = value.CodigoOIA,
                        CodigoCipp = value.CodigoCIPP,
                        Equipamento = value.NumeroEquipamento.ToString(),
                        Placa = value.PlacaLicenca,
                        DataInspecao = value.DataInspecao,
                        Responsavel = value.ResponsavelTecnico
                    }
                },
                Excecao = new ExcecaoService
                {
                    Excecao = value.ExcecaoDominio.ExisteExcecao,
                    Mensagem = value.ExcecaoDominio.Mensagem
                }
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

        public static InspecaoModelServico ConverterParaModeloServico(InspecaoCsvModel value)
        {
            if (value == null) return new InspecaoModelServico();

            return new InspecaoModelServico
            {
                CodigoCipp = value.CodigoCipp,
                CodigoOia = value.CodigoOia,
                Placa = value.PlacaLicenca,
                Equipamento = value.NumeroEquipamento.ToString(),
                DataInspecao = value.DataInspecao,
                Responsavel = value.Responsavel
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
                NumeroEquipamento = Convert.ToInt32(value.Equipamento),
                DataInspecao = value.DataInspecao,
                Responsavel = value.Responsavel
            };
        }

        public static List<InspecaoModelServico> ConverterListaParaModeloService(List<Inspecao> list)
        {
            var lista = new List<InspecaoModelServico>();
            foreach (var item in list)
            {
                var inspecao = new InspecaoModelServico
                {
                    CodigoOia = item.CodigoOIA,
                    CodigoCipp = item.CodigoCIPP,
                    Placa = item.PlacaLicenca,
                    Equipamento = item.NumeroEquipamento.ToString(),
                    Responsavel = item.ResponsavelTecnico,
                    DataInspecao = item.DataInspecao
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
                    Excecao = value.ExcecaoDominio.ExisteExcecao,
                    Mensagem = value.ExcecaoDominio.Mensagem
                }

            };
        }

        public static InspecoesGravadasModelServico ConverterParaModelService(InspecoesGravadas value)
        {
            if (value == null) return new InspecoesGravadasModelServico();

            return new InspecoesGravadasModelServico
            {
                InspecoesGravadas = value.Inspecoes.Select(s => new InspecaoModelServico()
                {
                    CodigoOia = s.CodigoOIA,
                    CodigoCipp = s.CodigoCIPP,
                    DataInspecao = s.DataInspecao,
                    Equipamento = s.NumeroEquipamento.ToString(),
                    Responsavel = s.ResponsavelTecnico,
                    Placa = s.PlacaLicenca

                }),
                Excecao = new ExcecaoService
                {
                    Excecao = value.ExcecaoDominio.ExisteExcecao,
                    Mensagem = value.ExcecaoDominio.Mensagem
                }

            };
        }
    }
}