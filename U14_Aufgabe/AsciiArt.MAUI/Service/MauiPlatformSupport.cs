using U14.Core.Service;

namespace U14.MAUI.Service;

/// <summary>
/// Enthält MAUI-spezifische Hilfsmethoden. Wird vom View Model indirect über Callbacks
/// verwendet, um plattform-spezifische Aktionen auszuführen. So bleibt das View Model
/// frei von konkreten Technologien.
/// </summary>
public sealed class MauiPlatformSupport : IPlatformSupport
{
    private readonly Application _application;

    public MauiPlatformSupport(Application application)
    {
        _application = application;
    }

    /// <summary>
    /// Zeigt ein Datei-Öffnen Dialogfenster, erlaubt die Auswahl einer
    /// Bilddatei und weist das ausgewählte Bild der Property ImagePath zu.
    /// </summary>
    public async Task<string> ChooseFileAsync()
    {
        // Fix for an error in MAUI when running on macOS - No picker opens when using a filter
        // See: https://github.com/dotnet/maui/issues/11088
#if MACCATALYST
        var result = await FilePicker.Default.PickAsync();
#else
        var result = await FilePicker.Default.PickAsync(PickOptions.Images);
#endif
        
        var pathToFile = result?.FullPath;

        return pathToFile;
    }

    /// <summary>
    /// Zeigt eine Fehlermeldung in einer Message Box an.
    /// </summary>
    /// <param name="title">Der Titel für die Fehlermeldung</param>
    /// <param name="msg">Die Nachricht für die Fehlermeldung</param>
    public Task ShowErrorAsync(string title, string msg)
    {
        return _application.MainPage!.DisplayAlert(title, msg, "OK");
    }
}