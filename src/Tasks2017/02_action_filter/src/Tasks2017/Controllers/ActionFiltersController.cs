using System.Net.Http;
using System.Web.Http;
using Tasks2017.Extensions;
using Tasks2017.Filters;

namespace Tasks2017.Controllers
{
    [StopwatchFilter]
    [RoutePrefix("filters")]
    public class ActionFiltersController : ApiController
    {
        [Route("stopwatch/1")]
        [HttpGet]
        public HttpResponseMessage StopwatchOne()
        {
            return Request.CreateStringContentResponse("Filters stopwatch 1.");
        }

        [Route("stopwatch/2")]
        [HttpGet]
        public HttpResponseMessage StopwatchTwo()
        {
            return Request.CreateStringContentResponse("Filters stopwatch 2.");
        }
    }
}