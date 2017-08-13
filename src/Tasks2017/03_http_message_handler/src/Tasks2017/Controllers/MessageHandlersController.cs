using System.Net.Http;
using System.Web.Http;
using Tasks2017.Extensions;

namespace Tasks2017.Controllers
{
    [RoutePrefix("handlers")]
    public class MessageHandlersController : ApiController
    {
        [Route("stopwatch")]
        [HttpGet]
        public HttpResponseMessage Stopwatch()
        {
            return Request.CreateStringContentResponse("Handler stopwatch.");
        }
    }
}