using System;
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
                    if (inspecao != null)
                    {
                        ctx.Inspecoes.Add(inspecao);
                        ctx.SaveChanges();
                        return true;
                    }

                    return false;
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
                throw;
            }
        }

        public Inspecao ObterDadosInspecao(string Cipp)
        {
            throw new System.NotImplementedException();
        }

        public bool AtualizarDadosInspecao(Inspecao inspecao)
        {
            throw new System.NotImplementedException();
        }

        public bool BuscarInspecaoPorCodigoCipp(string cipp)
        {
            throw new System.NotImplementedException();
        }
    }
}