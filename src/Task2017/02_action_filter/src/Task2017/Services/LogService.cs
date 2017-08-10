using Serilog;

namespace Task2017.Services
{
    class LogService : ILogService
    {
        public void Info(string message)
        {
            Log.Information(message);
        }
    }
}