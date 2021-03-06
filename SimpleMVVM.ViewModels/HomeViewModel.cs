﻿using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using SimpleMVVM.Messages;
using SimpleMVVM.Services;

namespace SimpleMVVM.ViewModels
{
    [RegisterWithIoc(InstanceMode.Transient)]
    public class HomeViewModel : ObservableRecipient
    {
        private readonly IMessenger _messenger;

        private string _message;
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public IAsyncRelayCommand GetBusyAsyncCommand => new AsyncRelayCommand(GetBusyAsync);

        public HomeViewModel(IMessenger messenger)
        {
            _messenger = messenger;

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
