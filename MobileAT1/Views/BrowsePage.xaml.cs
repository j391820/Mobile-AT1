using MobileAT1.Models;
using MobileAT1.Services;

namespace MobileAT1.Views;

public partial class BrowsePage : ContentPage
{
    IGDBApiService _api = new IGDBApiService();
    private bool _hasLoadedData = false;
    public BrowsePage()
	{
		InitializeComponent();

    }
   
    
    private async void GenrePicker_Focused(object sender, FocusEventArgs e)
        {
            GenrePicker.ItemsSource = await _api.GetGenre();
        }

    private async void OnPageLoaded(object sender, EventArgs e)
    {
        if (_hasLoadedData == true) 
        {
            return;
            
        }
        else
        {
            CollectionViewGames.ItemsSource = await _api.GetAllGames();
            _hasLoadedData = true;
        }
        
    }

    private async void GenrePicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (GenrePicker.SelectedIndex != -1) { 
            string selectedGenre = GenrePicker.Items[GenrePicker.SelectedIndex];
            CollectionViewGames.ItemsSource = await _api.GetGamesByGenre(selectedGenre);
        }
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
}