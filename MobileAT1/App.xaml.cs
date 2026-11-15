using Microsoft.Extensions.DependencyInjection;
using MobileAT1.Models;
using MobileAT1.Services;
using System.Collections.ObjectModel;

namespace MobileAT1
{
    public partial class App : Application
    {
        public const string MODE = "current_mode";
        public const string SIZE = "current_font_size";
        public App()
        {
            InitializeComponent();
            // Get saved theme
            string savedTheme = Preferences.Get(MODE, "Light");
            if (Enum.TryParse(savedTheme, out AppTheme targetTheme))
            {
                UserAppTheme = targetTheme;
            }
            else
            {
                // Fallback if the saved string doesn't match an AppTheme value
                UserAppTheme = AppTheme.Light;
            }

            // Font Size Setup 
            double savedSize = Preferences.Get(SIZE, 10.0);

            // Apply calculated sizing for other sizes
            Current.Resources["FontSizeBody"] = savedSize;
            Current.Resources["FontSizeHeader"] = savedSize + 8;
            Current.Resources["FontSizeSmall"] = savedSize - 4;

        }


        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}