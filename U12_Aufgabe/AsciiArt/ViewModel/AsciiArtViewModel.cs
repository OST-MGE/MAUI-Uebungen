using System.Windows.Input;
using U12.Infrastructure;
using U12.Model;

namespace U12.ViewModel;

public sealed class AsciiArtViewModel : IAsciiArtViewModel
{
    private readonly Generator _generator;

    public AsciiArtViewModel()
    {
        _generator= new Generator();

        ChooseImageCommand = new RelayCommand(() => { }, () => false);
        CreateAsciiCommand = new RelayCommand(() => { }, () => false);

        FontSize = 12;
        LineWidth = 120;
        ImagePath = string.Empty;
        Result = string.Empty;
        IsCalculating = false;
    }

    #region IAsciiArtViewModel

    public int FontSize { get; set; }

    public int MinimumFontSize => 2;

    public int MaximumFontSize => 20;

    public int LineWidth { get; set; }

    public int MinimumLineWidth => 80;

    public int MaximumLineWidth => 320;

    public string ImagePath { get; set; }

    public string Result { get; private set; }

    public bool IsCalculating { get; private set; }

    public ICommand ChooseImageCommand { get; }

    public ICommand CreateAsciiCommand { get; }

    public Func<Task<string>> OnChooseFileAsync { get; set; } = () => throw new InvalidOperationException($"Zuweisung fehlt: {nameof(OnChooseFileAsync)}");

    public Func<string, string, Task> OnShowErrorAsync { get; set; } = (_, _) => throw new InvalidOperationException($"Zuweisung fehlt: {nameof(OnShowErrorAsync)}");

    #endregion

    #region Private Methods

    private async void OnChooseImageCommandExecute()
    {
        var filename = await OnChooseFileAsync();

        if (!string.IsNullOrEmpty(filename))
        {
            ImagePath = filename;
        }
    }

    private async void OnCreateAsciiCommandExecute()
    {
        if (string.IsNullOrEmpty(ImagePath))
        {
            await ShowErrorAsync("Quelldatei fehlt", "Kann leider nichts berechnen: Keine Quelldatei angegeben");
            return;
        }

        if (!File.Exists(ImagePath))
        {
            await ShowErrorAsync("Quelldatei nicht verfügbar", "Kann leider nichts berechnen: Quelldatei nicht gefunden");
            return;
        }

        try
        {
            Result = string.Empty;
            IsCalculating = true;
            Result = _generator.GenerateFrom(ImagePath, LineWidth);
        }
        catch (Exception e)
        {
            await ShowErrorAsync("Fehler aufgetreten", $"Berechnung fehlgeschlagen. Ursache: {e.Message}");
        }
        finally
        {
            IsCalculating = false;
        }
    }

    private bool OnCreateAsciiCommandCanExecute() => !IsCalculating;

    private Task ShowErrorAsync(string title, string msg) => OnShowErrorAsync(title, msg);

    #endregion
}