using System.Diagnostics;
using System.Threading.Tasks;

namespace SimpleMVVM.Logging
{
    public class DebugLoggingService : ILoggingService
    {
        public Task Log(string message)
        {
            Debug.WriteLine(message);
            return Task.FromResult(0);
        }
    }
}
