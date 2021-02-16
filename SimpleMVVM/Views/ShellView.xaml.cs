using SimpleMVVM.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SimpleMVVM.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShellView : Page
    {
        public ShellView()
        {
            this.InitializeComponent();
        }

        private bool bNot(bool value) => value ? false : true;
        private Visibility vNot(bool value) => value ? Visibility.Collapsed : Visibility.Visible;
    }
}
