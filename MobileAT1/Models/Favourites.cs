using MobileAT1.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MobileAT1.Models
{
    public class Favourites
    {
        // Represents the local copy of the favourites in the app.
        // Observable collection will notify the UI when games are added or removed refreshing that page.
        public ObservableCollection<Game> FavouritesList { get; } = new();
        IGDBApiService _api = new IGDBApiService();
        private readonly SQLService _database;

        public Favourites(SQLService database)
        {
            _database = database;
        }

        /// <summary>
        /// Queries SQLite data for all stored game ids. 
        /// Uses ids to Query IGDB to build out game objects to add to local list.
        /// </summary>
        /// <returns></returns>
        public async Task LoadFavourites()
        {
            FavouritesList.Clear();
            List<FavouriteID> favouritesid = await _database.GetAllGames();

            foreach (FavouriteID favouriteID in favouritesid) 
            {
               Game game = await _api.GetGameById(favouriteID.GameId);
               if (game != null)
                {
                    // Is in list so should be true.
                    game.Favourited = true;
                    FavouritesList.Add(game);
                }

            }
            
        }

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
            try
            {


            if (IsFavourite(game.Id))
            {
                await _database.Delete(game);
                // Compares parsed in game with game in list via id.
                // Because technically they are different instances of the same object and evaluate the same. 
                Game gameInList = FavouritesList.First(g => g.Id == game.Id);

                FavouritesList.Remove(gameInList);
                game.Favourited = false;
            }
            else
            {
                await _database.Add(game);

                FavouritesList.Add(game);
                game.Favourited = true;
            }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }
    }

    }
