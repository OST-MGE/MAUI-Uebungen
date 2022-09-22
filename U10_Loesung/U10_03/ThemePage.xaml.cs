namespace U10_03;

public partial class ThemePage : ContentPage
{
    public ThemePage()
    {
        InitializeComponent();
        InitializeRadioButtons();
    }

    private static App App => ((App)Application.Current)!;

    private void InitializeRadioButtons()
    {
        switch (App.ActiveTheme)
        {
            case App.Theme.Blue:
                BlueOption.IsChecked = true;
                break;

            case App.Theme.Green:
                GreenOption.IsChecked = true;
                break;

            case App.Theme.Red:
                RedOption.IsChecked = true;
                break;
        }
    }

    private void ApplyButton_OnClicked(object sender, EventArgs e)
    {
        App.Theme newTheme;

        if (BlueOption.IsChecked)
        {
            newTheme = App.Theme.Blue;
        }
        else if (GreenOption.IsChecked)
        {
            newTheme = App.Theme.Green;
        }
        else
        {
            newTheme = App.Theme.Red;
        }

        App.ActiveTheme = newTheme;

        Application.Current.CloseWindow(Window);
    }
}

