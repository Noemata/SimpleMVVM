using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace SimpleMVVM.ViewModels
{
    public class SettingsViewModel : ObservableRecipient
    {
        private string _message;
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public SettingsViewModel()
        {
            Message = "Settings.";
        }
    }
}
