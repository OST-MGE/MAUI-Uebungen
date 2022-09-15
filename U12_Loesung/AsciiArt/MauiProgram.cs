namespace U12;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("RobotoMono-Regular.ttf", "RobotoMonoRegular");
                fonts.AddFont("RobotoMono-SemiBold.ttf", "RobotoMonoSemibold");
            });

        return builder.Build();
    }
}
