using System.Collections.ObjectModel;
using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.Toolkit.Mvvm.ComponentModel;

using SimpleMVVM.Models;
using SimpleMVVM.Services;
using System.Threading.Tasks;
using SimpleMVVM.Messages;
using Microsoft.Toolkit.Mvvm.Input;

namespace SimpleMVVM.ViewModels
{
    [RegisterWithIoc(InstanceMode.Transient)]
    public class CredentialsListViewModel : ObservableRecipient
    {
        private readonly ILoggingService _log;
        private readonly IMessenger _messenger;
        private readonly IUserNotificationService _userNotificationService;

        public AsyncRelayCommand<Credential> DeleteCommand => new AsyncRelayCommand<Credential>(OnDelete, cred => cred is object);

        public ObservableCollection<Credential> CredentialsCollection { get; } = new ObservableCollection<Credential>();

        public CredentialsListViewModel(ILoggingService loggingservice, IMessenger messenger, IUserNotificationService userNotificationService)
        {
            _log = loggingservice;
            _messenger = messenger;
            _userNotificationService = userNotificationService;

            CredentialsCollection.Add(new Credential { Name = "Janet", Email = "janet@acme.com", Role = "Admin" });
            CredentialsCollection.Add(new Credential { Name = "Tom", Email = "tom@acme.com", Role = "User" });
            CredentialsCollection.Add(new Credential { Name = "Joe", Email = "joe@acme.com", Role = "Joker" });
        }

        private async Task OnDelete(Credential entry)
        {
            bool? result = await _userNotificationService.ConfirmationDialogAsync($"Delete: {entry.Name}");

            if (result == true)
            {
                _log.Log($"Delete: {entry.Name}");
                CredentialsCollection.Remove(entry);
            }
        }
    }
}
