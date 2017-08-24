using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using SessionModuleClient;

namespace ServiceModule.Controllers
{
    public class DefaultController : ApiController
    {
        [HttpGet]
        [LoginRequired]
        public HttpResponseMessage Get()
        {
            var response = Request.CreateResponse(HttpStatusCode.OK);

            #region Please implement the following code

            // This method will create response based on current logged in user.

            var message = $"<h1>This is our awesome API about page for {Request.GetUserSession().UserFullname}</h1>";
            response.Content = new StringContent(message, Encoding.UTF8);

            #endregion

            return response;
        }
    }
}