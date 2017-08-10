namespace Task2017.Services
{
    public class TasksService
    {
        public string Copy(string uri)
        {
            return string.Copy(uri ?? "");
        }
    }
}