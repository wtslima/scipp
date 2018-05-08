using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using INMETRO.CIPP.DOMINIO.Interfaces.Repositorios;
using INMETRO.CIPP.DOMINIO.Modelos;
using INMETRO.CIPP.INFRA.ENTITYFRAMEWORK;

namespace INMETRO.CIPP.INFRA.REPOSITORIO.Repositorios
{
    public class OrganismoRepositorio : IOrganismoRepositorio
    {
        public Organismo BuscarOrganismoPorId(string codigoOia)
        {
            using (var contexto = new CippContexto())
            {
                try
                {
                    var consulta = contexto.Organismos.Include(f => f.IntegracaoInfo).FirstOrDefault(x => x.CodigoOIA.Contains(codigoOia) && x.EhAtivo);

                    return consulta ;
 
                }
                catch (Exception e)
                {
                    throw e;
                }

            }
        }

        public async Task<IList<Organismo>> BuscarTodosOrganismos()
        {
            using (var contexto = new CippContexto())
            {
                try
                {
                    
                    var consulta = await contexto.Organismos.Include(ftp => ftp.IntegracaoInfo).Where(s => s.EhAtivo && s.IntegracaoInfo != null ).ToListAsync();

                    return consulta.Select(item => new Organismo
                    {

                        Id = item.Id,
                        CodigoOIA = item.CodigoOIA,
                        Nome = item.Nome,
                        EhAtivo = item.EhAtivo,
                        IntegracaoInfo = item.IntegracaoInfo == null ? null : new IntegracaoInfos
                        {
                            DiretorioInspecao = item.IntegracaoInfo.DiretorioInspecao,
                            DiretorioInspecaoLocal = item.IntegracaoInfo.DiretorioInspecaoLocal,
                            HostURI = item.IntegracaoInfo.HostURI,
                            Usuario = item.IntegracaoInfo.Usuario,
                            Senha = item.IntegracaoInfo.Senha,
                            TipoIntegracao = item.IntegracaoInfo.TipoIntegracao,
                            PrivateKey = item.IntegracaoInfo.PrivateKey,
                            Porta = item.IntegracaoInfo.Porta
                        }
                    }).ToList();

                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public IList<Organismo> BuscarOrganismosPorParteDoCodigo(string valor)
        {
            using (var contexto = new CippContexto())
            {
                try
                {
                    var resultado = contexto.Organismos.Where(s => s.CodigoOIA.StartsWith(valor)).ToList();

                    return resultado;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}
