using System.Collections.Generic;
using INMETRO.CIPP.DOMINIO.Interfaces;
using INMETRO.CIPP.DOMINIO.Interfaces.Repositorios;
using INMETRO.CIPP.DOMINIO.Modelos;

namespace INMETRO.CIPP.DOMINIO.Servicos
{
    public class HistoricoExclusaoServico : IHistoricoExclusao
    {
        private readonly IHistoricoExclusaoRepositorio _repositorio;

        public HistoricoExclusaoServico(IHistoricoExclusaoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public bool AdicionarRegistroDeHistoricoDeExclusao(HistoricoExclusao historico)
        {
            return _repositorio.AdicionarRegistroDeHistoricoDeExclusao(historico);
        }

        public IEnumerable<HistoricoExclusao> BuscarRegistrosDeHistorico()
        {
            return _repositorio.BuscarRegistrosDeHistorico();
        }
    }
}