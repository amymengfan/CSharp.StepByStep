using System;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace SessionModuleClient
{
    public class AuthenticationAttribute : Attribute, IAuthenticationFilter
    {
        public bool AllowMultiple { get; } = false;

        public bool RedirectToLoginOnChallenge { get; set; }

        public async Task AuthenticateAsync(
            HttpAuthenticationContext context,
            CancellationToken cancellationToken)
        {
            #region Please implement the following method

            /*
             * We need to create IPrincipal from the authentication token. If
             * we can retrive user session, then the structure of the IPrincipal
             * should be in the following form:
             *
             * ClaimsPrincipal
             *   |- ClaimsIdentity (Primary)
             *        |- Claim: { key: "token", value: "$token value$" }
             *        |- Claim: { key: "userFullName", value: "$user full name$" }
             *
             * If user session cannot be retrived, then the context principal
             * should be an empty ClaimsPrincipal (unauthenticated).
             */

            var token = GetToken(context);
            if (string.IsNullOrWhiteSpace(token))
            {
                context.Principal = new ClaimsPrincipal();
                return;
            }

            var userSession = await GetUserSession(context, token, cancellationToken);
            if (userSession == null)
            {
                context.Principal = new ClaimsPrincipal();
                return;
            }

            context.Principal = new ClaimsPrincipal(new ClaimsIdentity(new []
            {
                new Claim("token", token),
                new Claim("userFullName", userSession.UserFullname)
            }, "my_authentication"));

            #endregion
        }

        public Task ChallengeAsync(
            HttpAuthenticationChallengeContext context,
            CancellationToken cancellationToken)
        {
            #region Please implement the following method

            /*
             * The challenge method will try checking the configuration of
             * RedirectToLoginOnChallenge property. If the value is true,
             * then it will replace the response to redirect to login page.
             * And if the value is false, then simply keeps the original
             * response.
             */

            if (RedirectToLoginOnChallenge)
            {
                context.Result = new RedirectToLoginPageIfUnauthorizedResult(context.Request, context.Result);
            }
            return Task.CompletedTask;

            #endregion
        }

        static async Task<UserSessionDto> GetUserSession(
            HttpAuthenticationContext context,
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
                return null;
            }
            return await response.Content.ReadAsAsync<UserSessionDto>(cancellationToken);
        }

        static string GetToken(HttpAuthenticationContext context)
        {
            const string sessionCookieKey = "X-Session-Token";

            var token = context?.Request?.Headers?
                .GetCookies(sessionCookieKey)?
                .SelectMany(e => e.Cookies)
                .FirstOrDefault(e => e.Name == sessionCookieKey)?
                .Value;

            return token;
        }
    }
}