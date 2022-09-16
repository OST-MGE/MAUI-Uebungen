using U13.Core.ViewModel;

namespace U13.MAUI.View.Pages;

public partial class AsciiArtPage : ContentPage
{
    public AsciiArtPage(IAsciiArtViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}

