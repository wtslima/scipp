using INMETRO.CIPP.DOMINIO.Interfaces;
using INMETRO.CIPP.DOMINIO.Interfaces.Repositorios;
using INMETRO.CIPP.DOMINIO.Modelos;

namespace INMETRO.CIPP.DOMINIO.Servicos
{
    public class InspecaoServico : IInspecao
    {
        private readonly IInspecaoRepositorio _repositorio;
        public InspecaoServico(IInspecaoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public bool AdicionarDadosInspecao(Inspecao inspecao)
        {
            var resultado = _repositorio.AdicionarDadosInspecao(inspecao);
            return resultado;
        }

        public Inspecao ObterDadosInspecao(string cipp)
        {
            throw new System.NotImplementedException();
        }

        public bool TemInspecao(string cipp)
        {
            var resultado = _repositorio.BuscarInspecaoPorCodigoCipp(cipp);

            return resultado;
        }

       
    }
}