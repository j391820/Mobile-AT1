using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileAT1.Models
{
    // Represents a game genre such as shooter, rpg.
    public class Genre
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        public Genre()
        {
            Name = string.Empty;
        }

        public Genre(string name)
        {
            Name = name;
        }
    }
}
