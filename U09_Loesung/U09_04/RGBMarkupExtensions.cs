namespace U09_04;

public sealed class RGBString : IMarkupExtension<string>
{
    public byte R { get; set; }

    public byte G { get; set; }

    public byte B { get; set; }

    public string ProvideValue(IServiceProvider serviceProvider) => $"#{R:X2}{G:X2}{B:X2}";

    object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider) => ProvideValue(serviceProvider);
}

public sealed class RGBColor : IMarkupExtension<Color>
{
    public byte R { get; set; }

    public byte G { get; set; }

    public byte B { get; set; }

    public Color ProvideValue(IServiceProvider serviceProvider) => Color.FromRgb(R, G, B);

    object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider) => ProvideValue(serviceProvider);
}