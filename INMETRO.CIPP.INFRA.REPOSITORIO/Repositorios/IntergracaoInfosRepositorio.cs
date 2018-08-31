using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using INMETRO.CIPP.DOMINIO.Interfaces.Repositorios;
using INMETRO.CIPP.DOMINIO.Modelos;
using INMETRO.CIPP.INFRA.ENTITYFRAMEWORK;

namespace INMETRO.CIPP.INFRA.REPOSITORIO.Repositorios
{
    public class IntegracaoInfosRepositorio : IIntegracaoInfoRepositorio
    {
        public IntegracaoInfos ObterPorId(int id)
        {
            using (var contexto = new CippContexto())
            {
                try
                {
                    var consulta = contexto.IntegracaoInfo.FirstOrDefault(x => x.Id == id);

                    return consulta ;
 
                }
                catch (Exception e)
                {
                    throw e;
                }

            }
        }

        public IList<IntegracaoInfos> ObterTodos()
        {
            using (var contexto = new CippContexto())
            {
                try
                {
                    
                    var consulta =  contexto.IntegracaoInfo.OrderBy(s=> s.DiretorioInspecaoLocal).ToList();

                    return consulta.Select(item => new IntegracaoInfos
                    {
                                                    Id = item.Id,
                            OrganismoId = item.OrganismoId,
                            DiretorioInspecao = item.DiretorioInspecao,
                            DiretorioInspecaoLocal = item.DiretorioInspecaoLocal,
                            HostURI = item.HostURI,
                            Usuario = item.Usuario,
                            Senha = item.Senha,
                            TipoIntegracao = item.TipoIntegracao,
                            PrivateKey = item.PrivateKey,
                            Porta = item.Porta
                        
                    }).ToList();

                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
        

        public bool Atualizar(IntegracaoInfos obj)
        {
            using (var ctx = new CippContexto())
            {
                try
                {
                     ctx.Entry(obj).State = EntityState.Modified;
                     var resultado = ctx.SaveChanges();

                    if (resultado <= 0) return false;

                    return true;

                }
                catch 
                {

                    throw;
                }
            }
        }

        public bool Adicionar(IntegracaoInfos obj)
        {
            using (var ctx = new CippContexto())
            {
                try
                {
                    ctx.IntegracaoInfo.Add(obj);
                    var resultado = ctx.SaveChanges();

                    if (resultado <= 0) return false;

                    return true;

                }
                catch 
                {

                    throw;
                }
            }
        }

        public bool Desativar(int id)
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
