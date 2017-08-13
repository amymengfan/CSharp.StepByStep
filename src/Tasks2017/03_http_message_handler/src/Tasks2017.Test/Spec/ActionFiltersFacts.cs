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
            Assert.Equal("Filters stopwatch 1.", Body());

            var logger = Scope.Resolve<ILogService>() as LogServiceFake;
            Assert.NotNull(logger);
            Assert.Contains("stopwatch start", logger.Messages[0]);
            Assert.Contains("stopwatch end", logger.Messages[1]);
        }

        [Fact]
        void should_get_filters_stopwatch_two()
        {
            Get("filters/stopwatch/2");
            Assert.Equal(HttpStatusCode.OK, Response.StatusCode);
            Assert.Equal("Filters stopwatch 2.", Body());

            var logger = Scope.Resolve<ILogService>() as LogServiceFake;
            Assert.NotNull(logger);
            Assert.Contains("stopwatch start", logger.Messages[0]);
            Assert.Contains("stopwatch end", logger.Messages[1]);
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