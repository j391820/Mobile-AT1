using MobileAT1.Models;
using MobileAT1.Services;
using CommunityToolkit.Maui.Core.Platform;

namespace MobileAT1
{
    public partial class MainPage : ContentPage
    {
        IGDBApiService _api = new IGDBApiService();

        public MainPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// When search button is clicked searches IGDB for list of matched titles.
        /// Then opens new screen with matches.
        /// </summary>
        private async void SearchButtonClicked(object? sender, EventArgs e)
        {
            string gameName = SearchBox.Text;
            try
            {
                List<Game> matches = await _api.SearchGame(gameName);
                // Convert list of matches into a dictionary of matches.
                // Reccomended way of parsing a list of results to a page.
                Dictionary<string, object> navigationParameters = new Dictionary<string, object>
                {
                    { "Matches", matches }
                };
                // Close keyboard.
                await SearchBox.HideKeyboardAsync(CancellationToken.None);
                // dictionary passed to page as a parameter.
                await Shell.Current.GoToAsync("searchpage", navigationParameters);
            }
            catch (HttpRequestException)
            {
                await Shell.Current.GoToAsync("nointernetpage");
            }
        }

        
    }
}
