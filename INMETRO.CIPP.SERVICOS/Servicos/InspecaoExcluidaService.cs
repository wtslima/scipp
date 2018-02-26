using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using INMETRO.CIPP.DOMINIO.Interfaces;
using INMETRO.CIPP.DOMINIO.Interfaces.Repositorios;
using INMETRO.CIPP.DOMINIO.Modelos;
using INMETRO.CIPP.SERVICOS.Interfaces;
using INMETRO.CIPP.SERVICOS.ModelService;
using INMETRO.CIPP.SHARED.Interfaces;

namespace INMETRO.CIPP.SERVICOS.Servicos
{
    public class InspecaoExcluidaService : IHistoricoInspecaoExcluidaServico
    {
        private readonly IHistoricoExclusao _historicoInspecaoExcluida;
        private readonly IOrganismoRepositorio _organismoRepositorio;
        private readonly IGerenciarFtp _ftp;

        public InspecaoExcluidaService(IHistoricoExclusao hIstoricoInspecaoExcluida, IOrganismoRepositorio organismoRepositorio, IGerenciarFtp ftp)
        {
            _historicoInspecaoExcluida = hIstoricoInspecaoExcluida;
            _organismoRepositorio = organismoRepositorio;
            _ftp = ftp;
        }
          public async Task<bool> ExcluirInspecaoPorRotinaAutomatica()
            {
                try
                {
                    var organismos = await _organismoRepositorio.BuscarTodosOrganismos();

                    if (organismos.Count <= 0) return false;
                    foreach (var item in organismos.GroupBy(c => c.FtpInfo))
                    {
                        var lista = ObterInspecoesComMaisDeTrintaDias(item.Key);

                        if (lista.Count > 0)
                        {
                            RemoverInspecaoComMaisDe30Dias(lista.ToList(), item.Key);
                        }
                    }
                    return true;

                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        

        public IEnumerable<HistoricoExclusaoServiceModel> ObterInspecoesExcluidasPorCodigoInformado(string codigoOia, string cipp)
        {
            try
            {
                if (!string.IsNullOrEmpty(codigoOia) && !string.IsNullOrEmpty(cipp))
                {
                    var listaInspecao = new List<HistoricoExclusaoServiceModel>
                    {
                        Conversao.ConverterParaModeloServico(_historicoInspecaoExcluida.ObterInspecaoParaCippECodigoOiaInformado(codigoOia, cipp))
                    };
                    return listaInspecao;
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
                throw e;
            }
        }

        public IEnumerable<HistoricoExclusaoServiceModel> ObterTodasInspecoesExcluidas()
        {
            try
            {
                var lista = _historicoInspecaoExcluida.BuscarRegistrosDeHistorico().ToList();

                var listaInspecao = Conversao.ConverterListaParaModeloService(lista);

                return listaInspecao;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        private void RemoverInspecaoComMaisDe30Dias(List<string> lista, FTPInfo ftpInfo)
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

        private IList<string> ObterInspecoesComMaisDeTrintaDias(FTPInfo ftpInfo)
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

        private void AddRegistrosExclusao(string codigoOia, string diretorio)
        {
            try
            {
                var registroExclusao = new HistoricoExclusaoServiceModel
                {
                    CodigoOia = codigoOia,
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
        private IEnumerable<HistoricoExclusaoServiceModel> BuscarInspecoesPorCodigoOia(string codigoOia)
        {
            try
            {
                var inspecoes = _historicoInspecaoExcluida.ObterInspecaoPorCodigoOia(codigoOia).ToList();

                return Conversao.ConverterListaParaModeloService(inspecoes);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private IEnumerable<HistoricoExclusaoServiceModel> BuscarInspecaoPorCipp(string cipp)
        {
            try
            {
                var listaInspecao = new List<HistoricoExclusaoServiceModel>();

                var inspecao = _historicoInspecaoExcluida.ObterDadosInspecaoPorCipp(cipp);

                listaInspecao.Add(Conversao.ConverterParaModeloServico(inspecao));

                return listaInspecao;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}