using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMVVM.Services
{
    public interface IUserNotificationService
    {
        object XamlRoot { get; set; }

        Task MessageDialogAsync(string title, string message);
        Task MessageDialogAsync(string title, string message, string buttonText);
        Task<bool?> ConfirmationDialogAsync(string title);
        Task<bool> ConfirmationDialogAsync(string title, string yesButtonText, string noButtonText);
        Task<bool?> ConfirmationDialogAsync(string title, string yesButtonText, string noButtonText, string cancelButtonText);
        Task<string> InputStringDialogAsync(string title);
        Task<string> InputStringDialogAsync(string title, string defaultText);
        Task<string> InputStringDialogAsync(string title, string defaultText, string okButtonText, string cancelButtonText);
        Task<string> InputTextDialogAsync(string title);
        Task<string> InputTextDialogAsync(string title, string defaultText);
        Task<string> InputTextDialogAsync(string title, string defaultText, string okButtonText, string cancelButtonText);
    }
}
