using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileAT1.Models
{
    public class GameMode
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        public GameMode()
        {
            Name = string.Empty;
        }

        public GameMode(string name)
        {
            Name = name;
        }
    }
}
