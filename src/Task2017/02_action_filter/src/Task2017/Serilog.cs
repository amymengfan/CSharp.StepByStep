using Serilog;

namespace Task2017
{
    class Serilog
    {
        public static void Init()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.RollingFile("D:\\Logs\\task2017-{Date}.log")
                .CreateLogger();
        }
    }
}