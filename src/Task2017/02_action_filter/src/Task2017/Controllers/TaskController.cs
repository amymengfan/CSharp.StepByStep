using System.Net.Http;
using System.Web.Http;
using Task2017.Extensions;
using Task2017.Services;

namespace Task2017.Controllers
{
    [RoutePrefix("task")]
    public class TaskController : ApiController
    {
        readonly TaskService taskService;

        public TaskController(TaskService taskService)
        {
            this.taskService = taskService;
        }

        [Route("")]
        [HttpGet]
        public HttpResponseMessage Index()
        {
            var copy = taskService.Copy("Task Index.");
            return Request.CreateStringContentResponse(copy);
        }
    }
}