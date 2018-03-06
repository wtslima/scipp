﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using INMETRO.CIPP.DOMINIO.Interfaces;
using INMETRO.CIPP.DOMINIO.Interfaces.Repositorios;
using INMETRO.CIPP.DOMINIO.Modelos;

namespace INMETRO.CIPP.DOMINIO.Servicos
{
    public class OrganismoDominioServico : IOrganismoDominioService
    {
        private readonly IOrganismoRepositorio _repositorio;
        public OrganismoDominioServico(IOrganismoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public  Organismo BuscarOrganismoPorId(string codigoOia)
        {
            var _codigo = Convert.ToInt32(codigoOia);
            var resultado =  _repositorio.BuscarOrganismoPorId(_codigo);

            if (resultado.Id <= 0)
                return new Organismo
                {
                    ExcecaoDominio = new ExcecaoDominio
                    {
                        ExisteExcecao = true,
                        Mensagem = string.Format(Mensagens.MensagemNegocio.NaoExisteCodigoOia, codigoOia)

                    }
                };
            resultado.ExcecaoDominio = new ExcecaoDominio
            {
                ExisteExcecao = false,
                Mensagem = string.Empty
            };

            return resultado;
        }

        public async Task<IList<Organismo>> BuscarTodosOrganismos()
        {
            var resultado = await _repositorio.BuscarTodosOrganismos();

            return resultado;
        }
    }
}