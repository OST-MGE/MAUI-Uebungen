using System.Globalization;

namespace U11_03.Converter;

public sealed class RGBToColorConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values == null)
            return BindableProperty.UnsetValue;

        if (values.Length != 3)
            throw new NotSupportedException($"3 values needed(R, G, B) but only {values.Length} given");

        var r = (byte)System.Convert.ToInt32(values[0]);
        var g = (byte)System.Convert.ToInt32(values[1]);
        var b = (byte)System.Convert.ToInt32(values[2]);

        return Color.FromRgb(r, g, b);
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        if (value == BindableProperty.UnsetValue)
            return null;

        if (value is not Color color)
            return null;

        var colors = new object[] { color.Red, color.Green, color.Blue };

        return colors;
    }
}