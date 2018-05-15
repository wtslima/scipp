using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using INMETRO.CIPP.DOMINIO.Interfaces;
using INMETRO.CIPP.DOMINIO.Interfaces.Repositorios;
using INMETRO.CIPP.DOMINIO.Modelos;
using INMETRO.CIPP.SERVICOS.Interfaces;
using INMETRO.CIPP.SERVICOS.ModelService;
using INMETRO.CIPP.SHARED.Email;
using INMETRO.CIPP.SHARED.Interfaces;

namespace INMETRO.CIPP.SERVICOS.Servicos
{
    public class InspecaoExcluidaService : IHistoricoInspecaoExcluidaServico
    {
        private readonly IHistoricoExclusao _historicoInspecaoExcluida;
        private readonly IOrganismoRepositorio _organismoRepositorio;
        private readonly IGerenciarFtp _ftp;
        List<String> _listExcecao = new List<string>();


        public InspecaoExcluidaService(IHistoricoExclusao hIstoricoInspecaoExcluida, IOrganismoRepositorio organismoRepositorio, IGerenciarFtp ftp)
        {
            _historicoInspecaoExcluida = hIstoricoInspecaoExcluida;
            _organismoRepositorio = organismoRepositorio;
            _ftp = ftp;
        }
        public async Task<bool> ExcluirInspecaoPorRotinaAutomatica()
        {
            var enviar = new Notificacao();
            try
            {
                var organismos = await _organismoRepositorio.BuscarTodosOrganismos();
               
                
                if (organismos.Count <= 0) return false;
                foreach (var item in organismos)
                {
                    var lista = ObterInspecoesComMaisDeTrintaDias(item.IntegracaoInfo);

                    if (lista.Count > 0)
                    {
                        RemoverInspecaoComMaisDe30Dias(lista.ToList(), item.IntegracaoInfo);
                    }
                }
                return true;

            }
            catch (Exception e)
            {

                enviar.EnviarEmailErroDownloadAutomátivo("wslima@colaborador.inmetro.gov.br", _listExcecao);
             
            }
            return false;
        }


        public HistoricoDeExclusaoModelService ObterInspecoesExcluidasPorCodigoInformado(string codigoOia, string cipp)
        {
            try
            {
                if (!string.IsNullOrEmpty(codigoOia))
                {
                    return Conversao.ConverterParaModeloServico(_historicoInspecaoExcluida.ObterInspecaoParaCippECodigoOiaInformado(codigoOia, cipp));
                }
                if (!string.IsNullOrEmpty(cipp))
                {
                    var inspecao = BuscarInspecaoPorCipp(cipp);
                    return inspecao;
                }

                return BuscarInspecoesPorCodigoOia(codigoOia);
            }
            catch (Exception e)
            {
                _listExcecao.Add(e.Message);
                throw;
            }
            
        }

        public HistoricoDeExclusaoModelService ObterTodasInspecoesExcluidas()
        {
            try
            {
                var resultado = _historicoInspecaoExcluida.BuscarRegistrosDeHistorico();

                var inspecaoExcluida = Conversao.ConverterParaModelService(resultado);


                return inspecaoExcluida;
            }
            catch (Exception e)
            {

                throw e;
            }
        }


        private void AddRegistrosExclusao(string codigoOia, string diretorio)
        {
            try
            {
                
                var registroExclusao = new HistoricoExclusaoServiceModel
                {
                    CodigoOia = codigoOia.ToString(),
                    Cipp = diretorio,
                    DataExclusao = DateTime.Now
                };
                _historicoInspecaoExcluida.AdicionarRegistroDeHistoricoDeExclusao(
                    Conversao.ConverterParaDominio(registroExclusao));
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        private HistoricoDeExclusaoModelService BuscarInspecoesPorCodigoOia(string codigoOia)
        {
            try
            {
                var inspecoes = _historicoInspecaoExcluida.ObterInspecaoPorCodigoOia(codigoOia);

                return Conversao.ConverterParaModelService(inspecoes);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private HistoricoDeExclusaoModelService BuscarInspecaoPorCipp(string cipp)
        {
            try
            {

                var inspecao = _historicoInspecaoExcluida.ObterDadosInspecaoPorCipp(cipp);

                return Conversao.ConverterParaModeloServico(inspecao);

            }
            catch (Exception e)
            {
                throw e;
            }

        }

        private void RemoverInspecaoComMaisDe30Dias(List<string> lista, IntegracaoInfos ftpInfo)
        {
            try
            {
                foreach (var item in lista)
                {
                    if (!_ftp.ExcluirInspecao(ftpInfo, item)) continue;
                    AddRegistrosExclusao(ftpInfo.Organismo.CodigoOIA, item);
                }

            }
            catch (Exception e)
            {
                throw e;
            }

        }

        private IList<string> ObterInspecoesComMaisDeTrintaDias(IntegracaoInfos ftpInfo)
        {
            var listaDiretoriosParaExclusaoComMais30Dias = new List<string>();

            var inspecoesDiretorios = _ftp.ObterListaDiretoriosInspecoesFtp(ftpInfo);


            if (inspecoesDiretorios.Length <= 0) return new List<string>();

            foreach (var inspecao in inspecoesDiretorios)
            {
                var dataDiretorioRemoto = _ftp.ObterDataEntradaFtp(ftpInfo, inspecao);
                if (!TemMaisDe30Dias(dataDiretorioRemoto)) continue;
                listaDiretoriosParaExclusaoComMais30Dias.Add(inspecao);
            }

            return listaDiretoriosParaExclusaoComMais30Dias;

        }

        private bool TemMaisDe30Dias(DateTime fileDate)
        {
            var limite = fileDate.AddDays(30);
            return DateTime.Now > limite;
        }
    }
}