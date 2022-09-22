using System.Globalization;

namespace U11_03.Converter
{
    public sealed class SliderToEntryConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is not double ? BindableProperty.UnsetValue : System.Convert.ToInt32(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is not string ? BindableProperty.UnsetValue : System.Convert.ToDouble(value);
        }
    }
}
