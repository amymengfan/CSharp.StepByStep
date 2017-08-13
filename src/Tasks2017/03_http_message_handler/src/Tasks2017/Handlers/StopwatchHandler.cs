using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Tasks2017.Extensions;
using Tasks2017.Services;

namespace Tasks2017.Handlers
{
    class StopwatchHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var stopwatch = Stopwatch.StartNew();
            var logService = request.Resolve<ILogService>();

            return base.SendAsync(request, cancellationToken)
                .ContinueWith(task =>
                {
                    var response = task.Result;

                    logService.Info("Stopwatch handler, elapsed: {0}, request: {@1}, response: {@2}.",
                        stopwatch.Elapsed.TotalMilliseconds, request, response);
                    stopwatch.Stop();

                    return response;
                }, TaskContinuationOptions.OnlyOnRanToCompletion);
        }
    }
}