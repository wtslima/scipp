using INMETRO.CIPP.DOMINIO.Interfaces.Repositorios;
using INMETRO.CIPP.DOMINIO.Modelos;
using INMETRO.CIPP.INFRA.REPOSITORIO.Repositorios;
using Moq;
using NUnit.Framework;

namespace INMETRO.CIPP.TESTE.Unidade.Repositorio
{
    [TestFixture]
    public class RepositorioOrganismoTeste
    {
        private Mock<IOrganismoRepositorio> _mockOrganismoRepositorio;
        public OrganismoRepositorio OrganismoRepositorio;

        public RepositorioOrganismoTeste(Mock<IOrganismoRepositorio> mockOrganismoRepositorio)
        {
            this._mockOrganismoRepositorio = mockOrganismoRepositorio;
        }

        public void BuscarOrganismoPorCodigoOIA_RetornarResultadoEsperado(string codigoOIA, Organismo expectedResult)
        {
            _mockOrganismoRepositorio = new Mock<IOrganismoRepositorio>(MockBehavior.Strict);
            _mockOrganismoRepositorio.Setup(p => p.BuscarOrganismoPorId(codigoOIA)).Returns(expectedResult);


            OrganismoRepositorio = new OrganismoRepositorio();
            var result = OrganismoRepositorio.BuscarOrganismoPorId(codigoOIA);

            Assert.That(result, Is.EqualTo(expectedResult));

            _mockOrganismoRepositorio.VerifyAll();
        }
    }
}