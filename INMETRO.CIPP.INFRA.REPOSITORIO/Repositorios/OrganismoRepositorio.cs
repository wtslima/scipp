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
                    var consulta = contexto.Organismos.Include(f => f.IntegracaoInfo).FirstOrDefault(x => x.CodigoOIA.Trim().Contains(codigoOia) && x.EhAtivo);

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
                    
                    var consulta = await contexto.Organismos.Include(ftp => ftp.IntegracaoInfo).Where(s => s.EhAtivo && s.IntegracaoInfo != null ).OrderBy(s=> s.Nome).ToListAsync();

                    return consulta.Select(item => new Organismo
                    {

                        Id = item.Id,
                        CodigoOIA = item.CodigoOIA,
                        Nome = item.Nome.ToUpper(),
                        EhAtivo = item.EhAtivo,
                        IntegracaoInfo = item.IntegracaoInfo == null ? null : new IntegracaoInfos
                        {
                            Id = item.IntegracaoInfo.Id,
                            OrganismoId = item.IntegracaoInfo.OrganismoId,
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

        public Organismo ObterPorId(int id)
        {
            using(var ctx = new CippContexto())
            {
                try
                {
                    var resultado = ctx.Organismos.FirstOrDefault(s => s.Id == id);

                    return resultado;
                }
                catch (Exception e)
                {

                    throw;
                }
            }
        }

        public bool Atualizar(Organismo organismo)
        {
            using (var ctx = new CippContexto())
            {
                try
                {
                     ctx.Entry(organismo).State = EntityState.Modified;
                     var resultado = ctx.SaveChanges();

                    if (resultado <= 0) return false;

                    return true;

                }
                catch (Exception e)
                {

                    throw;
                }
            }
        }

        public bool Adicionar(Organismo organismo)
        {
            using (var ctx = new CippContexto())
            {
                try
                {
                    ctx.Organismos.Add(organismo);
                    var resultado = ctx.SaveChanges();

                    if (resultado <= 0) return false;

                    return true;

                }
                catch (Exception e)
                {

                    throw;
                }
            }
        }

        public bool Excluir(int id)
        {
            using(var ctx = new CippContexto())
            {
                try
                {
                    var resultado = ctx.Organismos.FirstOrDefault(s => s.Id == id);
                    ctx.Organismos.Attach(resultado);
                    ctx.Organismos.Remove(resultado);
                    var retorno = ctx.SaveChanges();

                    if (retorno <= 0) return false;

                    return true;

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public IList<Organismo> BuscarTodos()
        {
            using (var contexto = new CippContexto())
            {
                try
                {
                    var consulta =  contexto.Organismos.Where(s => s.EhAtivo).OrderBy(s => s.Nome).ToList();

                    return consulta.Select(item => new Organismo
                    {


                        Id = item.Id,
                        CodigoOIA = item.CodigoOIA,
                        Nome = item.Nome.ToUpper(),
                        EhAtivo = item.EhAtivo,
                        IntegracaoInfo = item.IntegracaoInfo == null ? null : new IntegracaoInfos
                        {
                            Id = item.IntegracaoInfo.Id,
                            OrganismoId = item.IntegracaoInfo.OrganismoId,
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
    }
}
