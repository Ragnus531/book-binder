using BookBinder.Data;
using BookBinder.Services;
using BookBinder.Services.Files;
using BookBinder.Utils;
using CommunityToolkit.Maui;
using LiteDB;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;

namespace BookBinder
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("Abstante.ttf", "Abstante");
                });

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddScoped<INoteService, NoteService>();
            var path = Path.Combine(FileSystem.AppDataDirectory, "book-notes.db");
            //add as a singleton - it's a single file with a single access point
            builder.Services.AddSingleton<ILiteDatabase, LiteDatabase>(x => new LiteDatabase(path));
            builder.Services.AddMudServices();
            builder.Services.AddScoped<StateContainer>();
            builder.Services.AddScoped<ITextFileExport, TextFileExport>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            var app = builder.Build();

#if DEBUG
            DbSeed.Seed(app.Services.GetRequiredService<INoteService>());
#endif

            return app;
        }
    }
}
