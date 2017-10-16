using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using Castle.Windsor;

namespace ProductService.Installer
{
    internal class WindsorHttpDependencyResolver : System.Web.Http.Dependencies.IDependencyResolver
    {
        private IWindsorContainer container;

        public WindsorHttpDependencyResolver(IWindsorContainer container)
        {
            this.container = container;
        }

        public void Dispose()
        {
            this.container.Dispose();
        }

        public object GetService(Type serviceType)
        {
            return this.container.Kernel.HasComponent(serviceType) ? this.container.Resolve(serviceType) : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this.container.Kernel.ResolveAll(serviceType).Cast<object>();
        }

        public IDependencyScope BeginScope()
        {
            return new WindsorDependencyScope(this.container);
        }

    }
}