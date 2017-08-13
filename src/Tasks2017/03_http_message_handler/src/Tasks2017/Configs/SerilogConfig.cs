using System;
using System.Net.Http;
using Serilog;

namespace Tasks2017.Configs
{
    class SerilogConfig
    {
        static readonly Func<HttpRequestMessage, object> RequestTransform;
        static readonly Func<HttpResponseMessage, object> ResponseTransform;

        static SerilogConfig()
        {
            RequestTransform = e => new
            {
                e.Method.Method,
                Uri = e.RequestUri.AbsolutePath,
            };

            ResponseTransform = e => new
            {
                e.StatusCode
            };
        }

        public static void Register()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .Destructure.ByTransforming(RequestTransform)
                .Destructure.ByTransforming(ResponseTransform)
                .WriteTo.RollingFile("D:\\Logs\\task2017-{Date}.log")
                .CreateLogger();
        }
    }
}