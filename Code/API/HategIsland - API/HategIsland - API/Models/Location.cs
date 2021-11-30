using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HategIsland___API.Models
{
    /// <summary>
    /// Data structure of a location.
    /// Utilised with LocationVisit, where Location is the
    /// destination, and LocationVisit is an indvidual visit
    /// to a location.
    /// </summary>
    public class Location
    {
        public int LocationID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int BaseDuration { get; set; }

        /* Locations can offer multiple rewards,
         * Rewards will always be formatted as such:
         * "amount, reward type" */
        public string BaseReward { get; set; }

        //Navigation Properties
        public ICollection<LocationVisit> LocationVisits { get; set; }
    }
}
