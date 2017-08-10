using System.Collections.Generic;
using Task2017.Services;

namespace Task2017.Test.Fakes
{
    class LogServiceFake : ILogService
    {
        public readonly List<string> Messages;

        public LogServiceFake()
        {
            this.Messages = new List<string>();
        }

        public void Info(string message)
        {
            this.Messages.Add(message);
        }
    }
}
