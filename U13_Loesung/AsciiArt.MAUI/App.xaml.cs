using U13.Core.Infrastructure;
using U13.Core.ViewModel;
using U13.MAUI.Service;
using U13.MAUI.View.Pages;

namespace U13.MAUI;

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
