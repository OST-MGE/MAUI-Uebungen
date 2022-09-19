using System.Globalization;
using System.Windows.Data;
using System.Windows;
using System;

namespace U14.WPF.View.ValueConverter;

public sealed class VisibilityValueConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var visibility = Visibility.Hidden;

        if (value is bool valueAsBool)
        {
            visibility = valueAsBool ? Visibility.Visible : Visibility.Hidden;
        }

        return visibility;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var isVisible = false;

        if (value is Visibility valueAsVisibility)
        {
            isVisible = valueAsVisibility == Visibility.Visible;
        }

        return isVisible;
    }
}