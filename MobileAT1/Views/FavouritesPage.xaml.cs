using MobileAT1.Models;

namespace MobileAT1.Views;

public partial class FavouritesPage : ContentPage
{
	public FavouritesPage()
	{
		InitializeComponent();
        CollectionViewGames.ItemsSource = App.favouritesList.FavouritesList;
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