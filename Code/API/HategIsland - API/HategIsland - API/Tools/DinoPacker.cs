using HategIsland___API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HategIsland___API.Tools
{
    /// <summary>
    /// Used to transfer data between dinosaur structures like:
    /// Packed and Unpacked Dinosaur.
    /// </summary>
    public class DinoPacker
    {
        public DinoPacker()
        {

        }

        public PackedDinosaur PackDinosaur(UnpackedDinosaur inputDino)
        {
            PackedDinosaur returnDino = new PackedDinosaur();

            //Assigning Values
            //Dinosaur and Player ID's and Dino Name
            returnDino.PackedDinosaurID = inputDino.DinosaurID;
            returnDino.PlayerID = inputDino.PlayerID;
            returnDino.Name = inputDino.Name;

            //Features
            string features = $"{inputDino.AOH}#{inputDino.Diet}#{inputDino.Size}#{inputDino.Species}";
            returnDino.Features = features;

            //Traits
            string traits = $""
        }
    }
}
