using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Tasks2017.Services;

namespace Tasks2017
{
    public class Bootstrap
    {
        public static void Init(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<TasksService>();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(builder.Build());
            config.MapHttpAttributeRoutes();
            config.EnsureInitialized();
        }
    }
}