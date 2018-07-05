using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
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
                    var consulta = contexto.Inspecoes.FirstOrDefault(s => s.CodigoCipp.Equals(cipp));

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

                    var resultado = contexto.Inspecoes.FirstOrDefault(s => s.CodigoCipp.Equals(cipp));

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

                    var resultado = ctx.Inspecoes
                        .Where(s => s.CodigoOIA.Contains(codigoOia)).OrderBy(s => s.DataInspecao)
                        .ToList()
                        .Select(
                            item => new Inspecao
                            {
                                Id = item.Id,
                                CodigoOIA = item.CodigoOIA,
                                CodigoCipp = item.CodigoCipp,
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

                    return contexto.Inspecoes.Where(s => s.PlacaLicenca == placaLicenca).ToList()
                        .Select(
                            item => new Inspecao
                            {
                                Id = item.Id,
                                CodigoOIA = item.CodigoOIA,
                                CodigoCipp = item.CodigoCipp,
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
                    var data = $"{dataInspecao:yyyy-MM-dd}";

                    return  BuscarInspecaoPorData(data);

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
                        CodigoCipp = i.CodigoCipp,
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


        private List<Inspecao> BuscarInspecaoPorData(string data)
        {
            string sql = string.Empty;
            sql =
                "SELECT *  FROM [scipp].[dbo].[TB_INSPECAO_CIPP] where DAT_INSPECAO ='" + data + "' ORDER BY CDN_CIPP";
            var connectionString = ConfigurationManager.ConnectionStrings["CIPP_CONTEXTO"].ConnectionString;

            var list = new List<Inspecao>();
            using (var connection = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    var result = cmd.ExecuteReader();

                    while (result.Read())
                    {

                        var inspecao = new Inspecao
                        {
                            Id = (int) result["IDT_INSPECAO"],
                            CodigoCipp = result["CDN_CIPP"].ToString(),
                            CodigoOIA = result["CDA_CODIGO_OIA"].ToString(),
                            NumeroEquipamento =  result["NUM_EQUIPAMENTO"].ToString(),
                            PlacaLicenca = result["DES_PLACA_LICENCA"].ToString(),
                            DataInspecao = Convert.ToDateTime(result["DAT_INSPECAO"])
                        };
                        list.Add(inspecao);
                    }


                    connection.Close();
                }
                return list;
            }
        }
    }
}