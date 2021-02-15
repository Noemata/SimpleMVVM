using System.Threading.Tasks;

namespace SimpleMVVM.Logging
{
    public interface ILoggingService
    {
        Task Log(string message);
    }
}
