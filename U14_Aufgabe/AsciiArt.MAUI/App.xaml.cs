using U14.Core.Infrastructure;
using U14.Core.ViewModel;
using U14.MAUI.Service;
using U14.MAUI.View.Pages;

namespace U14.MAUI;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        RelayCommand.Dispatch = MainThread.BeginInvokeOnMainThread;

        var platformSupport = new MauiPlatformSupport(this);
        var viewModel = new AsciiArtViewModel(platformSupport);

        MainPage = new AsciiArtPage(viewModel);
    }

    protected override Window CreateWindow(IActivationState activationState)
    {
        var window = base.CreateWindow(activationState);
        
        window.Title = "ASCII Art Generator";

        return window;
    }
}
