using CommunityToolkit.Mvvm.ComponentModel;
using MobileAT1.Models;
using MobileAT1.Services;
using System.Collections.ObjectModel;


namespace MobileAT1.Views;

[QueryProperty(nameof(Matches), "Matches")]
public partial class SearchPage : ContentPage
{
    IGDBApiService _api = new IGDBApiService();
    public ObservableCollection<Game> Games { get; set; } = new();
    private List<Game> _matches;

    // Convert passed in ditionary to a list of matched games.
    public List<Game> Matches
    {
        get => _matches;
        set
        {
            _matches = value;
            Games.Clear(); // clear previous collection.
            if (_matches == null || _matches.Count == 0)
            {
                return;
            }
            // for each game in list add to collection.
            foreach (Game game in _matches)
            {
                Games.Add(game);
            }
        }
    }

    public SearchPage()
	{
		InitializeComponent();
	}



    /// <summary>
    /// When page first loads sets binding context to collection.
    /// </summary>
    private async void OnPageLoaded(object sender, EventArgs e)
    {
        CollectionViewGames.ItemsSource = Games;
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

    /// <summary>
    /// When clicked close page.
    /// </summary>
    private async void CloseButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}