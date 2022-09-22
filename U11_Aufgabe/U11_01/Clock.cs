namespace U11_01;

public class Clock
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

    #region Private Helper Methods

    private void DoOnTimerElapsed(object sender, EventArgs e)
    {
        var time = DateTime.UtcNow.Add(_offset);
        TimeString = time.ToString("dd.MM.yyyy HH:mm:ss");
    }

    #endregion
}