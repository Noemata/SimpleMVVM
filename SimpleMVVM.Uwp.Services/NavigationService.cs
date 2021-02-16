﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace SimpleMVVM.Services
{
    public static class NavigationService
    {
        public static event NavigatedEventHandler Navigated;

        public static event NavigationFailedEventHandler NavigationFailed;

        private static Frame frame;
        private static object lastParamUsed;

        public static Frame Frame
        {
            get
            {
                if (frame == null)
                {
                    frame = Window.Current.Content as Frame;
                    RegisterFrameEvents();
                }

                return frame;
            }

            set
            {
                UnregisterFrameEvents();
                frame = value;
                RegisterFrameEvents();
            }
        }

        public static bool CanGoBack => Frame.CanGoBack;

        public static bool CanGoForward => Frame.CanGoForward;

        public static bool GoBack()
        {
            if (CanGoBack)
            {
                Frame.GoBack();
                return true;
            }

            return false;
        }

        public static void GoForward() => Frame.GoForward();

        public static bool Navigate(Type pageType, object parameter = null, NavigationTransitionInfo infoOverride = null)
        {
            if (Frame.Content?.GetType() != pageType || (parameter != null && !parameter.Equals(lastParamUsed)))
            {
                var navigationResult = Frame.Navigate(pageType, parameter, infoOverride);
                if (navigationResult)
                {
                    lastParamUsed = parameter;
                }

                return navigationResult;
            }
            else
            {
                return false;
            }
        }

        public static bool Navigate<T>(object parameter = null, NavigationTransitionInfo infoOverride = null)
            where T : Page
            => Navigate(typeof(T), parameter, infoOverride);

        private static void RegisterFrameEvents()
        {
            if (frame != null)
            {
                frame.Navigated += OnFrameNavigated;
                frame.NavigationFailed += OnFrameNavigationFailed;
            }
        }

        private static void UnregisterFrameEvents()
        {
            if (frame != null)
            {
                frame.Navigated -= OnFrameNavigated;
                frame.NavigationFailed -= OnFrameNavigationFailed;
            }
        }

        private static void OnFrameNavigationFailed(object sender, NavigationFailedEventArgs e) => NavigationFailed?.Invoke(sender, e);

        private static void OnFrameNavigated(object sender, NavigationEventArgs e) => Navigated?.Invoke(sender, e);
    }
}

