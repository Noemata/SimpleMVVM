using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using SimpleMVVM.Services;

namespace SimpleMVVM.ViewModels
{
    [RegisterVMAttribute(InstanceMode.Transient)]
    public class SettingsViewModel : ObservableRecipient
    {
        private readonly ISettingsService _settingsService;

        private bool _showVersion;
        public bool ShowVersion
        {
            get => _showVersion;
            set
            {
                if (SetProperty(ref _showVersion, value))
                    _settingsService.SetValue(SettingsKeys.ShowVersionInfo, value);
            }
        }

        private string _message;
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public SettingsViewModel(ISettingsService settingsService)
        {
            _settingsService = settingsService;

            _showVersion = _settingsService.GetValue<bool>(SettingsKeys.ShowVersionInfo);

            Message = "Hello settings.";
        }
    }
}
