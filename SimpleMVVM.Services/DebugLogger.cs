using System.Diagnostics;

namespace SimpleMVVM.Services
{
    public class DebugLogger : ILoggingService
    {
        public void Log(string message)
        {
            Debug.WriteLine(message);
        }
    }
}
