using System;
using System.Collections.Generic;
using System.Text;

namespace TestAssistant1
{
    // Represents the id of a game on the favourites list.
    // Helps the sqlite database just store a bunch of ids.
    // Storing a whole game object that is built out of multiple components (cover, list<genre>) proved to be difficult.
    // See Favourites.cs for how this is handled.
    // Admittedly a hacky fix.
    public class FavouriteID
    {
        
        public int GameId { get; set; }
    }
}
