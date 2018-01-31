﻿using INMETRO.CIPP.INFRA.IoC.Modulos;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;

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
            Container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

            Container = new DominioInject().Register(Container);
            Container = new RepositorioInject().Register(Container);
            Container = new ServicoInject().Register(Container);
            Container = new SharedInject().Register(Container);

            return Container;
        }
    }
}