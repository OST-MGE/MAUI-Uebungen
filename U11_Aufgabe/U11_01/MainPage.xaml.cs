using System.Diagnostics;

namespace U11_01;

public partial class MainPage : ContentPage
{
    private readonly Clock _clock;

    public MainPage()
    {
        InitializeComponent();

        var timer = CreateTimer();
        _clock = new Clock(timer, TimeSpan.Zero);

        timer.Elapsed += DoOnTimerElapsed;
    }

    private void DoOnTimerElapsed(object sender, EventArgs e)
    {
        // Das Signal des Timers kommt von einem Background-Thread.
        // Wie bei Android, dürfen wir auch in .NET MAUI nur vom Main-Thread aus
        // das UI aktualisieren.

        MainThread.BeginInvokeOnMainThread(() =>
        {
            Debug.WriteLine($"Timer elapsed: {_clock.TimeString}");
            ClockLabel.Text = _clock.TimeString;
        });
    }

    private static System.Timers.Timer CreateTimer()
    {
        return new System.Timers.Timer
        {
            Interval = TimeSpan.FromSeconds(1).TotalMilliseconds,
            AutoReset = true,
            Enabled = true
        };
    }
}
