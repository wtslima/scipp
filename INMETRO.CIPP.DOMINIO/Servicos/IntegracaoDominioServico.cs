using System.Collections.Generic;
using INMETRO.CIPP.DOMINIO.Interfaces;
using INMETRO.CIPP.DOMINIO.Interfaces.Repositorios;
using INMETRO.CIPP.DOMINIO.Modelos;

namespace INMETRO.CIPP.DOMINIO.Servicos
{
    public class IntegracaoDominioServico : IIntegracaoInfo
    {
        private readonly IIntegracaoInfoRepositorio _repositorio;
        public IntegracaoDominioServico(IIntegracaoInfoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public bool Adicionar(IntegracaoInfos organismo)
        {
            var resultado = _repositorio.Adicionar(organismo);

            return resultado;
        }

        public bool Atualizar(IntegracaoInfos organismo)
        {
            var resultado =  _repositorio.Atualizar(organismo);

            return resultado;
        }

        public  IntegracaoInfos ObterPorId(int id)
        {
           
            var resultado =  _repositorio.ObterPorId(id);

            return resultado;
        }

        public IList<IntegracaoInfos> ObterTodos()
        {
            var resultado =  _repositorio.ObterTodos();

            return resultado;
        }

        public bool Desativar(int id)
        {
            var resultado = _repositorio.Desativar(id);

            return resultado;
        }
       
    }
}