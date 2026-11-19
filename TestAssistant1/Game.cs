using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace TestAssistant1
{
    // Represents a video game
    public class Game : ObservableObject
    {
        private bool _favourited = false;

        public int Id { get; set; }

        
        public string Title { get; set; } 

      
        public Cover? Cover { get; set; }

        
        public List<Genre> Genre { get; set; } = []; // List of genre objects.

     
        public List<Platform> Platforms { get; set; } = []; // List of platform objects.

       
        public List<GameMode> GameModes { get; set; } = []; // List of gamemode objects.

        public double Price { get; set; }

       
        public string Synopsis { get; set; }
        public bool Favourited 
        {
            // get backing field.
            get => _favourited;
            set 
            {
                // value is the passed in value.
                //setproperty checks if _favourited = value, returns bool.
                // ref passes in value of backing field.
               if (SetProperty(ref _favourited, value)) // if same.
                {
                    // call getter to update value of icon
                    OnPropertyChanged(nameof(Icon));
                }
            } 

        }

        // if favourited = true.
        // then return favourited.png
        // else return add_favourite.png
        public string Icon => _favourited ? "favourited.png" : "add_favourite.png";

      
        // 
        public string CoverImageUrl =>
            // check if there is no cover url.
            string.IsNullOrWhiteSpace(Cover?.Url)
            // if true set to placeholder image.
                ? "cover_placeholder.png"
            // else set to the url return from server
                : Cover.ImageUrl;




        public Game()
        {
            Id = 0;
            Title = "Not found";
            Cover = new Cover();
            Genre = [];
            Platforms = [];
            GameModes = [];
            Price = 0;
            Synopsis = "Not found";
            Favourited = false;
        }

        public Game(int id, string title, Cover cover, List<Genre> genre, List<Platform> platforms, List<GameMode> gamemodes, double price, string synopsis, bool favourited = false)
        {
            Id = id;
            Title = title;
            Cover = cover;
            Genre = genre;
            Platforms = platforms;
            GameModes = gamemodes;
            Price = price;
            Synopsis = synopsis;
            Favourited = favourited;
            

        }

        /// <summary>
        /// API returns genre objects.
        /// Would display an object reference.
        /// To display properly in UI loops through genre objects, grabs names and appends to human readable list.
        /// </summary>
        public string GenreDisplay
        {
            get 
            {
                // if server returns no genres, set to not found.
                if (Genre == null || Genre.Count == 0)
                {
                    return "Not found";
                }
                // else loops through and adds
                List<string> genreNames = new List<string>();
                foreach (Genre genre in Genre) 
                {
                    genreNames.Add(genre.Name);
                }
                // string of comma seperated genreNames.
                return string.Join(", ", genreNames);

            } 
        }

        /// <summary>
        /// API returns platform objects. 
        /// Would display an object reference.
        /// To display properly in UI loops through platform objects, grabs names and appends to human readable list.
        /// </summary>
        public string PlatformDisplay 
        {
            get
            {
                // if server returns no platforms, set to not found.
                if (Platforms == null || Platforms.Count == 0)
                {
                    return "Not found";
                }
                // else loops through and adds
                List<string> platformNames = new List<string>();
                foreach (Platform platform in Platforms)
                {
                    platformNames.Add(platform.Name);
                }
                // string of comma seperated platformNames.
                return string.Join(", ", platformNames);

            }
        }

        /// <summary>
        /// API returns gamemode objects. 
        /// Would display an object reference.
        /// To display properly in UI loops through gamemode objects, grabs names and appends to human readable list.
        /// </summary>
        public string GameModeDisplay
        {
            get
            {
                // if server returns no gamemode, set to not found.
                if (GameModes == null || GameModes.Count == 0)
                {
                    return "Not found";
                }
                // else loops through and adds
                List<string> gameModeNames = new List<string>();
                foreach (GameMode gamemode in GameModes)
                {
                    gameModeNames.Add(gamemode.Name);
                }
                // string of comma seperated gameModeNames.
                return string.Join(", ", gameModeNames);

            }
        }

        }
}
