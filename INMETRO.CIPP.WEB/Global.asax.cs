using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using INMETRO.CIPP.INFRA.IoC;
using INMETRO.DIOIS.INSPECAO.WEB;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;
using INMETRO.CIPP.WEB.Agendamento;

namespace INMETRO.CIPP.WEB
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            TarefasAgendadas.TarefasRegistradas();

            var container = new IoC().Register();
           
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.Verify();
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}
