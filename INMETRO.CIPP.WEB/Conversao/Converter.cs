﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using INMETRO.CIPP.SERVICOS.ModelService;
using INMETRO.CIPP.WEB.Models;

namespace INMETRO.CIPP.WEB.Conversao
{
    public class Converter
    {

        public static InspecoesGravadasModel  ConverterParaModelo(InspecoesGravadasModelServico value)
        {

            if (value == null) return new InspecoesGravadasModel();

            var inspecao = new InspecoesGravadasModel
            {
                
                Inspecoes = value.InspecoesGravadas.ToList().Select(s => new InspecaoModel()

                {
                    CodigoOia = s.CodigoOia,
                    CodigoCipp = s.CodigoCipp,
                    DataInspecao = s.DataInspecao.ToString("d"),
                    Equipamento = !string.Equals(s.Equipamento, null, StringComparison.Ordinal) ? s.Equipamento : String.Empty,
                    Placa = !string.Equals(s.Placa, null, StringComparison.Ordinal) ? s.Placa : String.Empty

                }),
                Mensagem = new MensagemModel
                {
                    ExisteExcecao = value.Excecao.ExisteExcecao,
                    Mensagem = value.Excecao.Mensagem
                }
            };

            return inspecao;

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
                    
                    MensagemModel = new MensagemModel
                    {
                        ExisteExcecao = item.ExisteExcecao,
                        Mensagem = item.Mensagem
                    }
                };
                lista.Add(inspecao);
            }

            return lista.ToList();
        }

        

        public static InspecaoExcluidaModel ConverterParaModelo(HistoricoDeExclusaoModelService value)
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
                Mensagem = new MensagemModel()
                {
                    ExisteExcecao = value.Excecao.ExisteExcecao,
                    Mensagem = value.Excecao.Mensagem
                }

            };
           
        }
        
    }
}