using INMETRO.CIPP.DOMINIO.Interfaces.Repositorios;
using INMETRO.CIPP.INFRA.REPOSITORIO.Repositorios;
using SimpleInjector;

namespace INMETRO.CIPP.INFRA.IoC.Modulos
{
    public class RepositorioInject
    {
        public Container Register(Container container)
        {
            container.Register<IOrganismoRepositorio, OrganismoRepositorio>(Lifestyle.Scoped);
            container.Register<IInspecaoRepositorio, InspecaoRepositorio>(Lifestyle.Scoped);
            container.Register<IHistoricoRepositorio, HistoricoDownloadRepositorio>(Lifestyle.Scoped);
            container.Register<IHistoricoExclusaoRepositorio, HistoricoExclusaoRepositorio>(Lifestyle.Scoped);
            return container;
        }
    }
}