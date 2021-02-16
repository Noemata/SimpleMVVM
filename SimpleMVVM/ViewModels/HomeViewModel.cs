﻿using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using SimpleMVVM.Messages;
using SimpleMVVM.Services;

namespace SimpleMVVM.ViewModels
{
    [RegisterVMAttribute(InstanceMode.Transient)]
    public class HomeViewModel : ObservableRecipient
    {
        public static HomeViewModel Instance => Ioc.Default.GetService<HomeViewModel>();

        private readonly IMessenger _messenger;

        private string _message;
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public IAsyncRelayCommand GetBusyAsyncCommand { get; }

        public HomeViewModel(IMessenger messenger)
        {
            _messenger = messenger;

            GetBusyAsyncCommand = new AsyncRelayCommand(GetBusyAsync);

            Message = "Hello home.";
        }

        private async Task GetBusyAsync()
        {
            await _messenger.Send(new ShellStateMessage(ShellState.BusyOn));
            await Task.Delay(3000);
            await _messenger.Send(new ShellStateMessage(ShellState.BusyOff));
        }
    }
}
