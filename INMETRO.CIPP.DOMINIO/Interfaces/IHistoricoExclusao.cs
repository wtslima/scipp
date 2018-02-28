using INMETRO.CIPP.DOMINIO.Modelos;

namespace INMETRO.CIPP.DOMINIO.Interfaces
{
    public interface IHistoricoExclusao
    {
        bool AdicionarRegistroDeHistoricoDeExclusao(HistoricoExclusao historico);

        HistoricoExclusao ObterDadosInspecaoPorCipp(string cipp);
      
        HistoricoExclusao ObterInspecaoParaCippECodigoOiaInformado(string codigoOia, string cipp);

        HistoricoDeInspecoesExcluidas ObterInspecaoPorCodigoOia(string codigoOia);

        HistoricoDeInspecoesExcluidas BuscarRegistrosDeHistorico();
    }
}
