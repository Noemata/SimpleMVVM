using System;
using System.Linq;
using System.Reflection;

using Microsoft.UI.Xaml;
using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;

using Windows.ApplicationModel;

using SimpleMVVM.Views;
using SimpleMVVM.Services;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SimpleMVVM
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
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
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            Ioc.Default.ConfigureServices(ConfigureServices());

            m_window = new ShellView();
            m_window.Activate();
        }

        private IServiceProvider ConfigureServices()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddSingleton<IMessenger>(WeakReferenceMessenger.Default);
            services.AddSingleton<ILoggingService, DebugLogger>();

            RegisterWithIoc(services);

            return services.BuildServiceProvider();
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
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            // Save application state and stop any background activity
        }

        private Window m_window;
    }
}
