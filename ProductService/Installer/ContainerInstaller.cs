using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using ProductService.DataStore;

namespace ProductService.Installer
{
    public class ContainerInstaller
    {
        public static WindsorContainer Init()
        {
            var container = new WindsorContainer();

            container.Register(
                Component
                    .For<IProductsDataStore>()
                    .ImplementedBy<ProductDataStore>()
                    .LifestyleTransient());

            container.Register(
                Classes
                    .FromThisAssembly()
                    .BasedOn<ApiController>()
                    .LifestyleScoped());

            return container;
        }
    }
}