using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MobileAT1.Models
{
    public class Favourites
    {
        public ObservableCollection<Game> FavouritesList { get;} = new();

        public void toggle(Game game)
        {
            Game gameInList = FavouritesList.FirstOrDefault(g => g.Id == game.Id);
            if (gameInList != null) 
            { 
                FavouritesList.Remove(gameInList);
                game.Favourited = false;
                //game.Icon = "add_favourite.png";
            }
            else
            {
                game.Favourited = true;
                FavouritesList.Add(game);
                
                //game.Icon = "favourited.png";
            }
        }

    }
    
}
