using System;
using System.Collections.Generic;
using System.Linq;
using INMETRO.CIPP.DOMINIO.Interfaces;
using INMETRO.CIPP.DOMINIO.Interfaces.Repositorios;
using INMETRO.CIPP.DOMINIO.Mensagens;
using INMETRO.CIPP.DOMINIO.Modelos;

namespace INMETRO.CIPP.DOMINIO.Servicos
{
    public class HistoricoExclusaoServico : IHistoricoExclusao
    {
        private readonly IHistoricoExclusaoRepositorio _repositorio;
        private readonly IOrganismoRepositorio _organismoRepositorio;

        public HistoricoExclusaoServico(IHistoricoExclusaoRepositorio repositorio, IOrganismoRepositorio organismoRepositorio)
        {
            _repositorio = repositorio;
            _organismoRepositorio = organismoRepositorio;
        }

        public bool AdicionarRegistroDeHistoricoDeExclusao(HistoricoExclusao historico)
        {
            return _repositorio.AdicionarRegistroDeHistoricoDeExclusao(historico);
        }

        public HistoricoExclusao ObterDadosInspecaoPorCipp(string cipp)
        {
            try
            {
                var resultado = _repositorio.ObterDadosInspecaoPorCipp(cipp);
                if (resultado.Id > 0)
                {
                    return resultado;
                }
                return new HistoricoExclusao
                {
                    Mensagem = string.Format(MensagemNegocio.NenhumInspecaoEncontradoParaCodigoCipp, cipp),
                    ExisteExcecao = true
                };
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public HistoricoExclusao ObterInspecaoParaCippECodigoOiaInformado(string codigoOia, string cipp)
        {
            var organismo = _organismoRepositorio.BuscarOrganismoPorId(codigoOia);
            if (organismo.Id <= 0)
                return new HistoricoExclusao
                {
                    Mensagem = string.Format(MensagemNegocio.NaoExisteCodigoOia, codigoOia),
                    ExisteExcecao = true
                };
            var inspecoes = _repositorio.ObterInspecaoPorCodigoOia(codigoOia);

            foreach (var item in inspecoes)
            {
                if (item.Cipp.Equals(cipp))
                {
                    return item;
                }
            }
            return new HistoricoExclusao
            {
                Mensagem = string.Format(MensagemNegocio.NenhumInspecaoEncontradoParaCodigoOiAeCipp, codigoOia, cipp),
                ExisteExcecao = true
            };
        }

        public HistoricoDeInspecoesExcluidas ObterInspecaoPorCodigoOia(string codigoOia)
        {
            try
            {
               

                var organismo = _organismoRepositorio.BuscarOrganismoPorId(codigoOia);
                if (organismo.Id <= 0)
                    return new HistoricoDeInspecoesExcluidas
                    {
                        InspecoesExcluidas = new List<HistoricoExclusao>(),
                        ExcecaoDominio = new ExcecaoDominio
                        {
                            Mensagem = string.Format(MensagemNegocio.NaoExisteCodigoOia, codigoOia),
                            ExisteExcecao = true
                        }
                    };
                var resultado = _repositorio.ObterInspecaoPorCodigoOia(codigoOia).ToList();

                if (resultado.Count > 0)
                {
                    return new HistoricoDeInspecoesExcluidas
                    {
                        InspecoesExcluidas = resultado.Select(s => new HistoricoExclusao
                        {
                            CodigoOia = s.CodigoOia,
                            Cipp = s.Cipp,
                            DataExclusao = s.DataExclusao
                        }),
                        ExcecaoDominio = new ExcecaoDominio
                        {
                            ExisteExcecao = false,
                            Mensagem = string.Empty
                        }
                    };
                }

                return new HistoricoDeInspecoesExcluidas
                {
                    InspecoesExcluidas = new List<HistoricoExclusao>(),
                    ExcecaoDominio = new ExcecaoDominio
                    {
                        Mensagem = string.Format( MensagemNegocio.NenhumInspecaoEncontradoParaCodigoOia, codigoOia),
                        ExisteExcecao = true
                    }
                };
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public HistoricoDeInspecoesExcluidas BuscarRegistrosDeHistorico()
        {
            try
            {
                var resultado = _repositorio.BuscarRegistrosDeHistorico().ToList();
                if (resultado.Count == 0)
                {
                    return new HistoricoDeInspecoesExcluidas
                    {
                       InspecoesExcluidas = new List<HistoricoExclusao>(),
                       ExcecaoDominio = new ExcecaoDominio
                       {
                           ExisteExcecao = true,
                           Mensagem = string.Format(MensagemNegocio.NenhumaInspecaoExcluidaEncontrada)
                       }
                    };
                }

                return new HistoricoDeInspecoesExcluidas
                {
                   InspecoesExcluidas = resultado.Select(d => new HistoricoExclusao
                    {
                        CodigoOia = d.CodigoOia,
                        Cipp = d.Cipp,
                        DataExclusao = d.DataExclusao
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

      
    }
}