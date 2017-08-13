using System.Net;
using Tasks2017.Test.Core;
using Xunit;

namespace Tasks2017.Test.Spec
{
    public class TasksFacts : TestBase
    {
        [Fact]
        void should_get_tasks_index()
        {
            Get("tasks");

            Assert.Equal(HttpStatusCode.OK, Response.StatusCode);
            Assert.Equal("Tasks Index.", Body());
        }
    }
}