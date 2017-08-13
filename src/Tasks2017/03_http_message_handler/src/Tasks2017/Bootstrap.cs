using System;
using System.Web.Http;
using Autofac;
using Tasks2017.Configs;

namespace Tasks2017
{
    public class Bootstrap
    {
        readonly HttpConfiguration config;
        Action<ContainerBuilder> beforeBuild;

        public Bootstrap(HttpConfiguration config)
        {
            this.config = config;
        }

        public Bootstrap BeforeBuild(Action<ContainerBuilder> action)
        {
            this.beforeBuild = action;
            return this;
        }

        public IContainer Init()
        {
            var scope = AutofacConfig.Register(beforeBuild);

            WebApiConfig.Register(config, scope);
            SerilogConfig.Register();

            return scope;
        }
    }
}