using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Task2017.Extensions
{
    static class HttpActionContextExtension
    {
        public static T Resolve<T>(this HttpActionContext context)
        {
            return (T) context.RequestContext.Configuration.DependencyResolver.GetService(typeof(T));
        }

        public static T Resolve<T>(this HttpActionExecutedContext context)
        {
            return (T) context.ActionContext.RequestContext.Configuration.DependencyResolver.GetService(typeof(T));
        }
    }
}