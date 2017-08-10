namespace Task2017.Services
{
    public class TaskService
    {
        public string Copy(string uri)
        {
            return string.Copy(uri ?? "");
        }
    }
}