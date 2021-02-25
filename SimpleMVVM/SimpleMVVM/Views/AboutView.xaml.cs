using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SimpleMVVM.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AboutView : Page
    {
        public AboutView()
        {
            this.InitializeComponent();
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
