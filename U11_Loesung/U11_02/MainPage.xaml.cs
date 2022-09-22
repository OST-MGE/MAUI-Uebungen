using System.Collections.ObjectModel;

namespace U11_02;

public partial class MainPage : ContentPage
{
    private readonly System.Timers.Timer _timer;
    private readonly ObservableCollection<Clock> _clocks;

    public MainPage()
    {
        InitializeComponent();
        InitializeTimezonePicker();

        _timer = CreateTimer();
        _clocks = new ObservableCollection<Clock>();

        AddClock(TimeSpan.Zero);

        BindingContext = _clocks;
    }

    private void InitializeTimezonePicker()
    {
        var possibleOffsets = new List<string>();

        for (var i = -12; i <= 14; i++)
            possibleOffsets.Add(i.ToString("+#;-#;0"));

        TimezonePicker.ItemsSource = possibleOffsets;
        TimezonePicker.SelectedIndex = possibleOffsets.IndexOf("0");
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

    private void AddClock(TimeSpan offset) => _clocks.Add(new Clock(_timer, offset));

    private void AddClock_OnClicked(object sender, EventArgs e) => AddClock(TimeSpan.FromHours(int.Parse((string)TimezonePicker.SelectedItem)));
}