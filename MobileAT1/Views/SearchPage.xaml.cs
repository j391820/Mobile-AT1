

using MobileAT1.Models;
using MobileAT1.Services;

namespace MobileAT1.Views;

[QueryProperty(nameof(GameName), "gameName")]
public partial class SearchPage : ContentPage
{
    IGDBApiService _api = new IGDBApiService();

    public string GameName { get; set; }
    public SearchPage()
	{
		InitializeComponent();
	}

    private async void OnPageLoaded(object sender, EventArgs e)
    {
        BindingContext = await _api.SearchGame(GameName);
    }

    private async void CollectionViewGames_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Game game)
        {
            int selectedId = game.Id;
            await Shell.Current.GoToAsync($"gamepage?gameId={selectedId}");
            CollectionViewGames.SelectedItem = null;
        }
    }

    private async void CloseButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}