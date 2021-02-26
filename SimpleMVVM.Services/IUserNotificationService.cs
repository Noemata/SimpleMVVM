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
        Task ShowMessageAsync(string message, string title = null);
        Task ShowErrorMessageAsync(string errorMessage, string title = null);
        Task<bool> AskQuestion(string question, string title = null);
        Task<bool> ShowOptions(string question, string firstOption, string secondOption, string title = null);
        Task<object> ShowOptions(string question, List<Option> options, string title = null);
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
