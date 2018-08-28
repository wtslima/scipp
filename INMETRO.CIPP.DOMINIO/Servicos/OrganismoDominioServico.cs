using System;
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

        public bool Adicionar(Organismo organismo)
        {
            var resultado = _repositorio.Adicionar(organismo);

            return resultado;
        }

        public bool Atualizar(Organismo organismo)
        {
            var resultado =  _repositorio.Atualizar(organismo);

            return resultado;
        }

        public  Organismo BuscarOrganismoPorId(string codigoOia)
        {
           
            var resultado =  _repositorio.BuscarOrganismoPorId(codigoOia);

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

        public IList<Organismo> BuscarTodos()
        {
            var resultado =  _repositorio.BuscarTodos();

            return resultado;
        }

        public async Task<IList<Organismo>> BuscarTodosOrganismos()
        {
            var resultado = await _repositorio.BuscarTodosOrganismos();

            return resultado;
        }

        public bool Excluir(int id)
        {
            throw new NotImplementedException();
        }

        public Organismo ObetrPorId(int id)
        {
            var resultado = _repositorio.ObterPorId(id);

            return resultado;
        }
    }
}