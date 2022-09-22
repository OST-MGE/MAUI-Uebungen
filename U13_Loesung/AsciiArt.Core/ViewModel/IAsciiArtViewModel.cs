using System.Windows.Input;

namespace U13.Core.ViewModel;

public interface IAsciiArtViewModel
{
    #region Properties für Bindings

    int FontSize { get; set; }

    int MinimumFontSize { get; }

    int MaximumFontSize { get; }

    int LineWidth { get; set; }

    int MinimumLineWidth { get; }

    int MaximumLineWidth { get; }

    string ImagePath { get; set; }

    string Result { get; }

    bool IsCalculating { get; }

    #endregion

    #region Commands für Bindings

    ICommand ChooseImageCommand { get; }

    ICommand CreateAsciiCommand { get; }

    #endregion
}