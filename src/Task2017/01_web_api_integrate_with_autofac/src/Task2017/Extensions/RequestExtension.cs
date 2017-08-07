using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Task2017.Extensions
{
    public static class RequestExtension
    {
        public static HttpResponseMessage CreateStringContentResponse(this HttpRequestMessage request, string content)
        {
            var response = request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(content, Encoding.UTF8);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
            return response;
        }
    }
}