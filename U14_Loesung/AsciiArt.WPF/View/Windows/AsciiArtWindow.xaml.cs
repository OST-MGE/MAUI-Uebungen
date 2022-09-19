using System.Windows;
using System.Windows.Input;
using U14.Core.ViewModel;

namespace U14.WPF.View.Windows;

public partial class AsciiArtWindow : Window
{
    private readonly IAsciiArtViewModel _viewModel;

    public AsciiArtWindow(IAsciiArtViewModel viewModel)
    {
        InitializeComponent();
        DataContext = _viewModel = viewModel;
    }

    private void OutputText_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
    {
        if (e.Delta < 0)
        {
            _viewModel.FontSize++;
        }
        else
        {
            _viewModel.FontSize--;
        }

        e.Handled = true;
    }
}