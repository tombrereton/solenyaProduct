using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;
using ProductService.Installer;

namespace ProductService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            ConfigureContainer(config);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Use json not XML
            config.Formatters.JsonFormatter.SupportedMediaTypes
                .Add(new MediaTypeHeaderValue("text/html"));


            // Enable Cors
            config.EnableCors();
        }

        private static void ConfigureContainer(HttpConfiguration config)
        {
            var container = ContainerInstaller.Init();

            config.DependencyResolver = new WindsorHttpDependencyResolver(container);
        }
    }
}
