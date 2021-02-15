using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;

using SimpleMVVM.Logging;

namespace SimpleMVVM.ViewModels
{
    public class ViewModelBase : ObservableRecipient
    {
        public ILoggingService LoggingService => Ioc.Default.GetService<ILoggingService>();
    }
}
