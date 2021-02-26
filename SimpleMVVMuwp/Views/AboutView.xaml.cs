#if WINDOWS_UWP
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
#else
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
#endif

namespace SimpleMVVM.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AboutView : Page
    {
        public AboutView()
        {
            InitializeComponent();
        }

        // MP! This shouldn't be here.  Put this in the XAML side?
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.IsActive = true;
        }

        // MP! This shouldn't be here.  Put this in the XAML side?
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            ViewModel.IsActive = false;
        }
    }
}
