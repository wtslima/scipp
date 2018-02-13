using System;
using System.Collections.Generic;
using System.Linq;
using INMETRO.CIPP.DOMINIO.Interfaces.Repositorios;
using INMETRO.CIPP.DOMINIO.Modelos;
using INMETRO.CIPP.INFRA.ENTITYFRAMEWORK;

namespace INMETRO.CIPP.INFRA.REPOSITORIO.Repositorios
{
    public class HistoricoDownloadRepositorio : IHistoricoRepositorio
    {
        public bool AdicionarInspecao(Historico historico)
        {
            try
            {
                using (var contexto = new CippContexto())
                {
                    if (historico == null) return false;
                    contexto.Historico.Add(historico);
                    contexto.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public IEnumerable<Historico> BuscarRegistrosDeHistorico()
        {
            try
            {
                using (var contexto = new CippContexto())
                {
                    var consulta = contexto.Historico.ToList();

                    return consulta.Select(d => new Historico
                    {
                        DataDownload = d.DataDownload,
                        InspecaoId = d.InspecaoId
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