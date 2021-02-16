using Microsoft.Toolkit.Mvvm.ComponentModel;
using SimpleMVVM.Services;

namespace SimpleMVVM.ViewModels
{
    [RegisterVMAttribute(InstanceMode.Transient)]
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
