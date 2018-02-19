
using INMETRO.CIPP.SHARED.Interfaces;
using INMETRO.CIPP.SHARED.Servicos;
using SimpleInjector;

namespace INMETRO.CIPP.INFRA.IoC.Modulos
{
    public class SharedInject
    {
        public Container Register(Container container)
        {
            container.Register<IGerenciarFtp, GerenciarFtp>(Lifestyle.Scoped);
            container.Register<IGerenciarArquivoCompactado, GerenciarArquivoCompactado>(Lifestyle.Scoped);
            container.Register<IGerenciarCsv, GerenciarCsv>(Lifestyle.Scoped);
            return container;
        }
    }
}