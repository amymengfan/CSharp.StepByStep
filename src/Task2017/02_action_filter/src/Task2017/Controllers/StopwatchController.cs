using System.Net.Http;
using System.Web.Http;
using Task2017.Extensions;
using Task2017.Filters;

namespace Task2017.Controllers
{
    [StopwatchFilter]
    [RoutePrefix("stopwatch")]
    public class StopwatchController : ApiController
    {
        [Route("one")]
        [HttpGet]
        public HttpResponseMessage One()
        {
            return Request.CreateStringContentResponse("Stopwath one.");
        }

        [Route("two")]
        [HttpGet]
        public HttpResponseMessage Two()
        {
            return Request.CreateStringContentResponse("Stopwatch two.");
        }
    }
}