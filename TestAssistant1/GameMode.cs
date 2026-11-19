using System;
using System.Collections.Generic;
using System.Text;

namespace TestAssistant1
{
    // Represents different gamemodes such as single player, multi-player.
    public class GameMode
    {
       
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
