using Windows.UI.Xaml.Controls;
using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;

using MSWinUI = Microsoft.UI.Xaml.Controls;

using SimpleMVVM.Views;
using SimpleMVVM.Services;
using SimpleMVVM.Dialogs;
using SimpleMVVM.Messages;
using SimpleMVVM.Behaviors;

namespace SimpleMVVM.ViewModels
{
    public class ShellViewModel : ObservableRecipient
    {
        private readonly ILogger _logger = null;

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
            set
            {
                SetProperty(ref _isSetting, value);
            }
        }

        // Used for logging telementry only.
        private const string ItemInvokedEvent = "ShellViewModel.NavigationView.ItemInvoked";
        private const string FrameLoadedEvent = "ShellViewModel.ContentFrame.Loaded";

        internal ActionCommand<MSWinUI.NavigationViewItemInvokedEventArgs> ItemInvokedCommand { get; }
        internal ActionCommand<object> FrameLoadedCommand { get; }

        public ShellViewModel()
        {
            _logger = Ioc.Default.GetService<ILogger>();

            ItemInvokedCommand = new ActionCommand<MSWinUI.NavigationViewItemInvokedEventArgs>(_logger, ItemInvokedEvent, ExecuteItemInvokedCommand);
            FrameLoadedCommand = new ActionCommand<object>(_logger, FrameLoadedEvent, ExecuteFrameLoadedCommand);

            Messenger.Register<ShellStateMessage>(this, (r, m) =>
            {
                if (m.State == ShellState.BusyOn)
                    IsBusy = true;

                if (m.State == ShellState.BusyOff)
                    IsBusy = false;

                m.Reply(m.State);
            });
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
                            Header = "Home";
                            NavigationService.Navigate(typeof(HomeView), null);
                            break;
                        case "About":
                            Header = "About";
                            NavigationService.Navigate(typeof(AboutView), null);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void ExecuteFrameLoadedCommand(object sender)
        {
            SetupNavigationService((Frame)sender);
        }

        public void SetupNavigationService(Frame frame)
        {
            if (frame != null)
                NavigationService.Frame = frame;
        }

        public async void OnSettings()
        {
            if (IsSetting)
            {
                if (NavigationService.Frame.CurrentSourcePageType == null)
                {
                    var dialogService = Ioc.Default.GetService<DialogView>();
                    await dialogService.MessageDialogAsync("Notice:", "Navigate to a page before selecting settings.");
                    IsSetting = false;
                    return;
                }

                NavigationService.Navigate(typeof(SettingsView), null);
            }
            else
                NavigationService.GoBack();
        }
    }
}
