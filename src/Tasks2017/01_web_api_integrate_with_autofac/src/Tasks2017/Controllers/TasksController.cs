using System.Net.Http;
using System.Web.Http;
using Tasks2017.Extensions;
using Tasks2017.Services;

namespace Tasks2017.Controllers
{
    [RoutePrefix("tasks")]
    public class TasksController : ApiController
    {
        readonly TasksService tasksService;

        public TasksController(TasksService tasksService)
        {
            this.tasksService = tasksService;
        }

        [Route("")]
        [HttpGet]
        public HttpResponseMessage Index()
        {
            var copy = tasksService.Copy("Tasks Index.");
            return Request.CreateStringContentResponse(copy);
        }
    }
}