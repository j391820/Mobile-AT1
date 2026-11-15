using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using MobileAT1.Models;
using MobileAT1.Services;

namespace MobileAT1
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            string databasePath = Path.Combine(FileSystem.AppDataDirectory,"favourites.db");
            builder
                .UseMauiApp<App>()

                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("MaterialSymbolsRounded.ttf", "MaterialSymbolsRounded");
                });
            // Load/save game ids to sql database.
            builder.Services.AddSingleton<SQLService>(s => new SQLService(databasePath));
            // Create a shared instance of favourites so the list and functions can be accessed and modified by multiple pages.
            builder.Services.AddSingleton<Favourites>();
            builder.UseMauiCommunityToolkit();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
