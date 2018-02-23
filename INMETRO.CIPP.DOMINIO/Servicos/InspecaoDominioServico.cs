﻿using System;
using System.Collections.Generic;
using INMETRO.CIPP.DOMINIO.Interfaces;
using INMETRO.CIPP.DOMINIO.Interfaces.Repositorios;
using INMETRO.CIPP.DOMINIO.Modelos;

namespace INMETRO.CIPP.DOMINIO.Servicos
{
    public class InspecaoDominioServico : IInspecaoDominioService
    {
        private readonly IInspecaoRepositorio _repositorio;
        public InspecaoDominioServico(IInspecaoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public bool AdicionarDadosInspecao(Inspecao inspecao)
        {
            var resultado = _repositorio.AdicionarDadosInspecao(inspecao);
            return resultado;
        }

        public Inspecao ObterDadosInspecao(string cipp)
        {
            var resultado = _repositorio.ObterDadosInspecao(cipp);
            return resultado;
        }

        public bool TemInspecao(string cipp)
        {
            var resultado = _repositorio.BuscarInspecaoPorCodigoCipp(cipp);

            return resultado;
        }

        public bool ExisteCodigoOia(string codigoOia)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Inspecao> ObterInspecaoPorCodigoOia(string codigoOia)
        {
            try
            {
                var consulta = _repositorio.ObterInspecaosPorCodigoOia(codigoOia);
               
                return consulta;
            }
            catch (Exception e)
            {
                throw e;
            }
           
        }

        public IEnumerable<Inspecao> ObterInspecaoPorPlacaLicenca(string placa)
        {
            try
            {
                if(string.IsNullOrEmpty(placa)) return new List<Inspecao>();

                var consulta = _repositorio.ObterInspecaosPorPlacaLicenca(placa);
                return consulta;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Inspecao> ObterTodasInspecoes()
        {
            try
            {
                var resultado =  _repositorio.ObterTodasInspecoes();
                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}