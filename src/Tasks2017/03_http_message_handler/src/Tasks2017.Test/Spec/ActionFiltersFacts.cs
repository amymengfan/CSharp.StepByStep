using System;
using System.Net;
using Autofac;
using Tasks2017.Services;
using Tasks2017.Test.Core;
using Tasks2017.Test.Fakes;
using Xunit;

namespace Tasks2017.Test.Spec
{
    public class ActionFiltersFacts : TestBase
    {
        [Fact]
        void should_get_filters_stopwatch_one()
        {
            Get("filters/stopwatch/1");
            Assert.Equal(HttpStatusCode.OK, Response.StatusCode);
            Assert.Equal("Filter stopwatch 1.", Body());

            var logger = Scope.Resolve<ILogService>() as LogServiceFake;
            Assert.NotNull(logger);
            Assert.Contains("Stopwatch filter, elapsed: {0}, request: {@1}, response: {@2}.", logger.Messages);
        }

        [Fact]
        void should_get_filters_stopwatch_two()
        {
            Get("filters/stopwatch/2");
            Assert.Equal(HttpStatusCode.OK, Response.StatusCode);
            Assert.Equal("Filter stopwatch 2.", Body());

            var logger = Scope.Resolve<ILogService>() as LogServiceFake;
            Assert.NotNull(logger);
            Assert.Contains("Stopwatch filter, elapsed: {0}, request: {@1}, response: {@2}.", logger.Messages);
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