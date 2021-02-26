using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Windows.UI;
using Windows.UI.Popups;

namespace SimpleMVVM.Services
{
    public class UserNotificationService : IUserNotificationService
    {
        public object XamlRoot { get; set; }

        /// <summary>
        /// Shows the standard MessageDialog
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public async Task ShowMessageAsync(string message, string title = null)
        {
            MessageDialog messageDialog = title == null ? new MessageDialog(message) : new MessageDialog(message, title);
            await messageDialog.ShowAsync();
        }

        /// <summary>
        /// The same as ShowMessageAsync. In the future there may be a difference so choose the right one
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public async Task ShowErrorMessageAsync(string errorMessage, string title = null)
        {
            try
            {
                MessageDialog messageDialog = title == null ? new MessageDialog(errorMessage) : new MessageDialog(errorMessage, title);
                await messageDialog.ShowAsync();
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Asks the user a yes/no question
        /// </summary>
        /// <param name="question"></param>
        /// <param name="title"></param>
        /// <returns>Returns true for yes and false for no</returns>
        public async Task<bool> AskQuestion(string question, string title = null)
        {
            MessageDialog messageDialog;
            messageDialog = string.IsNullOrEmpty(title) ? new MessageDialog(question) : new MessageDialog(question, title);
            var result = false;
            messageDialog.Commands.Add(new UICommand { Id = "yes", Label = "Yes", Invoked = delegate { result = true; } });
            messageDialog.Commands.Add(new UICommand { Id = "no", Label = "No", Invoked = delegate { result = false; } });
            await messageDialog.ShowAsync();
            return result;
        }

        /// <summary>
        /// Asks the user to choose between two options
        /// </summary>
        /// <param name="question"></param>
        /// <param name="firstOption"></param>
        /// <param name="secondOption"></param>
        /// <param name="title"></param>
        /// <returns>Returns true for first option or false for the second one</returns>
        public async Task<bool> ShowOptions(string question, string firstOption, string secondOption, string title = null)
        {
            MessageDialog messageDialog;
            messageDialog = string.IsNullOrEmpty(title) ? new MessageDialog(question) : new MessageDialog(question, title);
            var result = false;
            messageDialog.Commands.Add(new UICommand { Id = firstOption, Label = firstOption, Invoked = delegate { result = true; } });
            messageDialog.Commands.Add(new UICommand { Id = secondOption, Label = secondOption, Invoked = delegate { result = false; } });
            await messageDialog.ShowAsync();
            return result;
        }

        /// <summary>
        /// Shows a list of options to the user
        /// </summary>
        /// <param name="question"></param>
        /// <param name="options">List of options. You can't add more than 3 options</param>
        /// <param name="title"></param>
        /// <returns>Returns the result of the selected option</returns>
        public async Task<object> ShowOptions(string question, List<Option> options, string title = null)
        {
            if (string.IsNullOrWhiteSpace(question))
                throw new ArgumentException("The question can't be empty");

            if (options == null)
                throw new ArgumentException("The list of options can't be null");

            if (options.Count > 2)
                throw new ArgumentException("You can't send more than 2 options");

            var messageDialog = string.IsNullOrEmpty(title) ? new MessageDialog(question) : new MessageDialog(question, title);
            object result = null;

            foreach (var option in options)
            {
                if (option.Id == null || string.IsNullOrWhiteSpace(option.Text))
                    throw new ArgumentException("Id and Text properties are mandatory for an option");

                messageDialog.Commands.Add(new UICommand { Id = option.Id, Label = option.Text, Invoked = delegate { result = option.Result; } });
            }

            await messageDialog.ShowAsync();
            return result;
        }

        /// <summary>
        /// Opens a modal message dialog.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        /// <returns>Task.</returns>
        public async Task MessageDialogAsync(string title, string message)
        {
            await MessageDialogAsync(title, message, "OK");
        }

        /// <summary>
        /// Opens a modal message dialog.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        /// <param name="buttonText">The button text.</param>
        /// <returns>Task.</returns>
        public async Task MessageDialogAsync(string title, string message, string buttonText)
        {
            var dialog = new ContentDialog
            {
                Title = title,
                Content = message,
                CloseButtonText = buttonText
            };

            dialog.XamlRoot = (XamlRoot)XamlRoot;

            await dialog.ShowAsync();
        }

        /// <summary>
        /// Opens a modal confirmation dialog.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <returns>Task&lt;System.Nullable&lt;System.Boolean&gt;&gt;.</returns>
        public async Task<bool?> ConfirmationDialogAsync(string title)
        {
            return await ConfirmationDialogAsync(title, "OK", string.Empty, "Cancel");
        }

        /// <summary>
        /// Opens a modal confirmation dialog.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="yesButtonText">The 'Yes' button text.</param>
        /// <param name="noButtonText">The 'No' button text.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public async Task<bool> ConfirmationDialogAsync(string title, string yesButtonText, string noButtonText)
        {
            return (await ConfirmationDialogAsync(title, yesButtonText, noButtonText, string.Empty)).Value;
        }

        /// <summary>
        /// Opens a modal confirmation dialog.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="yesButtonText">The 'Yes' button text.</param>
        /// <param name="noButtonText">The 'No' button text.</param>
        /// <param name="cancelButtonText">The cancel button text.</param>
        /// <returns>Task&lt;System.Nullable&lt;System.Boolean&gt;&gt;.</returns>
        public async Task<bool?> ConfirmationDialogAsync(string title, string yesButtonText, string noButtonText, string cancelButtonText)
        {
            var dialog = new ContentDialog
            {
                Title = title,
                //IsPrimaryButtonEnabled = true,
                PrimaryButtonText = yesButtonText,
                //IsSecondaryButtonEnabled = true,
                SecondaryButtonText = noButtonText,
                CloseButtonText = cancelButtonText
            };

            dialog.XamlRoot = (XamlRoot)XamlRoot;

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.None)
            {
                return null;
            }

            return (result == ContentDialogResult.Primary);
        }

        /// <summary>
        /// Opens a modal input dialog for a string.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        public async Task<string> InputStringDialogAsync(string title)
        {
            return await InputStringDialogAsync(title, string.Empty);
        }

        /// <summary>
        /// Opens a modal input dialog for a string.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="defaultText">The default response text.</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        public async Task<string> InputStringDialogAsync(string title, string defaultText)
        {
            return await InputStringDialogAsync(title, defaultText, "OK", "Cancel");
        }

        /// <summary>
        /// Opens a modal input dialog for a string.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="defaultText">The default response text.</param>
        /// <param name="okButtonText">The 'OK' button text.</param>
        /// <param name="cancelButtonText">The 'Cancel' button text.</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        public async Task<string> InputStringDialogAsync(string title, string defaultText, string okButtonText, string cancelButtonText)
        {
            var inputTextBox = new TextBox
            {
                AcceptsReturn = false,
                Height = 32,
                Text = defaultText,
                SelectionStart = defaultText.Length
            };

            var dialog = new ContentDialog
            {
                Content = inputTextBox,
                Title = title,
                IsSecondaryButtonEnabled = true,
                PrimaryButtonText = okButtonText,
                SecondaryButtonText = cancelButtonText
            };

            dialog.XamlRoot = (XamlRoot)XamlRoot;

            if (await dialog.ShowAsync() == ContentDialogResult.Primary)
            {
                return inputTextBox.Text;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Opens a modal input dialog for a multi-line text.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        public async Task<string> InputTextDialogAsync(string title)
        {
            return await InputTextDialogAsync(title, string.Empty);
        }

        /// <summary>
        /// Opens a modal input dialog for a multi-line text.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="defaultText">The default text.</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        public async Task<string> InputTextDialogAsync(string title, string defaultText)
        {
            return await InputTextDialogAsync(title, defaultText, "OK", "Cancel");
        }

        /// <summary>
        /// Opens a modal input dialog for a multi-line text.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="defaultText">The default text.</param>
        /// <param name="okButtonText">The 'OK' button text.</param>
        /// <param name="cancelButtonText">The 'Cancel' button text.</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        public async Task<string> InputTextDialogAsync(string title, string defaultText, string okButtonText, string cancelButtonText)
        {
            var inputTextBox = new TextBox
            {
                AcceptsReturn = true,
                Height = 32 * 6,
                Text = defaultText,
                TextWrapping = TextWrapping.Wrap,
                SelectionStart = defaultText.Length,
                Opacity = 1,
                BorderThickness = new Thickness(1),
                BorderBrush = new SolidColorBrush((Color)Application.Current.Resources["CustomDialogBorderColor"])
            };

            var dialog = new ContentDialog
            {
                Content = inputTextBox,
                Title = title,
                IsSecondaryButtonEnabled = true,
                PrimaryButtonText = okButtonText,
                SecondaryButtonText = cancelButtonText
            };

            dialog.XamlRoot = (XamlRoot)XamlRoot;

            if (await dialog.ShowAsync() == ContentDialogResult.Primary)
            {
                return inputTextBox.Text;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
