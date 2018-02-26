using System;
using System.Collections.Generic;
using System.Linq;
using INMETRO.CIPP.DOMINIO.Interfaces.Repositorios;
using INMETRO.CIPP.DOMINIO.Modelos;
using INMETRO.CIPP.INFRA.ENTITYFRAMEWORK;

namespace INMETRO.CIPP.INFRA.REPOSITORIO.Repositorios
{
    public class HistoricoExclusaoRepositorio : IHistoricoExclusaoRepositorio
    {
        public bool AdicionarRegistroDeHistoricoDeExclusao(HistoricoExclusao historico)
        {
            try
            {
                using (var contexto = new CippContexto())
                {
                    if (historico == null) return false;
                    contexto.HistoricoExclusoes.Add(historico);
                    contexto.SaveChanges();
                    return true;

                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public HistoricoExclusao ObterDadosInspecaoPorCipp(string cipp)
        {
            try
            {
                using (var contexto = new CippContexto())
                {
                    var consulta = contexto.HistoricoExclusoes.FirstOrDefault(s => s.Cipp.Equals(cipp));

                    return consulta ?? new HistoricoExclusao();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        public IEnumerable<HistoricoExclusao> ObterInspecaoPorCodigoOia(string codigoOia)
        {
            using (var ctx = new CippContexto())
            {
                try
                {
                    if (string.IsNullOrEmpty(codigoOia)) return new List<HistoricoExclusao>();

                    var resultado = ctx.HistoricoExclusoes
                        .Where(s => s.CodigoOia == codigoOia)
                        .ToList()
                        .Select(
                            item => new HistoricoExclusao()
                            {
                                Id = item.Id,
                                CodigoOia = item.CodigoOia,
                                Cipp = item.Cipp,
                                DataExclusao = item.DataExclusao
                            }).ToList();

                    return resultado.ToList();
                }
                catch (Exception e)
                {
                    throw e;
                }

            }
        }

        public IEnumerable<HistoricoExclusao> BuscarRegistrosDeHistorico()
        {
            try
            {
                using (var contexto = new CippContexto())
                {
                    var resultado = contexto.HistoricoExclusoes.ToList();

                    return resultado.Select(h => new HistoricoExclusao
                    {
                        CodigoOia = h.CodigoOia,
                        Cipp = h.Cipp,
                        DataExclusao = h.DataExclusao
                    });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}