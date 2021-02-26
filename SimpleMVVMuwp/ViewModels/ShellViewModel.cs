using System;
using Windows.UI.Xaml.Controls;
using Windows.ApplicationModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.Toolkit.Mvvm.ComponentModel;

using MSWinUI = Microsoft.UI.Xaml.Controls;

using SimpleMVVM.Views;
using SimpleMVVM.Services;
using SimpleMVVM.Messages;

namespace SimpleMVVM.ViewModels
{
    [RegisterWithIoc(InstanceMode.Transient)]
    public class ShellViewModel : ObservableRecipient
    {
        private readonly IUserNotificationService _userNotificationService;
        private readonly ISettingsService _settingsService;
        private readonly IMessenger _messenger;

        private string _header = "Simple Microsoft MVVM Toolkit Sample";
        public string Header
        {
            get => _header;
            set => SetProperty(ref _header, value);
        }

        private bool _isBusy = false;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        private bool _isSetting = false;
        public bool IsSetting
        {
            get => _isSetting;
            set => SetProperty(ref _isSetting, value);
        }

        private bool _showVersion;
        public bool ShowVersion
        {
            get => _showVersion;
            set => SetProperty(ref _showVersion, value);
        }

        public string AppVersion => App.AppVersion;

        public IRelayCommand<Frame> FrameLoadedCommand { get; }
        public IRelayCommand<MSWinUI.NavigationViewItemInvokedEventArgs> ItemInvokedCommand { get; }

        // Ioc will resolve all these interfaces for us provided they have been configured in App.xaml.cs
        public ShellViewModel(ISettingsService settingsService, IMessenger messenger, IUserNotificationService userNotificationService)
        {
            // Help the designer not fail and run faster.  Return early whenever a ViewModel constructor causes issues for the designer.
            // The Ioc calls below are problematic if you have viewModels:ShellViewModel x:Name="ViewModel" in the View's XAML.  Specifying
            // internal ShellViewModel ViewModel = Ioc.Default.GetService<ShellViewModel>() in the View's code behind is cleaner and
            // supports VM parameters.  XAML requires parameterless constructors for a viewModels:ShellViewModel style reference.
            if (DesignMode.DesignMode2Enabled)
                return;

            _settingsService = settingsService;
            _messenger = messenger;
            _userNotificationService = userNotificationService;

            _showVersion = _settingsService.GetValue<bool>(SettingsKeys.ShowVersionInfo);

            FrameLoadedCommand = new RelayCommand<Frame>(SetupNavigationService);
            ItemInvokedCommand = new RelayCommand<MSWinUI.NavigationViewItemInvokedEventArgs>(ExecuteItemInvokedCommand);

            _messenger.Register<ShellStateMessage>(this, (r, m) =>
            {
                if (m.State == ShellState.BusyOn)
                    IsBusy = true;

                if (m.State == ShellState.BusyOff)
                    IsBusy = false;

                if (m.State == ShellState.VersionOn)
                    ShowVersion = true;

                if (m.State == ShellState.VersionOff)
                    ShowVersion = false;

                m.Reply(m.State);
            });
        }

        private void SetupNavigationService(Frame frame)
        {
            if (frame != null)
                NavigationService.Frame = frame;
        }

        private void ExecuteItemInvokedCommand(MSWinUI.NavigationViewItemInvokedEventArgs args)
        {
            if (args != null)
            {
                string option = args.InvokedItem as string;

                if (option != null)
                {
                    switch (option)
                    {
                        case "Home":
                            Header = option;
                            NavigationService.Navigate(typeof(HomeView), null);
                            break;

                        case "List":
                            Header = option;
                            NavigationService.Navigate(typeof(CredentialsListView), null);
                            break;

                        case "About":
                            Header = option;
                            NavigationService.Navigate(typeof(AboutView), null);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public async void OnSettings()
        {
            if (IsSetting)
            {
                if (NavigationService.Frame.CurrentSourcePageType == null)
                {
                    await _userNotificationService.MessageDialogAsync("Notice:", "Navigate to a page before selecting settings.");

                    IsSetting = false;
                    return;
                }

                NavigationService.Navigate(typeof(SettingsView), null);
            }
            else
            {
                NavigationService.GoBack();
            }
        }
    }
}
