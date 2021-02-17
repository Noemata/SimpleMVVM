using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.Toolkit.Mvvm.ComponentModel;

using SimpleMVVM.Services;
using SimpleMVVM.Messages;

namespace SimpleMVVM.ViewModels
{
    [RegisterWithIoc(InstanceMode.Transient)]
    public class SettingsViewModel : ObservableRecipient
    {
        private readonly ISettingsService _settingsService;
        private readonly IMessenger _messenger;

        private bool _showVersion;
        public bool ShowVersion
        {
            get => _showVersion;
            set
            {
                if (SetProperty(ref _showVersion, value))
                {
                    _settingsService.SetValue(SettingsKeys.ShowVersionInfo, value);

                    if (value)
                        _messenger.Send(new ShellStateMessage(ShellState.VersionOn));
                    else
                        _messenger.Send(new ShellStateMessage(ShellState.VersionOff));
                }
            }
        }

        private string _message;
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public SettingsViewModel(ISettingsService settingsService, IMessenger messenger)
        {
            _settingsService = settingsService;
            _messenger = messenger;

            _showVersion = _settingsService.GetValue<bool>(SettingsKeys.ShowVersionInfo);

            Message = "Hello settings.";
        }
    }
}
