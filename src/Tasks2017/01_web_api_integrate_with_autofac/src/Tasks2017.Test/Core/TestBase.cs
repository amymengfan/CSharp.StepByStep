using System;
using System.Net.Http;
using System.Web.Http;

namespace Tasks2017.Test.Core
{
    public abstract class TestBase : IDisposable
    {
        protected string BaseUri;
        protected HttpResponseMessage Response;
        readonly HttpClient Client;
        readonly HttpServer Server;

        protected TestBase()
        {
            BaseUri = "http://task.com";

            var config = new HttpConfiguration();
            Bootstrap.Init(config);

            Server = new HttpServer(config);
            Client = new HttpClient(Server)
            {
                BaseAddress = new Uri(BaseUri)
            };
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