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

        /// <summary>
        /// Used to convert an UnpackedDinosaur into a PackedDinosaur structure.
        /// </summary>
        /// <param name="inputDino"></param>
        /// <returns>PackedDinosaur</returns>
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
            string traits = $"{inputDino.Trait1}#{inputDino.Trait2}#{inputDino.Trait3}";
            returnDino.Traits = traits;

            //Abilites
            string abilities = $"{inputDino.Ability1}#{inputDino.Ability2}#{inputDino.Ability3}";
            returnDino.Abilities = abilities;

            //Stats
            string stats = $"{inputDino.Health}#{inputDino.Stamina}#{inputDino.Hunger}#{inputDino.Thirst}#{inputDino.Level}";
            returnDino.Stats = stats;

            //Status
            returnDino.Status = inputDino.Status;

            return returnDino;
        }
        
        /// <summary>
        /// Used to convert a PackedDinosaur into an UnpackedDinosaur structure.
        /// </summary>
        /// <param name="inputDino"></param>
        /// <returns>UnpackedDinosaur</returns>
        public UnpackedDinosaur UnpackDinosaur(PackedDinosaur inputDino)
        {
            UnpackedDinosaur returnDino = new UnpackedDinosaur();

            //Assigning Values
            //Dinosaur and Player ID's and Dino Name
            returnDino.DinosaurID = inputDino.PackedDinosaurID;
            returnDino.PlayerID = inputDino.PlayerID;
            returnDino.Name = inputDino.Name;

            //Features
            string[] features =  inputDino.Features.Split('#');
            returnDino.AOH = features[0];
            returnDino.Diet = features[1];
            returnDino.Size = features[2];
            returnDino.Species = features[3];

            //Traits
            string[] rawTraits = inputDino.Traits.Split('#');
            returnDino.Trait1 = int.Parse(rawTraits[0]);
            returnDino.Trait2 = int.Parse(rawTraits[1]);
            returnDino.Trait3 = int.Parse(rawTraits[2]);

            //Abilites
            string[] rawAbilites = inputDino.Abilities.Split('#');
            returnDino.Ability1 = int.Parse(rawAbilites[0]);
            returnDino.Ability2 = int.Parse(rawAbilites[1]);
            returnDino.Ability3 = int.Parse(rawAbilites[2]);

            //Stats
            string[] rawStats = inputDino.Stats.Split('#');
            returnDino.Health = int.Parse(rawStats[0]);
            returnDino.Stamina = int.Parse(rawStats[1]);
            returnDino.Hunger = int.Parse(rawStats[2]);
            returnDino.Thirst = int.Parse(rawStats[3]);
            returnDino.Level = int.Parse(rawStats[4]);

            //Status
            returnDino.Status = inputDino.Status;

            return returnDino;
        }
    }
}
