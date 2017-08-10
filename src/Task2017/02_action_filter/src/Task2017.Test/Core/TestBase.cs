using System;
using System.Net.Http;
using System.Web.Http;
using Autofac;

// ReSharper disable VirtualMemberCallInConstructor

namespace Task2017.Test.Core
{
    public abstract class TestBase : IDisposable
    {
        protected readonly string BaseUri;
        protected readonly IContainer Scope;
        protected HttpResponseMessage Response;
        readonly HttpClient Client;
        readonly HttpServer Server;

        protected TestBase()
        {
            var config = new HttpConfiguration();

            BaseUri = "http://task.com";
            Scope = new Bootstrap(config).BeforeBuild(MockDependency()).Init();
            Server = new HttpServer(config);
            Client = new HttpClient(Server)
            {
                BaseAddress = new Uri(BaseUri)
            };
        }

        protected virtual Action<ContainerBuilder> MockDependency()
        {
            return builder => { };
        }

        protected HttpResponseMessage Get(string uri)
        {
            Response = Client.SendAsync(new HttpRequestMessage(HttpMethod.Get, uri)).Result;
            return Response;
        }

        protected string Body()
        {
            return Response.Content.ReadAsStringAsync().Result;
        }

        public virtual void Dispose()
        {
            Response?.Dispose();
            Client?.Dispose();
            Server?.Dispose();
        }
    }
}