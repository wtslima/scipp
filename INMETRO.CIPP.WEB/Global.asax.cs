﻿using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using INMETRO.CIPP.DOMINIO.Interfaces;
using INMETRO.CIPP.DOMINIO.Interfaces.Repositorios;
using INMETRO.CIPP.DOMINIO.Servicos;
using INMETRO.CIPP.INFRA.REPOSITORIO.Repositorios;
using INMETRO.CIPP.SERVICOS.Interfaces;
using INMETRO.CIPP.SERVICOS.Servicos;
using INMETRO.CIPP.SHARED.Interfaces;
using INMETRO.CIPP.SHARED.Servicos;
using INMETRO.DIOIS.INSPECAO.WEB;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;

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

            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            container.Register<IDownloadServico, DownloadServico>(Lifestyle.Scoped);
            container.Register<IOrganismoRepositorio, OrganismoRepositorio>(Lifestyle.Scoped);
            container.Register<IOrganismo, OrganismoServico>(Lifestyle.Scoped);
            container.Register<IGerenciarArquivoCompactado, GerenciarArquivoCompactado>(Lifestyle.Scoped);
            container.Register<IGerenciarFtp, GerenciarFtp>(Lifestyle.Scoped);
            container.Register<IGerenciarCsv, GerenciarCsv>(Lifestyle.Scoped);
            container.Register<IInspecao, InspecaoServico>(Lifestyle.Scoped);
            container.Register<IInspecaoRepositorio, InspecaoRepositorio>(Lifestyle.Scoped);
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            //container.RegisterInitializer(GlobalConfiguration.Configuration); (GlobalConfiguration.Configuration);
            container.Verify();
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}
