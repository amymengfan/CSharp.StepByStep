using System.Diagnostics;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Task2017.Extensions;
using Task2017.Services;

namespace Task2017.Filters
{
    class StopwatchFilter : ActionFilterAttribute
    {
        const string StopwatchKey = "stopwatch";

        public override void OnActionExecuting(HttpActionContext context)
        {
            context.Request.Properties[StopwatchKey] = Stopwatch.StartNew();
            context.Resolve<ILogService>().Info("stopwatch start.");
        }

        public override void OnActionExecuted(HttpActionExecutedContext context)
        {
            var stopwatch = (Stopwatch) context.Request.Properties[StopwatchKey];
            var milliseconds = stopwatch.Elapsed.TotalMilliseconds;
            context.Resolve<ILogService>().Info($"stopwatch end, elapsed {milliseconds}.");
            stopwatch.Stop();
        }
    }
}