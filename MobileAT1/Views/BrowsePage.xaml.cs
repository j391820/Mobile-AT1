using MobileAT1.Models;
using MobileAT1.Services;
using System.Collections.ObjectModel;


namespace MobileAT1.Views;

public partial class BrowsePage : ContentPage
{
    IGDBApiService _api = new IGDBApiService();
    private bool _hasLoadedData = false;
    private bool _isLoading = false;
    private bool _moreGames = true;
    private int offset = 0;
    public ObservableCollection<Game> Games { get; set; } = new();

    private readonly Favourites _favourites;
    public BrowsePage(Favourites favourites)
	{
		InitializeComponent();
        _favourites = favourites;
        // provide list of game objects to databind to.
        // Moved here from on page loaded as it caused the list to jump to top upon pagination
        // Only setup once then added to.
        CollectionViewGames.ItemsSource = Games;

    }

   
    ///// <summary>
    ///// When picker is seletected fetches list of genres from server.
    ///// </summary>
    //private async void GenrePicker_Focused(object sender, FocusEventArgs e)
    //    {
    //        GenrePicker.ItemsSource = await _api.GetGenre();
    //    }

    /// <summary>
    /// When page first loads fetches games from server to populate collectionview.
    /// Prevents further reloads.  
    /// </summary>
    private async void OnPageLoaded(object sender, EventArgs e)
    {
        // Load page data once on first navigation to page
        if (_hasLoadedData == true)
        {
            return;

        }
        else
        {
            try 
            {
                // Get list of genres
                GenrePicker.ItemsSource = await _api.GetGenre();
                await GetGames();
                // loads favourited status from sql.
                await _favourites.LoadFavourites();

                // Flag original load has finished.
                _hasLoadedData = true;
            }
            catch (HttpRequestException)
            {
                await Shell.Current.GoToAsync("nointernetpage");
            }
        }
        
    }

    /// <summary>
    /// When a genre is selected repopulates collectionview with data specific to that genre.
    /// </summary>
    private async void GenrePicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (GenrePicker.SelectedIndex != -1) {
            // genre item at selected index.
            string selectedGenre = GenrePicker.Items[GenrePicker.SelectedIndex];
            // Search IGDB for games of selected genre.
            List<Game> games = await _api.GetGamesByGenre(selectedGenre);
            // check if any of the games have been favourited.
            foreach (Game game in games)
            {
                game.Favourited = _favourites.IsFavourite(game.Id);
            }
            CollectionViewGames.ItemsSource = games;
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
            // pass selected game id as parameter to page.
            await Shell.Current.GoToAsync($"gamepage?gameId={selectedId}");
            CollectionViewGames.SelectedItem = null;
        }
    }

    /// <summary>
    /// Gets next page of games when user reaches Remaining Items Threshold eg. pagination.
    /// </summary>>
    private async void CollectionViewGames_RemainingItemsThresholdReached(object sender, EventArgs e)
    {
        // had issue where pagination stole focus from picker
        if (GenrePicker.IsFocused) return;
        await GetGames();
    }

    /// <summary>
    /// Gets a page of 50 games depedening on an offset.  
    /// </summary>
    private async Task GetGames()
    {
        // had issue where it would endlessly fire as scrolling.
        // if already loading dont fire again.
        if (_isLoading) return;
        // had issue where it would continue to fire even though at end of results.
        if (!_moreGames) return;
        else
        {
            _isLoading = true;
            
            try
            {
                List<Game> newGames = await _api.GetGames(offset);
                // Add each game to ObservableCollection.
                foreach (Game game in newGames)
                {
                    // Check if game has already been favourited.
                    game.Favourited = _favourites.IsFavourite(game.Id);
                    Games.Add(game);
                }

                // increase count for next chunk of games.
                offset += 50;

                // check if chunk is smaller then 50.
                if (newGames == null || newGames.Count < 50)
                {
                    _moreGames = false;
                }
            }
            finally
            {
                _isLoading = false;
            }
        }
    }
    }