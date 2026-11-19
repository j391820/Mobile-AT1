using System;
using System.Collections.Generic;
using System.Text;

namespace TestAssistant1
{
    // Represents a game platform such as pc, snes. 
    public class Platform
    {
      
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
