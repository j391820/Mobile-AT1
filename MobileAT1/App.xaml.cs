using Microsoft.Extensions.DependencyInjection;
using MobileAT1.Models;
using System.Collections.ObjectModel;

namespace MobileAT1
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        public static Favourites favouritesList = new Favourites();

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}