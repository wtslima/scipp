using System.Collections.Generic;
using System.Threading.Tasks;
using INMETRO.CIPP.DOMINIO.Modelos;

namespace INMETRO.CIPP.DOMINIO.Interfaces.Repositorios
{
    public interface IOrganismoRepositorio
    {
        Organismo BuscarOrganismoPorId(int codigoOIA);
        Task<IList<Organismo>> BuscarTodosOrganismos();
    }
}