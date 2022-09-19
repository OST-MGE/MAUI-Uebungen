using U14.Core.Service;
using U14.Core.ViewModel;
using U14.WPF.Service;

namespace U14.WPF.View.Designer;

public static class DesignerData
{
    private static IPlatformSupport PlatformSupport { get; } = new WpfPlatformSupport();

    public static AsciiArtViewModel ViewModel { get; } = new(PlatformSupport);
}