using BookBinder.Data;
using LiteDB;
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
        builder.Services.AddSingleton<BookNoteDetail>();
        builder.Services.AddTransient<BookNoteDetailViewModel>();

        var path = Path.Combine(FileSystem.AppDataDirectory, "book-notes.db");
        //add as a singleton - it's a single file with a single access point
        builder.Services.AddSingleton<ILiteDatabase, LiteDatabase>(x => new LiteDatabase(path));

        builder.Services.AddScoped<INoteService, NoteService>();

        var app = builder.Build();
#if DEBUG
        DbSeed.Seed(app.Services.GetRequiredService<INoteService>());
#endif

        return app;
    }
}
