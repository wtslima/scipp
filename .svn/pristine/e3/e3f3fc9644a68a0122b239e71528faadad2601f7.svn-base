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