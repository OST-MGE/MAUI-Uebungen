using U12.View.Pages;
using U12.View.Util;
using U12.ViewModel;

namespace U12;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        var platformSupport = new MauiPlatformSupport(this);

        var viewModel = new AsciiArtViewModel
        {
            OnChooseFileAsync = platformSupport.ChooseFileAsync,
            OnShowErrorAsync = platformSupport.ShowErrorAsync
        };

        MainPage = new AsciiArtPage();
    }

    protected override Window CreateWindow(IActivationState activationState)
    {
        var window = base.CreateWindow(activationState);
        
        window.Title = "ASCII Art Generator";

        return window;
    }
}
