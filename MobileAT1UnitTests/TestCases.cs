using System.Collections;
using System.Diagnostics.Contracts;
using TestAssistant1;

namespace MobileAT1UnitTests
{
    [TestClass]
    public sealed class TestCases
    {
        // Instance of classes
        // API's
        IGDBApiService games;
        CheapSharkApiService deals;

        Favourites myFavourites;

        Game myGame;

        // List of model objects
        private List<Genre> myGenre;
        private List<GameMode> myGameModes;
        private List<Platform> myPlatforms;
        private List<Game> myGames;
        private List<Deal> myDeals;


        [TestInitialize]
        public void Setup()
        {
            games = new IGDBApiService();
            deals = new CheapSharkApiService();
            myFavourites = new Favourites();


            // Lists
            // Create a list of deals as test seed data
            myDeals = 
                [
                    new Deal(19.99, 1, "1"),
                    new Deal(9.99, 2, "2"),
                    new Deal(14.50, 3, "3"),
                    new Deal(4.99, 4, "4")
                ];

            // Create a list of genre as test seed data
            myGenre = 
                [
                    new Genre("Action"),
                    new Genre("Puzzle"),
                    new Genre("Arcade"),
                    new Genre("2D")
                ];


            // Create a list of GameModes as test seed data
            myGameModes = 
                [
                    new GameMode("Single"),
                    new GameMode("Multiplayer")
                ];

            // Create a list of platforms as test seed data
            myPlatforms = 
                [
                    new Platform("Computer"),
                    new Platform("Nintendo Switch"),
                    new Platform("PS5"),
                    new Platform("Xbox")
                ];

            // Create a list of games as test seed data
            myGames =
            [
                new Game(1, "The Witcher 3", new Cover(), myGenre, myPlatforms, myGameModes, 19.99, string.Empty, true),
                new Game(2, "Grand Theft Auto", new Cover(), [], [], [], 29.99, string.Empty),
                new Game(3, "Minecraft", new Cover(), [], [], [], 24.50, string.Empty),
                new Game(4, "Stardew Valley", new Cover(), [], [], [], 14.99, string.Empty)
            ];

            // select an individual game
            myGame = myGames[0];

            // add game to favourites
            myFavourites.FavouritesList.Add(myGame);


        }

        // IGDBApiSerice
        // games.NormaliseName
        [TestMethod]
        public void TestAllCapsGameConvertToTitleCase()
        {
            // Arrange
            string myGameTitle = myGames[1].Title.ToUpper();
            // Act
            string result = games.NormaliseName(myGameTitle);
            // Assert
            string expected = "Grand Theft Auto";
            Assert.AreEqual(expected, result);

        }

        [TestMethod]
        public void TestLowerCaseGameConvertToTitleCase()
        {
            // Arrange
            string myGameTitle = myGames[1].Title.ToLower();
            // Act
            string result = games.NormaliseName(myGameTitle);
            // Assert
            string expected = "Grand Theft Auto";
            Assert.AreEqual(expected, result);

        }



        // CheapSharkApi Service
        // deals.capitaliseTitle
        [TestMethod]
        public void TestLowerCaseGameConvertToAllCaps()
        {
            // Arrange
            string myGameTitle = myGames[1].Title.ToLower();
            // Act
            string result = deals.capitaliseTitle(myGameTitle);
            // Assert
            string expected = "GRANDTHEFTAUTO";
            Assert.AreEqual(expected, result);

        }

        [TestMethod]
        public void TestRemoveSpaceFromGameTitle()
        {
            // Arrange
            string myGameTitle = myGames[3].Title;
            // Act
            string result = deals.capitaliseTitle(myGameTitle);
            // Assert
            string expected = "STARDEWVALLEY";
            Assert.AreEqual(expected, result);

        }

        [TestMethod]
        public void TestRemoveSpecialCharactersFromGameTitle()
        {
            // Act
            string result = deals.capitaliseTitle("Deus Ex: Mankind Divided");
            // Assert
            string expected = "DEUSEXMANKINDDIVIDED";
            Assert.AreEqual(expected, result);

        }

        // deals.FindCheapestDeal
        [TestMethod]
        public void TestCanFindCheapestDeal()
        {
            // Act
            Deal result = deals.FindCheapestDeal(myDeals);
            // Assert
            Deal expected = myDeals[3];
            Assert.AreSame(expected, result);

        }

        [TestMethod]
        public void TestFindCheapestDealNullValueReturnsDefaultValue()
        {
            // Arrange
            List<Deal> deal = null;
            // Act
            Deal result = deals.FindCheapestDeal(deal);
            // Assert
            Deal expected = new Deal(0, 0, string.Empty);
            Assert.AreEqual(0, result.storeID);
            Assert.AreEqual(0, result.salePrice);

        }

        [TestMethod]
        public void TestFindCheapestDealEmptyListReturnsDefaultValue()
        {
            // Arrange
            List<Deal> deal = [];
            // Act
            Deal result = deals.FindCheapestDeal(deal);
            // Assert
            Deal expected = new Deal(0, 0, string.Empty);
            Assert.AreEqual(0, result.storeID);
            Assert.AreEqual(0, result.salePrice);

        }

        // Games object
        // Game.Genre
        [TestMethod]
        public void TestCanDisplayListOfGenre()
        {
            // Act
            string result = myGame.GenreDisplay;
            // Assert
            string expected = "Action, Puzzle, Arcade, 2D";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestWhenNullDisplayListOfGenreReturnNotFound()
        {
            // Arrange
            myGame.Genre = null;
            // Act
            string result = myGame.GenreDisplay;
            // Assert
            string expected = "Not found";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestWhenEmptyListDisplayListOfGenreReturnNotFound()
        {

            // Arrange
            myGame.Genre = [];
            // Act
            string result = myGame.GenreDisplay;
            // Assert
            string expected = "Not found";
            Assert.AreEqual(expected, result);
        }

        // Game.Platforms
        [TestMethod]
        public void TestCanDisplayListOfPlatforms()
        {
            // Act
            string result = myGame.PlatformDisplay;
            // Assert
            string expected = "Computer, Nintendo Switch, PS5, Xbox";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestWhenNullDisplayListOfPlatformsReturnNotFound()
        {
            // Arrange
            myGame.Platforms = null;
            // Act
            string result = myGame.PlatformDisplay;
            // Assert
            string expected = "Not found";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestWhenEmptyListDisplayListOfPlatformsReturnNotFound()
        {
            // Arrange
            myGame.Platforms = [];
            // Act
            string result = myGame.PlatformDisplay;
            // Assert
            string expected = "Not found";
            Assert.AreEqual(expected, result);
        }

        // Game.GameMode
        [TestMethod]
        public void TestCanDisplayListOfGameMode()
        {
            // Act
            string result = myGame.GameModeDisplay;
            // Assert
            string expected = "Single, Multiplayer";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestWhenNullDisplayListOfGameModeReturnNotFound()
        {
            myGame.GameModes = null;
            // Act
            string result = myGame.GameModeDisplay;
            // Assert
            string expected = "Not found";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestWhenEmptyListDisplayListOfGameModeReturnNotFound()
        {

            // Arrange
            myGame.GameModes = [];
            // Act
            string result = myGame.GameModeDisplay;
            // Assert
            string expected = "Not found";
            Assert.AreEqual(expected, result);
        }

        //Game.CoverImageUrl
        [TestMethod]
        public void TestNullUrlReturnPlaceHolderImageURL() 
        {
            myGame.Cover.Url = null;
            // Act
            string result = myGame.CoverImageUrl;
            // Assert
            string expected = "cover_placeholder.png";
            Assert.AreEqual(expected, result);

        }

        // Favourites
        // toggle game
        [TestMethod]
        public async Task TestToggleGameAdd()
        {
            // Arrange
            Game anotherGame = myGames[3];
            // Act
            await myFavourites.Toggle(anotherGame);
            // Assert
            bool result = myFavourites.FavouritesList.Contains(anotherGame);
            Assert.IsTrue(result);
            Assert.IsTrue(anotherGame.Favourited);


        }

        [TestMethod]
        public async Task TestToggleGameRemove()
        {
            // Act
            await myFavourites.Toggle(myGame);
            // Assert
            bool result = myFavourites.FavouritesList.Contains(myGame);
            Assert.IsFalse(result);
            Assert.IsFalse(myGame.Favourited);
        }

        // myFavourites.IsFavourite
        [TestMethod]
        public void TestGameIsFavourited()
        {
            // Arrange
            int gameID = 1;
            // Act
            bool result = myFavourites.IsFavourite(gameID);
            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestGameIsNotFavourited()
        {
            // Arrange
            int gameID = 3;
            // Act
            bool result = myFavourites.IsFavourite(gameID);
            // Assert
            Assert.IsFalse(result);
        }

    }
}
