using System.ComponentModel;

namespace U11_02;

// ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
public class Clock : INotifyPropertyChanged
{
    private readonly TimeSpan _offset;

    // Timer wird injected, damit wir einen "globalen" Taktgeber haben
    public Clock(System.Timers.Timer timer, TimeSpan offset)
    {
        _offset = offset;
        DoOnTimerElapsed(null, EventArgs.Empty);

        timer.Elapsed += DoOnTimerElapsed;
    }

    public string TimeString { get; private set; } = "??.??.???? ??:??:??";

    #region INotifyPropertyChanged

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion

    #region Private Helper Methods

    private void DoOnTimerElapsed(object sender, EventArgs e)
    {
        var time = DateTime.UtcNow.Add(_offset);
        TimeString = time.ToString("dd.MM.yyyy HH:mm:ss");
        OnPropertyChanged(nameof(TimeString));
    }

    #endregion
}