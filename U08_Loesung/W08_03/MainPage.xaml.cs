namespace W08_03;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void OnHelloButtonClicked(object sender, EventArgs e)
    {
        OutputLabel.Text = $"Hello, {NameEntry.Text}!";
    }
}

