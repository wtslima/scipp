using System.Collections.Generic;
using System.Threading.Tasks;
using INMETRO.CIPP.DOMINIO.Modelos;

namespace INMETRO.CIPP.DOMINIO.Interfaces.Repositorios
{
    public interface IOrganismoRepositorio
    {
        Organismo BuscarOrganismoPorId(string codigoOIA);
        Task<IList<Organismo>> BuscarTodosOrganismos();
        IList<Organismo> BuscarOrganismosPorParteDoCodigo(string valor);
        Organismo ObterPorId(int id);
        bool Atualizar(Organismo organismo);
        bool Adicionar(Organismo organismo);
        bool Excluir(int id);
        IList<Organismo> BuscarTodos();

    }
}