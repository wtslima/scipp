using INMETRO.CIPP.DOMINIO.Modelos;
using System.Collections.Generic;

namespace INMETRO.CIPP.DOMINIO.Interfaces
{
    public interface IIntegracaoInfo
    {
        bool Adicionar(IntegracaoInfos obj);
        bool Atualizar(IntegracaoInfos obj);
        IList<IntegracaoInfos> ObterTodos();
        IntegracaoInfos ObterPorId(int id);
        bool Desativar(int id);
    }
}
