using Serilog;

namespace Tasks2017.Services
{
    class LogService : ILogService
    {
        public void Info(string message)
        {
            Log.Information(message);
        }
    }
}