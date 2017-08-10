using System.Net;
using Task2017.Test.Core;
using Xunit;

namespace Task2017.Test.Spec
{
    public class TaskFacts : TestBase
    {
        [Fact]
        void should_get_task_index()
        {
            Get("task");

            Assert.Equal(HttpStatusCode.OK, Response.StatusCode);
            Assert.Equal("Task Index.", Body());
        }
    }
}