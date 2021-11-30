using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HategIsland___API.Models
{
    /// <summary>
    /// This data structure is used within the database.
    /// The API is responsible for handling the data and 
    /// transforming it to the required data set depending on the destination.
    /// Not to be transferred to the front end.
    /// </summary>
    public class PackedDinosaur
    {
        public int PackedDinosaurID { get; set; }
        public int PlayerID { get; set; }
        public string Name { get; set; }
        /*Features are the minified values of:
         * AOH, Diet, Size, Species */
        public string Features { get; set; }

        /*Traits are the minifed values of:
         * Trait1, Trait2, Trait3 */
        public string Traits { get; set; }

        /*Abilities are the minifed values of:
         * Ability1, Ability2, Ability3 */
        public string Abilities { get; set; }

        /*Stats are the minified values of:
         * Health, Stamina, Hunger, Thirst, Level */
        public string Stats { get; set; }
        public int Status { get; set; }

        //Navigation Properties
        public Player Player { get; set; }
    }
}
