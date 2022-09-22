using System.Globalization;

namespace U11_03.Converter;

public sealed class RGBToStringConverter : IMultiValueConverter
{
    private readonly RGBToColorConverter _toColorConverter = new();

    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        var color = (Color)_toColorConverter.Convert(values, targetType, parameter, culture);
        var colorString = color.ToHex();

        return colorString;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}