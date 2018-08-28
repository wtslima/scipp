using System.Collections.Generic;
using System.Threading.Tasks;
using INMETRO.CIPP.DOMINIO.Modelos;

namespace INMETRO.CIPP.DOMINIO.Interfaces
{
    public interface IOrganismoDominioService
    {
        Organismo BuscarOrganismoPorId(string codigoOia);
        
        Task<IList<Organismo>> BuscarTodosOrganismos();
        Organismo ObetrPorId(int id);
        bool Atualizar(Organismo organismo);
        bool Adicionar(Organismo organismo);
        bool Excluir(int id);
        IList<Organismo> BuscarTodos();

    }
}