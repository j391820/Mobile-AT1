using MobileAT1.Models;
using MobileAT1.Services;

namespace MobileAT1
{
    public partial class MainPage : ContentPage
    {
        IGDBApiService _api = new IGDBApiService();

        public MainPage()
        {
            InitializeComponent();
        }

        private async void SearchButtonClicked(object? sender, EventArgs e)
        {
            string gameName = SearchBox.Text;
            await _api.SearchGame(gameName);
            await Shell.Current.GoToAsync($"searchpage?gameName={gameName}");
        }
    }
}
