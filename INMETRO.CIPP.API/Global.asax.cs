using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using INMETRO.CIPP.DOMINIO.Interfaces;
using INMETRO.CIPP.DOMINIO.Interfaces.Repositorios;
using INMETRO.CIPP.DOMINIO.Servicos;
using INMETRO.CIPP.INFRA.ENTITYFRAMEWORK;
using INMETRO.CIPP.INFRA.IoC;
using INMETRO.CIPP.INFRA.REPOSITORIO.Repositorios;
using INMETRO.CIPP.SERVICOS.Interfaces;
using INMETRO.CIPP.SERVICOS.Servicos;
using INMETRO.CIPP.SHARED.Interfaces;
using INMETRO.CIPP.SHARED.Servicos;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using SimpleInjector.Integration.WebApi;

namespace INMETRO.CIPP.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var db = new Contexto();

            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            container.Register<IDownloadServico, DownloadServico>(Lifestyle.Scoped);
            container.Register<IOrganismoRepositorio, OrganismoRepositorio>(Lifestyle.Scoped);
            container.Register<IGerenciarFtp, GerenciarFtp>(Lifestyle.Scoped);
            container.Register<IGerenciarArquivoCompactado, Descompactar>(Lifestyle.Scoped);
            container.Register<IGerenciarCsv, GerenciarCsv>(Lifestyle.Scoped);
            container.Register<IInspecao, InspecaoServico>(Lifestyle.Scoped);
            container.Register<IInspecaoRepositorio, InspecaoRepositorio>(Lifestyle.Scoped);
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.Verify();
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));

            
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            container.Verify();
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}
