using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Newtonsoft.Json;

namespace SessionModuleClient
{
    public class LoginRequiredAttribute : ActionFilterAttribute
    {
        public override bool AllowMultiple { get; } = false;

        public override async Task OnActionExecutingAsync(
            HttpActionContext context,
            CancellationToken cancellationToken)
        {
            #region Please implement the method

            // This filter will try resolve session cookies. If the cookie can be
            // parsed correctly, then it will try calling session API to get the
            // specified session. To ease user session access, it will store the
            // session object in request message properties.
            var token = GetToken(context);

            var userSession = await GetUserSession(context, token, cancellationToken);

            context.Request.SetUserSession(userSession);

            #endregion
        }

        static async Task<UserSessionDto> GetUserSession(
            HttpActionContext context,
            string token,
            CancellationToken cancellationToken)
        {
            var client = (HttpClient) context.Request.GetDependencyScope().GetService(typeof(HttpClient));
            var requestUri = context.Request.RequestUri;
            var response = await client.GetAsync(
                    $"{requestUri.Scheme}://{requestUri.UserInfo}{requestUri.Authority}/session/{token}",
                    cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }
            return await response.Content.ReadAsAsync<UserSessionDto>(cancellationToken);
        }

        static string GetToken(HttpActionContext context)
        {
            const string sessionCookieKey = "X-Session-Token";

            var token = context?.Request?.Headers?
                .GetCookies(sessionCookieKey)?
                .SelectMany(e => e.Cookies)
                .FirstOrDefault(e => e.Name == sessionCookieKey)?
                .Value;

            if (string.IsNullOrWhiteSpace(token))
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }
            return token;
        }
    }
}