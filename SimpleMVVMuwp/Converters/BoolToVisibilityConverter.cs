using System;
#if WINDOWS_UWP
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
#else
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
#endif

namespace SimpleMVVM.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Visibility visibility = Visibility.Collapsed;

            if (value is bool && (bool)value)
            {
                visibility = Visibility.Visible;
            }

            var invertResult = SafeParseBool(parameter);

            if (invertResult)
            {
                visibility = Opposite(visibility);
            }

            return visibility;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            Visibility visibility = (Visibility)value;
            bool returnValue = (visibility == Visibility.Visible) ? true : false;
            bool invertResult;

            if (parameter != null && bool.TryParse(parameter.ToString(), out invertResult))
            {
                if (invertResult)
                {
                    returnValue = !(returnValue);
                }
            }

            return returnValue;
        }

        private static bool SafeParseBool(object parameter)
        {
            var parsed = false;

            if (parameter != null)
            {
                bool.TryParse(parameter.ToString(), out parsed);
            }

            return parsed;
        }

        private static Visibility Opposite(Visibility target)
        {
            if (target == Visibility.Visible)
            {
                return Visibility.Collapsed;
            }
            else
            {
                return Visibility.Visible;
            }
        }
    }
}
