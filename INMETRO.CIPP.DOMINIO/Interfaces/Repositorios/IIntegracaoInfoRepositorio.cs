using INMETRO.CIPP.DOMINIO.Modelos;
using System.Collections.Generic;


namespace INMETRO.CIPP.DOMINIO.Interfaces.Repositorios
{
    public interface IIntegracaoInfoRepositorio
    {

            bool Adicionar(IntegracaoInfos obj);
            bool Atualizar(IntegracaoInfos obj);
            IList<IntegracaoInfos> ObterTodos();
            IntegracaoInfos ObterPorId(int id);
            bool Desativar(int id);
    }
}
