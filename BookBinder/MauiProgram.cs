using BookBinder.Data;
using BookBinder.Services;
using BookBinder.Services.Files;
using BookBinder.Services.Files.FileRequests;
using BookBinder.UI;
using BookBinder.Utils;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Storage;
using LiteDB;
using Microsoft.Extensions.Logging;
using MudBlazor;
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
            builder.Services.AddMudServices(config =>
            {
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;
                config.SnackbarConfiguration.ShowCloseIcon = true;
                config.SnackbarConfiguration.VisibleStateDuration = 1500;
                config.SnackbarConfiguration.HideTransitionDuration = 500;
                config.SnackbarConfiguration.ShowTransitionDuration = 500;
                config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
            });
            builder.Services.AddScoped<StateContainer>();
            builder.Services.AddScoped<ITextFileExport, TextFileExport>();

            builder.Services.AddTransient<IFilePicker>(x => FilePicker.Default);
            builder.Services.AddTransient<IFileSaver>(x => FileSaver.Default);
            builder.Services.AddTransient<IShare>(x => Share.Default);
            builder.Services.AddTransient<IFilePickerRequest, TextFilePickerRequest>();

            builder.Services.AddScoped<IFileImport, TextFileImport>();
            builder.Services.AddSingleton<AppState>();
            builder.Services.AddSingleton<IClipboard>(x => Clipboard.Default);

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
