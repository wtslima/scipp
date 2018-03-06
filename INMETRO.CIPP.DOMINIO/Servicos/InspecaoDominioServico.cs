using System;
using System.Collections.Generic;
using System.Linq;
using INMETRO.CIPP.DOMINIO.Interfaces;
using INMETRO.CIPP.DOMINIO.Interfaces.Repositorios;
using INMETRO.CIPP.DOMINIO.Mensagens;
using INMETRO.CIPP.DOMINIO.Modelos;

namespace INMETRO.CIPP.DOMINIO.Servicos
{
    public class InspecaoDominioServico : IInspecaoDominioService
    {
        private readonly IInspecaoRepositorio _repositorio;
        private readonly IOrganismoRepositorio _organismoRepositorio;
        private int _codigo = 0;
        public InspecaoDominioServico(IInspecaoRepositorio repositorio, IOrganismoRepositorio organismoRepositorio)
        {
            _repositorio = repositorio;
            _organismoRepositorio = organismoRepositorio;
           
        }
        public bool AdicionarDadosInspecao(Inspecao inspecao)
        {
            try
            {
                var resultado = _repositorio.AdicionarDadosInspecao(inspecao);
               
                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public Inspecao ObterDadosInspecaoPorCipp(string cipp)
        {
            try
            {
                var resultado = _repositorio.ObterDadosInspecao(cipp);
                if (resultado.Id > 0)
                {
                    resultado.ExcecaoDominio = new ExcecaoDominio
                    {
                        ExisteExcecao = false,
                        Mensagem = string.Empty
                    };
                    return resultado;
                }
                return new Inspecao
                {
                    ExcecaoDominio = new ExcecaoDominio
                    {
                        ExisteExcecao = true,
                        Mensagem = string.Format(MensagemNegocio.NenhumInspecaoEncontradoParaCodigoCipp, cipp)
                    }
                };
            }
            catch (Exception e)
            {
                throw e;
            }

        }


        public Inspecao ObterInspecaoParaCippECodigoOiaInformado(string codigoOia, string cipp)
        {
            try
            {
                _codigo = Convert.ToInt32(codigoOia);
                var organismo = _organismoRepositorio.BuscarOrganismoPorId(_codigo);
                if (organismo.Id <= 0)
                {
                    return new Inspecao
                    {

                        ExcecaoDominio = new ExcecaoDominio
                        {
                            ExisteExcecao = true,
                            Mensagem = string.Format(MensagemNegocio.NaoExisteCodigoOia, codigoOia)

                        }

                    };
                }
                var inspecoes = _repositorio.ObterInspecaosPorCodigoOia(_codigo);

                foreach (var item in inspecoes)
                {
                    if (item.CodigoCIPP.Equals(cipp))
                    {
                        item.ExcecaoDominio = new ExcecaoDominio
                        {
                            Mensagem = string.Empty,
                            ExisteExcecao = false
                        };
                        return item;
                    }
                }

                return new Inspecao
                {

                    ExcecaoDominio = new ExcecaoDominio
                    {
                        ExisteExcecao = true,
                        Mensagem = string.Format(MensagemNegocio.InspecaoJaGravadaParaCippEOia)

                    }

                };
            }
            catch (Exception e)
            {
                throw e;
            }
           
        
    }

    public InspecoesGravadas ObterInspecaoPorCodigoOia(string codigoOia)
    {
        try
        {
            var codigo = Convert.ToInt32(codigoOia);
            var organismo = _organismoRepositorio.BuscarOrganismoPorId(codigo);
            if (organismo.Id <= 0)
                return new InspecoesGravadas
                {
                    Inspecoes = new List<Inspecao>(),
                    ExcecaoDominio = new ExcecaoDominio
                    {
                        Mensagem = string.Format(MensagemNegocio.NaoExisteCodigoOia, codigoOia),
                        ExisteExcecao = true
                    }
                };
            var resultado = _repositorio.ObterInspecaosPorCodigoOia(codigo);

            if (resultado.Count > 0)
            {
                return new InspecoesGravadas
                {
                    Inspecoes = resultado.Select(d => new Inspecao
                    {
                        CodigoOIA = d.CodigoOIA,
                        CodigoCIPP = d.CodigoCIPP,
                        PlacaLicenca = d.PlacaLicenca,
                        NumeroEquipamento = d.NumeroEquipamento,
                        DataInspecao = d.DataInspecao
                      
                    }),
                    ExcecaoDominio = new ExcecaoDominio
                    {
                        ExisteExcecao = false,
                        Mensagem = string.Empty
                    }
                };
            }

            return new InspecoesGravadas
            {
                Inspecoes = new List<Inspecao>(),
                ExcecaoDominio = new ExcecaoDominio
                {
                    Mensagem = string.Format(MensagemNegocio.NenhumInspecaoEncontradoParaCodigoOia, codigoOia),
                    ExisteExcecao = true
                }
            };
        }
        catch (Exception e)
        {
            throw e;
        }

    }

        public InspecoesGravadas ObterInspecaoPorPlacaLicenca(string placa)
        {
            try
            {
                var resultado = _repositorio.ObterInspecaosPorPlacaLicenca(placa).ToList();
                if (resultado.Count > 0)
                {
                    return new InspecoesGravadas
                    {
                        Inspecoes = resultado.Select(d => new Inspecao
                        {
                            CodigoOIA = d.CodigoOIA,
                            CodigoCIPP = d.CodigoCIPP,
                            PlacaLicenca = d.PlacaLicenca,
                            NumeroEquipamento = d.NumeroEquipamento,
                            DataInspecao = d.DataInspecao
                           
                        }),
                        ExcecaoDominio = new ExcecaoDominio
                        {
                            ExisteExcecao = false,
                            Mensagem = string.Empty
                        }
                    };
                }

                return new InspecoesGravadas
                {
                    Inspecoes = new List<Inspecao>(),
                    ExcecaoDominio = new ExcecaoDominio
                    {
                        Mensagem = string.Format(MensagemNegocio.NenhumaInspecaoPlcaInformada, placa),
                        ExisteExcecao = true
                    }
                };
            }
            catch (Exception e)
            {
                
                throw e;
            }
        }

        public InspecoesGravadas ObterInspecaoPorDataInspecao(DateTime dataInspecao)
        {
            try
            {
                var resultado = _repositorio.ObterInspecoesPorDataInspecao(dataInspecao).ToList();

                if (resultado.Count <= 0)
                {
                    return new InspecoesGravadas
                    {
                        Inspecoes = new List<Inspecao>(),
                        ExcecaoDominio = new ExcecaoDominio
                        {
                            ExisteExcecao = true,
                            Mensagem = string.Format(MensagemNegocio.NenhumaInspecaoParaPeriodoInformado)
                        }
                    };
                }

                return new InspecoesGravadas
                {
                    Inspecoes = resultado.Select(d => new Inspecao
                    {
                        CodigoOIA = d.CodigoOIA,
                        CodigoCIPP = d.CodigoCIPP,
                        PlacaLicenca = d.PlacaLicenca,
                        NumeroEquipamento = d.NumeroEquipamento,
                        DataInspecao = d.DataInspecao
                      
                    }),
                    ExcecaoDominio = new ExcecaoDominio
                    {
                        ExisteExcecao = false,
                        Mensagem = string.Empty
                    }
                };
            }
            catch (Exception e)
            {
                
                throw e;
            }
            
        }

        public InspecoesGravadas ObterTodasInspecoes()
    {
        try
        {
            var resultado = _repositorio.ObterTodasInspecoes().ToList();

            if (resultado.Count == 0)
            {
                return new InspecoesGravadas
                {
                    Inspecoes = new List<Inspecao>(),
                    ExcecaoDominio = new ExcecaoDominio
                    {
                        ExisteExcecao = true,
                        Mensagem = string.Format(MensagemNegocio.NenhumaInspecaoEncontrada)
                    }
                };
            }
            return new InspecoesGravadas
            {
                Inspecoes = resultado.Select(d => new Inspecao
                {
                    CodigoOIA = d.CodigoOIA,
                    CodigoCIPP = d.CodigoCIPP,
                    PlacaLicenca = d.PlacaLicenca,
                    NumeroEquipamento = d.NumeroEquipamento,
                    DataInspecao = d.DataInspecao
                   
                }),
                ExcecaoDominio = new ExcecaoDominio
                {
                    ExisteExcecao = false,
                    Mensagem = string.Empty
                }
            };
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public bool TemInspecao(string cipp)
    {
        try
        {
            var resultado = _repositorio.BuscarInspecaoPorCodigoCipp(cipp);

            return resultado;
        }
        catch (Exception e)
        {
            throw e;
        }

    }


}
}