using System;
using System.Net;
using Autofac;
using Task2017.Services;
using Task2017.Test.Core;
using Task2017.Test.Fakes;
using Xunit;

namespace Task2017.Test.Spec
{
    public class StopwatchFacts : TestBase
    {
        [Fact]
        void should_get_stopwatch_one()
        {
            Get("stopwatch/one");
            Assert.Equal(HttpStatusCode.OK, Response.StatusCode);

            var logger = Scope.Resolve<ILogService>() as LogServiceFake;
            Assert.NotNull(logger);
            Assert.Contains("stopwatch start", logger.Messages[0]);
            Assert.Contains("stopwatch end", logger.Messages[1]);
        }

        [Fact]
        void should_get_stopwatch_two()
        {
            Get("stopwatch/two");
            Assert.Equal(HttpStatusCode.OK, Response.StatusCode);

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