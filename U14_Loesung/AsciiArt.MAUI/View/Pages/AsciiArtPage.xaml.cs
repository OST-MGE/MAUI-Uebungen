using U14.Core.ViewModel;

namespace U14.MAUI.View.Pages;

public partial class AsciiArtPage : ContentPage
{
    public AsciiArtPage(IAsciiArtViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}

