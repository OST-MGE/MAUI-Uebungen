namespace U10_03;

public partial class App : Application
{
    private Theme _activeTheme;

    public App()
    {
        InitializeComponent();

        ActiveTheme = Theme.Blue;

        MainPage = new AlertPage();
    }

    protected override Window CreateWindow(IActivationState activationState)
    {
        var window = base.CreateWindow(activationState);

        window.Title = "Änderungen speichern";

        return window;
    }

    internal Theme ActiveTheme
    {
        get => _activeTheme;
        set 
        {
            var newColor = value switch
            {
                Theme.Blue => (Color)Resources["BlueColor"],
                Theme.Green => (Color)Resources["GreenColor"],
                Theme.Red => (Color)Resources["RedColor"],
                _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
            };

            ((SolidColorBrush)Resources["HighlightButtonColor"]).Color = newColor;

            _activeTheme = value;
        }
    }

    internal enum Theme
    {
        Blue,
        Green,
        Red
    }
}
