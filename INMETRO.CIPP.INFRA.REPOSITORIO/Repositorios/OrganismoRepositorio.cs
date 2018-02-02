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
        public Organismo BuscarOrganismoPorId(string codigoOIA)
        {
            using (var contexto = new Contexto())
            {
                try
                {
                    var consulta = contexto.Organismos.Include(ftp => ftp.FtpInfo).FirstOrDefault(x => x.CodigoOIA.Equals(codigoOIA));

                    return consulta ?? new Organismo();


                    //todo: Criar exceção() 
                }
                catch (Exception e)
                {
                    throw e;
                }

            }
        }

        public async Task<IList<Organismo>> BuscarTodosOrganismos()
        {
            using (var contexto = new Contexto())
            {
                try
                {
                    var consulta = await contexto.Organismos.Include(ftp => ftp.FtpInfo).ToListAsync();

                    return consulta.Select(item => new Organismo
                    {

                        Id = item.Id,
                        CodigoOIA = item.CodigoOIA,
                        Nome = item.Nome,
                        EhAtivo = item.EhAtivo,
                        FtpInfo = item.FtpInfo == null ? null : new FTPInfo
                        {
                            DiretorioInspecao = item.FtpInfo.DiretorioInspecao,
                            OrganismoId = item.FtpInfo.OrganismoId,
                            DiretorioInspecaoLocal = item.FtpInfo.DiretorioInspecaoLocal,
                            HostURI = item.FtpInfo.HostURI,
                            Usuario = item.FtpInfo.Usuario,
                            Senha = item.FtpInfo.Senha
                        }
                    }).ToList();

                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

    }
}
