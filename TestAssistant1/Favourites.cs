using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace TestAssistant1
{
    public class Favourites
    {
        // Represents the local copy of the favourites in the app.
        // Observable collection will notify the UI when games are added or removed refreshing that page.
        public ObservableCollection<Game> FavouritesList { get; } = new();


        /// <summary>
        /// Checks if game has already been favourited.
        /// </summary>
        /// <param name="gameId">game id that you want to check is in favourites list.</param>
        /// <returns>bool.</returns>
        public bool IsFavourite(int gameId)
        {
            foreach (Game favourite in FavouritesList)
            {
                if (favourite.Id == gameId)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Removes/adds game id from/to sqlite database.
        /// Removes/adds game object from/to local list.
        /// Toggles game favourited status.
        /// </summary>
        /// <param name="game">Game object that you want to reference.</param>
        public async Task Toggle(Game game)
        {
            //try
            //{


            if (IsFavourite(game.Id))
            {
                //await _database.Delete(game);
                // Compares parsed in game with game in list via id.
                // Because technically they are different instances of the same object and evaluate the same. 
                Game gameInList = FavouritesList.First(g => g.Id == game.Id);

                FavouritesList.Remove(gameInList);
                game.Favourited = false;
            }
            else
            {
                //await _database.Add(game);

                FavouritesList.Add(game);
                game.Favourited = true;
            }
            //}
            //catch (Exception ex)
            //{
            //    System.Diagnostics.Debug.WriteLine(ex.ToString());
            //}
        }
    }

    }
