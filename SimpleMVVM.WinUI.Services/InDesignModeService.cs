using Windows.ApplicationModel;

namespace SimpleMVVM.Services
{
    public sealed class InDesignModeService : IInDesignModeService
    {
        public bool InDesignMode()
        {
            return DesignMode.DesignMode2Enabled;
        }
    }
}
