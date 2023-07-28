namespace U10_03;

public partial class AlertPage : ContentPage
{
    public AlertPage()
    {
        InitializeComponent();
    }

    private void ThemesButton_OnClicked(object sender, EventArgs e)
    {
        var window = new Window(new ThemePage())
        {
            Title = "Theme wählen"
        };

        Application.Current!.OpenWindow(window);
    }
}