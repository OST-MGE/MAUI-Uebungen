using U12.ViewModel;

namespace U12.View.Pages;

public partial class AsciiArtPage : ContentPage
{
    public AsciiArtPage(IAsciiArtViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}

