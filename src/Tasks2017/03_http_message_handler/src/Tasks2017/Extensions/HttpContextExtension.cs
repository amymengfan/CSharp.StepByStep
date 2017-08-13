using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Tasks2017.Extensions
{
    static class HttpContextExtension
    {
        public static T Resolve<T>(this HttpActionContext context)
        {
            return (T) context.RequestContext.Configuration.DependencyResolver.GetService(typeof(T));
        }

        public static T Resolve<T>(this HttpActionExecutedContext context)
        {
            return (T) context.ActionContext.RequestContext.Configuration.DependencyResolver.GetService(typeof(T));
        }

        public static T Resolve<T>(this HttpRequestMessage request)
        {
            return (T) request.GetRequestContext().Configuration.DependencyResolver.GetService(typeof(T));
        }
    }
}