using MobileAT1.Models;

namespace MobileAT1.Views;

public partial class FavouritesPage : ContentPage
{
    private readonly Favourites _favourites;
    private bool _hasLoaded;
    public FavouritesPage(Favourites favourites)
	{
		InitializeComponent();
        // Consume singleton
        _favourites = favourites;
        CollectionViewGames.ItemsSource = _favourites.FavouritesList;
    }

    /// <summary>
    /// When page first loads fetches games from shared favourites list in app.
    /// Prevents further reloads.  
    /// </summary>
    private async void OnPageLoaded(object sender, EventArgs e)
    {
        if (!_hasLoaded)
        {
            try
            {
                await _favourites.LoadFavourites();
                _hasLoaded = true;
            }
            catch (HttpRequestException)
            {
                await Shell.Current.GoToAsync("nointernetpage");
            }
        }
    }

    /// <summary>
    /// When game is selected opens gamepage and uses URI-based routing to pass selected games id to the gamepage.
    /// </summary>
    private async void CollectionViewGames_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Game game)
        {
            int selectedId = game.Id;
            // pass selected game id as parameter to gamepage
            await Shell.Current.GoToAsync($"gamepage?gameId={selectedId}");
            CollectionViewGames.SelectedItem = null;

        }
    }
}