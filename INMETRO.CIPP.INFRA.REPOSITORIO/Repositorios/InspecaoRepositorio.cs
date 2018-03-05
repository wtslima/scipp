using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Globalization;
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
                using (var ctx = new CippContexto())
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

        public Inspecao ObterDadosInspecao(string cipp)
        {
            try
            {
                using (var contexto = new CippContexto())
                {
                    var consulta = contexto.Inspecoes.FirstOrDefault(s => s.CodigoCIPP.Equals(cipp));

                    return consulta ?? new Inspecao();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool BuscarInspecaoPorCodigoCipp(string cipp)
        {
            try
            {
                using (var contexto = new CippContexto())
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

        public IList<Inspecao> ObterInspecaosPorCodigoOia(string codigoOia)
        {
            using (var ctx = new CippContexto())
            {
                try
                {
                    if (string.IsNullOrEmpty(codigoOia)) return new List<Inspecao>();

                    var resultado = ctx.Inspecoes
                        .Where(s => s.CodigoOIA == codigoOia)
                        .ToList()
                        .Select(
                    item => new Inspecao
                    {
                        Id = item.Id,
                        CodigoOIA = item.CodigoOIA,
                        CodigoCIPP = item.CodigoCIPP,
                        NumeroEquipamento = item.NumeroEquipamento,
                        PlacaLicenca = item.PlacaLicenca,
                        DataInspecao = item.DataInspecao
                    }).ToList();

                    return resultado;
                }
                catch (Exception e)
                {
                    throw e;
                }

            }

        }

       

        public IEnumerable<Inspecao> ObterInspecaosPorPlacaLicenca(string placaLicenca)
        {
            using (var contexto = new CippContexto())
            {
                try
                {
                    if (string.IsNullOrEmpty(placaLicenca)) return new List<Inspecao>();

                    return contexto.Inspecoes.Where(s => s.PlacaLicenca == placaLicenca ).ToList()
                        .Select(
                            item => new Inspecao
                            {
                                Id = item.Id,
                                CodigoOIA = item.CodigoOIA,
                                CodigoCIPP = item.CodigoCIPP,
                                NumeroEquipamento = item.NumeroEquipamento,
                                PlacaLicenca = item.PlacaLicenca,
                                DataInspecao = item.DataInspecao.Date
                            }).ToList();

                }
                catch (Exception e)
                {
                    throw e;
                }

            }

        }

        public IEnumerable<Inspecao> ObterInspecoesPorDataInspecao(DateTime dataInspecao)
        {
            using (var contexto = new CippContexto())
            {
                try
                {
                    var str = Convert.ToString(dataInspecao);
                    DateTime condate2 = DateTime.ParseExact(str, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None);
                    var consulta = contexto.Inspecoes.Where(s => s.DataInspecao == condate2);

                    return consulta.Select(i => new Inspecao
                    {
                        Id = i.Id,
                        CodigoCIPP = i.CodigoCIPP,
                        CodigoOIA = i.CodigoOIA,
                        NumeroEquipamento = i.NumeroEquipamento,
                        PlacaLicenca = i.PlacaLicenca,
                        DataInspecao = i.DataInspecao
                        
                    });

                }
                catch (Exception e)
                {
                    throw e;
                }

            }
        }

        public IEnumerable<Inspecao> ObterTodasInspecoes()
        {
            using (var contexto = new CippContexto())
            {
                try
                {

                    var consulta = contexto.Inspecoes.ToList();

                    return consulta.Select(i => new Inspecao
                    {
                        Id = i.Id,
                        CodigoCIPP = i.CodigoCIPP,
                        CodigoOIA = i.CodigoOIA,
                        NumeroEquipamento = i.NumeroEquipamento,
                        PlacaLicenca = i.PlacaLicenca,
                        DataInspecao = i.DataInspecao.Date
                      
                       
                    });

                }
                catch (Exception e)
                {
                    throw e;
                }

            }
        }
        
    }
}