using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using INMETRO.CIPP.DOMINIO.Interfaces.Repositorios;
using INMETRO.CIPP.DOMINIO.Modelos;
using INMETRO.CIPP.INFRA.ENTITYFRAMEWORK;

namespace INMETRO.CIPP.INFRA.REPOSITORIO.Repositorios
{
    public class InspecaoRepositorio : IInspecaoRepositorio
    {
        public bool AdicionarDadosInspecao(Inspecao inspecao)
        {
            try
            {
                using (var ctx = new Contexto())
                {
                    if (inspecao == null) return false;
                    ctx.Inspecoes.AddOrUpdate(inspecao);
                    ctx.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Inspecao ObterDadosInspecao(string Cipp)
        {
            try
            {
                using (var contexto = new Contexto())
                {
                    var consulta = contexto.Inspecoes.FirstOrDefault(s => s.CodigoCIPP.Equals(Cipp));

                    return consulta ?? new Inspecao();
                }
                  
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool AtualizarDadosInspecao(Inspecao inspecao)
        {
            throw new System.NotImplementedException();
        }

        public bool BuscarInspecaoPorCodigoCipp(string cipp)
        {
            try
            {
                using (var contexto = new Contexto())
                {
                    if (string.IsNullOrEmpty(cipp)) return false;

                    var resultado = contexto.Inspecoes.FirstOrDefault(s => s.CodigoCIPP.Equals(cipp));

                    return resultado != null;
                }
            }
            catch (Exception e)
            {
               
                throw e;
            }
          
        }

        public IEnumerable<Inspecao> ObterInspecaosPorCodigoOia(string codigoOia)
        {
                using (var contexto = new Contexto())
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(codigoOia))
                        {
                            var consulta = contexto.Inspecoes.Where(s => s.CodigoOIA.Equals(codigoOia));

                            return consulta.Select(item => new Inspecao
                            {
                                Id = item.Id,
                                CodigoOIA = item.CodigoOIA,
                                CodigoCIPP = item.CodigoCIPP,
                                NumeroEquipamento = item.NumeroEquipamento,
                                PlacaLicenca = item.PlacaLicenca,
                                ResponsavelTecnico = item.ResponsavelTecnico,
                                DataInspecao = item.DataInspecao

                            }).ToList();
                        }

                        return new List<Inspecao>();
                }
                    catch (Exception e)
                    {
                        throw e;
                    }
                   
                }
           
        }
    }
}