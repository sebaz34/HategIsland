using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HategIsland___API.Models
{
    /// <summary>
    /// Data structure used to hold template of dinosaur features and stats.
    /// Catagorised by dino species.
    /// </summary>
    public class DinosaurSpecies
    {
        public int DinosaurSpeciesID { get; set; }
        public string Species { get; set; }
        public string AOH { get; set; }
        public string Diet { get; set; }
        public string Size { get; set; }
        public int BaseHealth { get; set; }
        public int BaseStamina { get; set; }
        public int BaseHunger { get; set; }
        public int BaseThirst { get; set; }
        public string HistoricalBlurb { get; set; }

        //Navigation Properties
        public ICollection<PackedDinosaur> Dinosaurs { get; set; }
    }
}
