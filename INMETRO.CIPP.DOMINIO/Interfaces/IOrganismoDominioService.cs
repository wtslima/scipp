using System.Collections.Generic;
using System.Threading.Tasks;
using INMETRO.CIPP.DOMINIO.Modelos;

namespace INMETRO.CIPP.DOMINIO.Interfaces
{
    public interface IOrganismoDominioService
    {
        Organismo BuscarOrganismoPorId(string codigoOia);
        
        Task<IList<Organismo>> BuscarTodosOrganismos();
    }
}