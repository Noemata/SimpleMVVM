using System;
using System.Linq;
using System.Reflection;
using Windows.UI.Xaml;
using Windows.ApplicationModel;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.Activation;

using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;

using SimpleMVVM.Views;
using SimpleMVVM.Helpers;
using SimpleMVVM.Services;
using SimpleMVVM.Uwp.Services;
using SimpleMVVM.ViewModels;

namespace SimpleMVVM
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Application version infromation.
        /// </summary>
        public static string AppVersion;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;

            var assembly = (typeof(App)).GetTypeInfo().Assembly;
            AppVersion = assembly.GetName().Version.ToString();

            // MP! resolve: better way to force assembly load.
            typeof(SimpleMVVM_ViewModels_ForceLoad).Assembly.GetName();
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            // Ensure the UI is initialized
            if (Window.Current.Content is null)
            {
                // Register services.
                Ioc.Default.ConfigureServices(ConfigureServices());

                InitializeSettings();

                Window.Current.Content = new ShellView();

                TitleBarHelper.StyleTitleBar();
                TitleBarHelper.ExpandViewIntoTitleBar();
            }

            // Enable the prelaunch if needed, and activate the window
            if (e.PrelaunchActivated == false)
            {
                CoreApplication.EnablePrelaunch(true);

                Window.Current.Activate();
            }
        }

        private IServiceProvider ConfigureServices()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddSingleton<IInDesignModeService, InDesignModeService>();
            services.AddSingleton<ISettingsService, SettingsService>();
            services.AddSingleton<IMessenger>(WeakReferenceMessenger.Default);
            services.AddSingleton<ILoggingService, DebugLogger>();
            services.AddSingleton<IUserNotificationService, UserNotificationService>();

            RegisterWithIoc(services);

            return services.BuildServiceProvider();
        }

        /// <summary>
        /// Initialize settings default values.
        /// </summary>
        private void InitializeSettings()
        {
            ISettingsService settings = Ioc.Default.GetService<ISettingsService>();

            // Initialize default settings
            settings.SetValue(SettingsKeys.ShowVersionInfo, true, false);
        }

        /// <summary>
        /// Register attributed classes with your Ioc container.
        /// </summary>
        /// <param name="services">The ServiceCollection to be used.</param>
        void RegisterWithIoc(ServiceCollection services)
        {
            string localname = (typeof(App)).GetTypeInfo().Assembly.GetName().Name;

            foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
            {
                string name = a.GetName().Name;

                if (name != localname && !name.EndsWith("ViewModels"))
                    continue;

                var types = a.GetTypes().Select(t => new { T = t, Mode = t.GetCustomAttribute<RegisterWithIocAttribute>()?.Mode })
                .Where(o => o.Mode != null && o.Mode != InstanceMode.None);

                foreach (var t in types)
                {
                    var type = t.T;
                    if (t.Mode == InstanceMode.Singleton)
                        services.AddSingleton(type);
                    else if (t.Mode == InstanceMode.Transient)
                        services.AddTransient(type);
                }
            }
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
