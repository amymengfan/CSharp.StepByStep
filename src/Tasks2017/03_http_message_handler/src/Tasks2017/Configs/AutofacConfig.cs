using System;
using System.Reflection;
using Autofac;
using Autofac.Integration.WebApi;
using Tasks2017.Services;

namespace Tasks2017.Configs
{
    class AutofacConfig
    {
        public static IContainer Register(Action<ContainerBuilder> beforeBuild)
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<TasksService>().InstancePerLifetimeScope();
            builder.RegisterType<LogService>().As<ILogService>().SingleInstance();

            beforeBuild?.Invoke(builder);

            return builder.Build();
        }
    }
}