using System.Threading.Tasks;
using U14.Core.Service;

namespace U14.WPF.Service;

public sealed class WpfPlatformSupport : IPlatformSupport
{
    public Task<string> ChooseFileAsync()
    {
        // TODO [Aufgabe 04] Datei öffnen-Dialog implementieren
        // https://learn.microsoft.com/en-us/dotnet/desktop/wpf/windows/how-to-open-common-system-dialog-box

        return Task.FromResult<string>(null);
    }

    public Task ShowErrorAsync(string title, string msg)
    {
        // TODO [Aufgabe 04] Fehlermeldung mit Dialog anzeigen
        // https://learn.microsoft.com/en-us/dotnet/desktop/wpf/windows/how-to-open-message-box?view=netdesktop-6.0

        return Task.CompletedTask;
    }
}