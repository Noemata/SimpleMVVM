#if WINDOWS_UWP
using Windows.UI.Xaml.Controls;
#else
using Microsoft.UI.Xaml.Controls;
#endif
using Microsoft.Toolkit.Mvvm.DependencyInjection;

using SimpleMVVM.ViewModels;

namespace SimpleMVVM.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomeView : Page
    {
        public HomeView()
        {
            InitializeComponent();
        }

        internal HomeViewModel ViewModel = Ioc.Default.GetService<HomeViewModel>();
    }
}
