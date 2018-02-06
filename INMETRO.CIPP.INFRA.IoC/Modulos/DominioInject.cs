using INMETRO.CIPP.DOMINIO.Interfaces;
using INMETRO.CIPP.DOMINIO.Servicos;
using SimpleInjector;


namespace INMETRO.CIPP.INFRA.IoC.Modulos
{
    public class DominioInject
    {
        public Container Register(Container container)
        {
            container.Register<IOrganismo, OrganismoServico>(Lifestyle.Scoped);
            container.Register<IInspecao, InspecaoServico>(Lifestyle.Scoped);
            container.Register<IHistorico, HistoricoServico>(Lifestyle.Scoped);
            container.Register<IHistoricoExclusao, HistoricoExclusaoServico>(Lifestyle.Scoped);
            return container;
        }
    }
}