using MobileAT1.Models;
using MobileAT1.Services;
using Microsoft.Maui.ApplicationModel;



namespace MobileAT1.Views;

// id passed in by shell navigation
[QueryProperty(nameof(GameId), "gameId")]
public partial class GamePage : ContentPage
{
    IGDBApiService _api = new IGDBApiService();
    CheapSharkApiService _cheapAPI = new CheapSharkApiService();
    ExchangeRateApiService _exchangeRateAPI = new ExchangeRateApiService();
    private readonly Favourites _favourites;
    private Deal _deal;
    private double rate;

    public int GameId { get; set; }
    public GamePage(Favourites favourites)
	{
		InitializeComponent();
        // Consume singleton
        _favourites = favourites;

    }

    /// <summary>
    /// When page loads fetches the game via its id from server to populate page. 
    /// </summary>
    private async void OnPageLoaded(object sender, EventArgs e)
    {
        try
        {
            Game game = await _api.GetGameById(GameId);
            // Get favourited status from favourites list.
            game.Favourited = _favourites.IsFavourite(game.Id);
            MySpinner.IsRunning = true;
            MySpinner.IsVisible = true;
            try
            {
                // Get exchange rate.
                rate = await _exchangeRateAPI.GetExchangeRate();
                // Get cheapest deal.
                _deal = await _cheapAPI.GetDeals(game.Title);
                // Get price from deal.
                // Convert to AUD.
                game.Price = _deal.salePrice * rate;
                BindingContext = game;
            }
            finally
            {
                MySpinner.IsRunning = false;
                MySpinner.IsVisible = false;
            }
        }
        catch (HttpRequestException)
        {
            await Shell.Current.GoToAsync("nointernetpage");
        }

    }

    /// <summary>
    /// Adds/removes game from favourited games.
    /// </summary>
    private async void FavouriteButton_Clicked(object sender, EventArgs e)
    {
        // If game that belongs to the clicked button is a game.
        if (this.BindingContext is Game game)
        {
            _favourites.Toggle(game);
        }
    }

    /// <summary>
    /// If button clicked redirects to the deal.
    /// If deal wasnt found alerts the user that a deal was not found.
    /// </summary>
    private async void PriceButton_Clicked(object sender, EventArgs e)
    {
        if(_deal.salePrice != 0)
        {
            Uri uri = new Uri($"https://www.cheapshark.com/redirect?dealID={_deal.dealID}");
            await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }
        else
        {
            await DisplayAlert("Alert", "No deal found.", "OK");
        }

    }

    /// <summary>
    /// Closes game popout. Returns to previous view.
    /// </summary>
    private async void CloseButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}