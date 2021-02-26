#if !WINDOWS_UWP
using Microsoft.UI.Xaml.Controls;
#endif
using Windows.Networking.Connectivity;

namespace SimpleMVVM.Helpers
{
    public static class GlobalVariable
    {
        public static bool IsInternetConnected
        {
            get
            {
                ConnectionProfile connections = NetworkInformation.GetInternetConnectionProfile();
                bool internet = (connections != null) && (connections.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess);
                return internet;
            }
        }

#if !WINDOWS_UWP
        public static Frame ContentFrame { get; set; }
#endif
    }
}
