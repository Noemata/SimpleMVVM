using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using SimpleMVVM.Messages;

namespace SimpleMVVM.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private string _message;
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public IAsyncRelayCommand GetBusyAsyncCommand { get; }

        public HomeViewModel()
        {
            GetBusyAsyncCommand = new AsyncRelayCommand(GetBusyAsync);

            Message = "Hello home.";
        }

        private async Task GetBusyAsync()
        {
            await Messenger.Send(new ShellStateMessage(ShellState.BusyOn));
            await Task.Delay(3000);
            await Messenger.Send(new ShellStateMessage(ShellState.BusyOff));
        }
    }
}
