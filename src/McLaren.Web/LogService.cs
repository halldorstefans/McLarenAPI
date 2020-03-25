using Microsoft.Extensions.Logging;

namespace McLaren.Web
{
    public interface ILogService
    {
        public void WriteLog(string message);
    }
    public class LogService : ILogService
    {
        public ILogger Logger { get; set; }
        public void WriteLog(string message)
        {
            Logger.LogInformation(message);
        }
    }
}