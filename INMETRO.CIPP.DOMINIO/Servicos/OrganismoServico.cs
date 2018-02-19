using System.Collections.Generic;
using System.Threading.Tasks;
using INMETRO.CIPP.DOMINIO.Interfaces;
using INMETRO.CIPP.DOMINIO.Interfaces.Repositorios;
using INMETRO.CIPP.DOMINIO.Modelos;

namespace INMETRO.CIPP.DOMINIO.Servicos
{
    public class OrganismoServico : IOrganismo
    {
        private readonly IOrganismoRepositorio _repositorio;
        public OrganismoServico(IOrganismoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public  Organismo BuscarOrganismoPorId(string codigoOIA)
        {
            if (!string.IsNullOrWhiteSpace(codigoOIA))
            {
                var resultado =  _repositorio.BuscarOrganismoPorId(codigoOIA);
                return resultado;
            }
           
            return new Organismo();
           
        }

        public async Task<IList<Organismo>> BuscarTodosOrganismos()
        {
            var resultado = await _repositorio.BuscarTodosOrganismos();

            return resultado;
        }
    }
}