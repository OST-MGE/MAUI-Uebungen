namespace U10_02;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AlertPage();
    }

    protected override Window CreateWindow(IActivationState activationState)
    {
        var window = base.CreateWindow(activationState);

        window.Title = "Änderungen speichern";

        return window;
    }
}
