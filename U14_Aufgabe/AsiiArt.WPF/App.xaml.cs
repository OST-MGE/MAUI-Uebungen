using System.Windows;
using U14.Core.Infrastructure;
using U14.Core.ViewModel;
using U14.WPF.Service;
using U14.WPF.View.Windows;

namespace U14.WPF;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        RelayCommand.Dispatch = Dispatcher.Invoke;

        var platformSupport = new WpfPlatformSupport();
        var viewModel = new AsciiArtViewModel(platformSupport);

        MainWindow = new AsciiArtWindow(viewModel);
        MainWindow.Show();
    }
}