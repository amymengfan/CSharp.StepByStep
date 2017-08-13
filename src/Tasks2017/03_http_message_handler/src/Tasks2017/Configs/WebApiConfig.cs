using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Tasks2017.Handlers;

namespace Tasks2017.Configs
{
    class WebApiConfig
    {
        public static void Register(HttpConfiguration config, ILifetimeScope scope)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(scope);
            config.MapHttpAttributeRoutes();
            config.MessageHandlers.Add(new StopwatchHandler());
            config.EnsureInitialized();
        }
    }
}