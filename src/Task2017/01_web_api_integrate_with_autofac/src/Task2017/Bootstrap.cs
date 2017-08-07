using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Task2017.Services;

namespace Task2017
{
    public class Bootstrap
    {
        public static void Init(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<TaskService>();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(builder.Build());
            config.MapHttpAttributeRoutes();
            config.EnsureInitialized();
        }
    }
}