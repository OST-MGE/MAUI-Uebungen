using System.Windows.Input;
using U13.Core.Infrastructure;
using U13.Core.Model;
using U13.Core.Service;

namespace U13.Core.ViewModel;

public sealed class AsciiArtViewModel : BindableBase, IAsciiArtViewModel
{
    private readonly IPlatformSupport _platformSupport;
    private readonly Generator _generator;
    private readonly RelayCommand _chooseImageCommand;
    private readonly RelayCommand _createAsciiCommand;

    private int _fontSize;
    private int _lineWidth;
    private bool _isCalculating;
    private string _imagePath;
    private string _result;

    public AsciiArtViewModel(IPlatformSupport platformSupport)
    {
        _platformSupport = platformSupport;
        _generator= new Generator();
        _chooseImageCommand = new RelayCommand(OnChooseImageCommandExecute);
        _createAsciiCommand = new RelayCommand(OnCreateAsciiCommandExecute, OnCreateAsciiCommandCanExecute);

        FontSize = 12;
        LineWidth = 120;
        ImagePath = string.Empty;
        Result = string.Empty;
        IsCalculating = false;
    }

    #region IAsciiArtViewModel

    public int FontSize
    {
        get => _fontSize;
        set
        {
            if (value < MinimumFontSize)
                value = MinimumFontSize;

            if (value > MaximumFontSize)
                value = MaximumFontSize;

            SetProperty(ref _fontSize, value);
        }
    }

    public int MinimumFontSize => 2;

    public int MaximumFontSize => 20;

    public int LineWidth
    {
        get => _lineWidth;
        set
        {
            if (value < MinimumLineWidth)
                value = MinimumLineWidth;

            if (value > MaximumLineWidth)
                value = MaximumLineWidth;

            SetProperty(ref _lineWidth, value);
        }
    }

    public int MinimumLineWidth => 80;

    public int MaximumLineWidth => 320;

    public string ImagePath
    {
        get => _imagePath;
        set => SetProperty(ref _imagePath, value);
    }

    public string Result
    {
        get => _result;
        private set => SetProperty(ref _result, value);
    }

    public bool IsCalculating
    {
        get => _isCalculating;
        private set
        {
            SetProperty(ref _isCalculating, value);
            _createAsciiCommand.RaiseCanExecuteChanged();
        }
    }

    public ICommand ChooseImageCommand => _chooseImageCommand;

    public ICommand CreateAsciiCommand => _createAsciiCommand;

    #endregion

    #region Private Methods

    private async void OnChooseImageCommandExecute()
    {
        var filename = await _platformSupport.ChooseFileAsync();

        if (!string.IsNullOrEmpty(filename))
        {
            ImagePath = filename;
        }
    }

    public async void OnCreateAsciiCommandExecute()
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

        await Task.Run(async () =>
        {
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
        });
    }

    private bool OnCreateAsciiCommandCanExecute() => !IsCalculating;

    private Task ShowErrorAsync(string title, string msg) => _platformSupport.ShowErrorAsync(title, msg);

    #endregion
}