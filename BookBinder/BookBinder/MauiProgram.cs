using UraniumUI;

namespace BookBinder;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseUraniumUI()
            .UseUraniumUIMaterial()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("FontAwesome6FreeSolid.otf", "FontAwesomeSolid");
            });

        builder.Services.AddSingleton<MainViewModel>();

        builder.Services.AddSingleton<MainPage>();

        builder.Services.AddTransient<SampleDataService>();
        builder.Services.AddTransient<ListDetailDetailViewModel>();
        builder.Services.AddTransient<ListDetailDetailPage>();

        builder.Services.AddSingleton<ListDetailViewModel>();

        builder.Services.AddSingleton<ListDetailPage>();

        builder.Services.AddSingleton<LocalizationViewModel>();

        builder.Services.AddSingleton<LocalizationPage>();

        return builder.Build();
    }
}
