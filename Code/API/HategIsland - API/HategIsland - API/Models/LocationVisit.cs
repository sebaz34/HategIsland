using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HategIsland___API.Models
{
    /// <summary>
    /// Data structure of a LocationVisit.
    /// Utilised with Location, where location is the 
    /// destination, and LocationVisit is an individual visit
    /// to a location.
    /// </summary>
    public class LocationVisit
    {
        public int LocationVisitID { get; set; }
        public int PlayerID { get; set; }
        public int DinosaurID { get; set; }
        public int LocationID { get; set; }
        public DateTime EndTime { get; set; }
        public string CompletionReward { get; set; }

        //Navigation Properties
        public Player Player { get; set; }
        public Location Location { get; set; }
    }
}
