using Serilog;

namespace Tasks2017.Services
{
    class LogService : ILogService
    {
        public void Info(string template)
        {
            Log.Information(template);
        }

        public void Info(string template, params object[] args)
        {
            Log.Information(template, args);
        }
    }
}