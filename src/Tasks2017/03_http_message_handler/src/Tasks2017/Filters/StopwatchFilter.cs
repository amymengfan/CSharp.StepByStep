using System.Diagnostics;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Tasks2017.Extensions;
using Tasks2017.Services;

namespace Tasks2017.Filters
{
    class StopwatchFilter : ActionFilterAttribute
    {
        const string StopwatchKey = "stopwatch";

        public override void OnActionExecuting(HttpActionContext context)
        {
            context.Request.Properties[StopwatchKey] = Stopwatch.StartNew();
        }

        public override void OnActionExecuted(HttpActionExecutedContext context)
        {
            var stopwatch = (Stopwatch) context.Request.Properties[StopwatchKey];

            var logService = context.Resolve<ILogService>();

            logService.Info("Stopwatch filter, elapsed: {0}, request: {@1}, response: {@2}.",
                stopwatch.Elapsed.TotalMilliseconds, context.Request, context.Response);
            stopwatch.Stop();
        }
    }
}