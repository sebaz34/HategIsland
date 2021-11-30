using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HategIsland___API.Models
{
    /// <summary>
    /// Data structure of a player.
    /// Player connected to a user.
    /// Player used for game purposes, user used for authentication
    /// and authorisation purposes.
    /// </summary>
    public class Player
    {
        public int PlayerID { get; set; }
        public int UserID { get; set; }
        public string PlayerName { get; set; }
        public string UnlockedLocations { get; set; }

        //Navigation Properties
        public User User { get; set; }
        public Inventory Inventory { get; set; }
        public ICollection<PackedDinosaur> Dinosaurs { get; set; }
        public ICollection<LocationVisit> LocationVisits { get; set; }
        public ICollection<Battle> Battles { get; set; }
    }
}
