namespace ProductService.Installer
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Web;
    using System.Web.Http;

    using Castle.MicroKernel.Registration;
    using Castle.Windsor;

    using Microsoft.ApplicationInsights.Channel;

    using ProductService.DataStore;

    public class ContainerInstaller
    {
        public static WindsorContainer Init()
        {
            var container = new WindsorContainer();

            container.Register(
                Component.For<IProductsDataStore>().ImplementedBy<ProductDataStore>().DependsOn(
                    Dependency.OnAppSettingsValue("endPointUrl", "DocumentDBEndpoint"),
                    Dependency.OnAppSettingsValue("primaryKey", "DocumentDBPrimaryKey")).LifestyleTransient());

            container.Register(Classes.FromThisAssembly().BasedOn<ApiController>().LifestyleScoped());

            return container;
        }
    }
}