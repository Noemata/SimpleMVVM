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

        // Interface resolution was intentionally not used here to show how services can be resolved through Ioc calls.
        public AboutViewModel()
        {
            // This constructor will never be reached in design mode because the designer does not call into App.xaml.cs where the IInDesignModeService is registered.
            // As a result, we will continue to get an exception in the AboutView while editing with VS.  Consider this illustrative of how interfaces are resolved in
            // general.
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
