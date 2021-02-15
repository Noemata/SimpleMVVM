using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace SimpleMVVM.ViewModels
{
    public class AboutViewModel : ObservableRecipient
    {
        private string _message;
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        protected override void OnActivated()
        {
            base.OnActivated();

            Message = "Activate about.";
        }
    }
}
