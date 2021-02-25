using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml;
using SimpleMVVM.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SimpleMVVM.Views
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShellView : Window
    {
        public ShellView()
        {
            this.InitializeComponent();
        }

        private bool bNot(bool value) => value ? false : true;
        private Visibility vNot(bool value) => value ? Visibility.Collapsed : Visibility.Visible;

        internal ShellViewModel ViewModel = Ioc.Default.GetService<ShellViewModel>();
    }
}
