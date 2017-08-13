using System;
using System.Net;
using Autofac;
using Tasks2017.Services;
using Tasks2017.Test.Core;
using Tasks2017.Test.Fakes;
using Xunit;

namespace Tasks2017.Test.Spec
{
    public class MessageHandlersFacts : TestBase
    {
        [Fact]
        void should_get_handler_stopwatch()
        {
            Get("handlers/stopwatch");
            Assert.Equal(HttpStatusCode.OK, Response.StatusCode);
            Assert.Equal("Handler stopwatch.", Body());

            var logger = Scope.Resolve<ILogService>() as LogServiceFake;
            Assert.NotNull(logger);
            Assert.Contains("Stopwatch handler, elapsed: {0}, request: {@1}, response: {@2}.", logger.Messages);
        }

        protected override Action<ContainerBuilder> MockDependency()
        {
            return builder =>
            {
                builder.RegisterInstance(new LogServiceFake()).As<ILogService>().SingleInstance();
            };
        }
    }
}