using System;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Task2017.Services;

namespace Task2017
{
    public class Bootstrap
    {
        readonly HttpConfiguration config;
        Action<ContainerBuilder> beforeBuild;

        public Bootstrap(HttpConfiguration config)
        {
            this.config = config;
            this.beforeBuild = builder => { };
        }

        public Bootstrap BeforeBuild(Action<ContainerBuilder> action)
        {
            this.beforeBuild = action;
            return this;
        }

        public IContainer Init()
        {
            Serilog.Init();
            return InitScope();
        }

        IContainer InitScope()
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<TasksService>().InstancePerLifetimeScope();
            builder.RegisterType<LogService>().As<ILogService>().SingleInstance();
            beforeBuild(builder);

            var scope = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(scope);
            config.MapHttpAttributeRoutes();
            config.EnsureInitialized();
            return scope;
        }
    }
}