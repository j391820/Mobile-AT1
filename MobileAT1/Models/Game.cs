using Newtonsoft.Json;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MobileAT1.Models
{
    public class Game : ObservableObject
    {
        private bool _favourited = false;

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Title { get; set; }

        [JsonProperty("cover")]
        public Cover? Cover { get; set; }

        [JsonProperty("genres")]
        public List<Genre> Genre { get; set; }

        [JsonProperty("platforms")]
        public List<Platform> Platforms { get; set; }

        [JsonProperty("game_modes")]
        public List<GameMode> GameModes { get; set; }

        public double Price { get; set; }

        [JsonProperty("storyline")]
        public string Synopsis { get; set; }
        public bool Favourited 
        {
            get => _favourited;
            set => SetProperty(ref _favourited, value);

        }

        public string Icon { get; set; }




        public Game()
        {
            Id = 0;
            Title = string.Empty;
            Cover = null;
            Genre = [];
            Platforms = [];
            GameModes = [];
            Price = 0;
            Synopsis = string.Empty;
            Icon = "add_favourite.png";
            Favourited = false;
        }

        public Game(int id, string title, Cover cover, List<Genre> genre, List<Platform> platforms, List<GameMode> gamemodes, double price, string synopsis, string icon = "add_favourite.png", bool favourited = false)
        {
            Id = id;
            Title = title;
            Cover = cover;
            Genre = genre;
            Platforms = platforms;
            GameModes = gamemodes;
            Price = price;
            Synopsis = synopsis;
            Icon = icon;
            Favourited = favourited;
            

        }

        public string GenreDisplay
        {
            get 
            {
                List<string> genreNames = new List<string>();
                foreach (Genre genre in Genre) 
                {
                    genreNames.Add(genre.Name);
                }
                return string.Join(", ", genreNames);

            } 
        }

        public string PlatformDisplay 
        {
            get
            {
                List<string> platformNames = new List<string>();
                foreach (Platform platform in Platforms)
                {
                    platformNames.Add(platform.Name);
                }
                return string.Join(", ", platformNames);

            }
        }

        public string GameModeDisplay
        {
            get
            {
                List<string> gameModeNames = new List<string>();
                foreach (GameMode gamemode in GameModes)
                {
                    gameModeNames.Add(gamemode.Name);
                }
                return string.Join(", ", gameModeNames);

            }
        }
    }
}
