using System.Net.Http;
using System.Web.Http;
using Tasks2017.Extensions;

namespace Tasks2017.Controllers
{
    public class HomeController : ApiController
    {
        [Route("~/")]
        [AcceptVerbs("GET", "POST", "PUT", "PATCH", "DELETE")]
        public HttpResponseMessage Index()
        {
            return Request.CreateStringContentResponse("Home Page !!!");
        }
    }
}