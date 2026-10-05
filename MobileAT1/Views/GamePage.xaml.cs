using MobileAT1.Models;
using MobileAT1.Services;



namespace MobileAT1.Views;

[QueryProperty(nameof(GameId), "gameId")]
public partial class GamePage : ContentPage
{
    IGDBApiService _api = new IGDBApiService();
    Favourites _favourites = App.favouritesList;

    public int GameId { get; set; }
    public GamePage()
	{
		InitializeComponent();

    }

    private async void OnPageLoaded(object sender, EventArgs e)
    {
        BindingContext = await _api.GetGameById(GameId);
    }


    private void FavouriteButton_Clicked(object sender, EventArgs e)
    {
        if (this.BindingContext is Game game)
        {
            _favourites.toggle(game);
        }
    }

    private void PriceButton_Clicked(object sender, EventArgs e)
    {

    }

    private async void CloseButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}