namespace Tasks2017.Services
{
    public interface ILogService
    {
        void Info(string template);

        void Info(string template, params object[] args);
    }
}