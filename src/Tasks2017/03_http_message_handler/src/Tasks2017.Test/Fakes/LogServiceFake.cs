using System.Collections.Generic;
using Tasks2017.Services;

namespace Tasks2017.Test.Fakes
{
    class LogServiceFake : ILogService
    {
        public List<string> Messages { get; }

        public LogServiceFake()
        {
            this.Messages = new List<string>();
        }

        public void Info(string template)
        {
            this.Messages.Add(template);
        }

        public void Info(string template, params object[] args)
        {
            this.Messages.Add(template);
        }
    }
}
