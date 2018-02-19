using INMETRO.CIPP.SERVICOS.Interfaces;
using INMETRO.CIPP.SERVICOS.Servicos;
using SimpleInjector;

namespace INMETRO.CIPP.INFRA.IoC.Modulos
{
    public class ServicoInject
    {
        public Container Register(Container container)
        {
            container.Register<IDownloadServico, DownloadServico>(Lifestyle.Scoped);
            container.Register<IInspecaoServico, InspecaoServico>(Lifestyle.Scoped);
            return container;
        }
    }
}