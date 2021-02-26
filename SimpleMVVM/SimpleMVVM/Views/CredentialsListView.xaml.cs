using Microsoft.UI.Xaml.Controls;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using SimpleMVVM.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SimpleMVVM.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CredentialsListView : Page
    {
        public CredentialsListView()
        {
            this.InitializeComponent();
        }

        internal CredentialsListViewModel ViewModel => Ioc.Default.GetService<CredentialsListViewModel>();
    }
}
