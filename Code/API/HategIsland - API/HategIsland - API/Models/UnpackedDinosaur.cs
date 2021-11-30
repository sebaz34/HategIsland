using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HategIsland___API.Models
{
    /// <summary>
    /// This data structure is used to transfer a dinosaurs information to the front end.
    /// Used externally to the API.
    /// </summary>
    public class UnpackedDinosaur
    {
        public int DinosaurID { get; set; }
        public int PlayerID { get; set; }
        public string Name { get; set; }
        public string AOH { get; set; }
        public string Diet { get; set; }
        public string Size { get; set; }
        public string Species { get; set; }
        public int? Trait1 { get; set; }
        public int? Trait2 { get; set; }
        public int? Trait3 { get; set; }
        public int? Ability1 { get; set; }
        public int? Ability2 { get; set; }
        public int? Ability3 { get; set; }
        public int Health { get; set; }
        public int Stamina { get; set; }
        public int Hunger { get; set; }
        public int Thirst { get; set; }
        public int Level { get; set; }
        public int Status { get; set; }
    }
}
