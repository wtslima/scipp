using System;
using System.Collections.Generic;
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
                    return resultado;
                }
                return new Inspecao
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

        

        public Inspecao ObterInspecaoParaCippECodigoOiaInformado(string codigoOia, string cipp)
        {
            var organismo = _organismoRepositorio.BuscarOrganismoPorId(codigoOia);
            if (organismo.Id <= 0)
                return new Inspecao
                {
                    Mensagem = string.Format(MensagemNegocio.NaoExisteCodigoOia, codigoOia),
                    ExisteExcecao = true
                };
            var inspecoes = _repositorio.ObterInspecaosPorCodigoOia(codigoOia);

            foreach (var item in inspecoes)
            {
                if (item.CodigoCIPP.Equals(cipp))
                {
                    return item;
                }
            }
            return new Inspecao
            {
                Mensagem = string.Format(MensagemNegocio.NenhumInspecaoEncontradoParaCodigoOiAeCipp, codigoOia, cipp),
                ExisteExcecao = true
            };
        }

        public IEnumerable<Inspecao> ObterInspecaoPorCodigoOia(string codigoOia)
        {
            try
            {
                var organismo = _organismoRepositorio.BuscarOrganismoPorId(codigoOia);
                if (organismo.Id <= 0)
                    return new List<Inspecao>
                    {
                        new Inspecao
                        {
                            Mensagem = string.Format(MensagemNegocio.NaoExisteCodigoOia, codigoOia),
                            ExisteExcecao = true
                        }
                    };
                var resultado = _repositorio.ObterInspecaosPorCodigoOia(codigoOia);

                if (resultado.Count > 0)
                {
                    return resultado;
                }

                return new List<Inspecao>
                {
                    new Inspecao
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

        public IEnumerable<Inspecao> ObterTodasInspecoes()
        {
            try
            {
                var resultado = _repositorio.ObterTodasInspecoes();
                return resultado;
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