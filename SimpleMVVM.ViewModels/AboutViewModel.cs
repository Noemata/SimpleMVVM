using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using SimpleMVVM.Services;

namespace SimpleMVVM.ViewModels
{
    [RegisterWithIoc(InstanceMode.Transient)]
    public class AboutViewModel : ObservableRecipient
    {
        private readonly IInDesignModeService _design;

        private string _message;
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public AboutViewModel()
        {
            _design = Ioc.Default.GetService<IInDesignModeService>();

            if (_design.InDesignMode())
                return;
        }

        protected override void OnActivated()
        {
            base.OnActivated();

            Message = "Activate about.";
        }
    }
}
