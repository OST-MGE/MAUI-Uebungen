namespace U11_01;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        var timer = CreateTimer();
        var clock = new Clock(timer, TimeSpan.Zero);

        BindingContext = clock;
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
