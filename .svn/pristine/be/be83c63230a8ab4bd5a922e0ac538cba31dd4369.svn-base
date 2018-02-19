using System.Collections.Generic;
using INMETRO.CIPP.DOMINIO.Interfaces;
using INMETRO.CIPP.DOMINIO.Interfaces.Repositorios;
using INMETRO.CIPP.DOMINIO.Modelos;

namespace INMETRO.CIPP.DOMINIO.Servicos
{
    public class HistoricoServico : IHistorico
    {
        private readonly IHistoricoRepositorio _repositorio;
        public HistoricoServico(IHistoricoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public bool AdicionarInspecao(Historico historico)
        {
            return _repositorio.AdicionarInspecao(historico);
        }

        public IEnumerable<Historico> BuscarRegistrosDeHistorico()
        {
            return _repositorio.BuscarRegistrosDeHistorico();
        }
    }
}