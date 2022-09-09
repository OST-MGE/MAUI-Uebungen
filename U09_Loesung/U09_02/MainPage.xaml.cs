namespace U09_02;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void ColorSlider_ValueChanged(object sender, ValueChangedEventArgs eventArgs)
    {
        var r = (byte)SliderR.Value;
        var g = (byte)SliderG.Value;
        var b = (byte)SliderB.Value;

        EntryR.Text = r.ToString();
        EntryG.Text = g.ToString();
        EntryB.Text = b.ToString();

        var color = Color.FromRgb(r, g, b);
        ColorArea.Background = new SolidColorBrush(color);
        ColorLabel.Text = color.ToHex();
    }

    private void EntryR_OnTextChanged(object sender, TextChangedEventArgs eventArgs) => UpdateSliderFromTextBox(SliderR, EntryR);

    private void EntryG_OnTextChanged(object sender, TextChangedEventArgs eventArgs) => UpdateSliderFromTextBox(SliderG, EntryG);

    private void EntryB_OnTextChanged(object sender, TextChangedEventArgs eventArgs) => UpdateSliderFromTextBox(SliderB, EntryB);

    private static void UpdateSliderFromTextBox(IRange slider, InputView textBox)
    {
        // Can be null during startup
        if (slider == null)
            return;

        slider.Value = byte.TryParse(textBox.Text, out var b) ? b : 0;
    }
}

