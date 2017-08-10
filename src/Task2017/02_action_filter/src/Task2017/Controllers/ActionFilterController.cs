using System.Net.Http;
using System.Web.Http;
using Task2017.Extensions;
using Task2017.Filters;

namespace Task2017.Controllers
{
    [StopwatchFilter]
    [RoutePrefix("filters")]
    public class ActionFilterController : ApiController
    {
        [Route("stopwatch/1")]
        [HttpGet]
        public HttpResponseMessage StopwatchOne()
        {
            return Request.CreateStringContentResponse("Stopwath one.");
        }

        [Route("stopwatch/2")]
        [HttpGet]
        public HttpResponseMessage StopwatchTwo()
        {
            return Request.CreateStringContentResponse("Stopwatch two.");
        }
    }
}