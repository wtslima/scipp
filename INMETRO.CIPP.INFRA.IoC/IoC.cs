using SimpleInjector.Integration.Web;
using INMETRO.CIPP.INFRA.IoC.Modulos;
using SimpleInjector;

namespace INMETRO.CIPP.INFRA.IoC
{
  
    
    public class IoC
    {
        public Container Container { get; set; }
        public IoC()
        {
            Container = new Container();
        }

        public Container Register()
        {
            Container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            Container = new DominioInject().Register(Container);
            Container = new RepositorioInject().Register(Container);
            Container = new ServicoInject().Register(Container);
            Container = new SharedInject().Register(Container);

            return Container;
        }
    }
}