using System.Net.Http;
using System.Web.Http;
using Task2017.Extensions;

namespace Task2017.Controllers
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