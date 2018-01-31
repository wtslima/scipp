using INMETRO.CIPP.SERVICOS.Interfaces;
using INMETRO.CIPP.SERVICOS.Servicos;
using SimpleInjector;

namespace INMETRO.CIPP.INFRA.IoC.Modulos
{
    public class ServicoInject
    {
        public Container Register(Container container)
        {
            container.Register<DownloadServico, DownloadServico>(Lifestyle.Scoped);
            return container;
        }
    }
}