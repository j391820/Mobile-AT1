using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileAT1.Models
{
    public class Platform
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        public Platform()
        {
            Name = string.Empty;
        }

        public Platform(string name)
        {
            Name = name;
        }
    }
}
