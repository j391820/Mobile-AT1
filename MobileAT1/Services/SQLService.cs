using MobileAT1.Models;
using SQLite;
using System;
using System.Collections.Generic;


namespace MobileAT1.Services
{
    public class SQLService
    {
        SQLiteAsyncConnection database;

        public SQLService(string databasePath)
        {
            database = new SQLiteAsyncConnection(databasePath);
            // create new table.
            database.CreateTableAsync<FavouriteID>().Wait();
        }

        // Browse
        /// <summary>
        /// Get all game ids.
        /// </summary>
        /// <returns>All the game ids stored in the database.</returns>
        public async Task<List<FavouriteID>> GetAllGames()
        {
            return await database.Table<FavouriteID>().ToListAsync();
        }

        // Add
        /// <summary>
        /// Add new game to database.
        /// </summary>
        /// <param name="game">Game to store.</param>
        public async Task Add(Game game)
        {
            // just grab and store the id from the game.
            await database.InsertAsync(new FavouriteID { GameId = game.Id});
        }

        // Delete
        /// <summary>
        /// Remove game from database.
        /// </summary>
        /// <param name="game">Game to delete.</param>
        /// <returns></returns>
        public async Task Delete(Game game)
        {
            // just grab the id from the game and delete from database.
            await database.DeleteAsync<FavouriteID>(game.Id);
        }
    }
}
