using Windows.ApplicationModel;
using SimpleMVVM.Services;

namespace SimpleMVVM.Uwp.Services
{
    public sealed class InDesignModeService : IInDesignModeService
    {
        public bool InDesignMode()
        {
            return DesignMode.DesignMode2Enabled;
        }
    }
}
