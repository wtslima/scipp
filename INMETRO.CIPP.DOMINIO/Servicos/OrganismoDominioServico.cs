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
            if (!string.IsNullOrWhiteSpace(codigoOia))
            {
                var resultado =  _repositorio.BuscarOrganismoPorId(codigoOia);
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