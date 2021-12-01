using HategIsland___API.Models;
using HategIsland___API.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HategIsland___API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DinosaurController : ControllerBase
    {
        HategIslandContext _context;
        DinoPacker dp = new DinoPacker();
        public DinosaurController(HategIslandContext context)
        {
            _context = context;
        }

        /// <summary>
        /// This method is used to create a new dinosaur.
        /// Random values are used to determine the dinosaurs aspects.
        /// </summary>
        /// <param name="PlayerID"></param>
        /// <returns>Unpacked Dinosaur</returns>
        [HttpGet("NewRandom/{PlayerID}")]
        public ActionResult NewRandomDinosaur(int PlayerID)
        {
            //Instantiate new Unpacked Dinosaur
            UnpackedDinosaur newDino = new UnpackedDinosaur();

            //Assign New dinosaur to playerID
            newDino.PlayerID = PlayerID;

            //Instantiate random class
            Random rand = new Random();

            //Randomly determine Features based on species
            int species = rand.Next(2, 42);
            DinosaurSpecies randSpecies = _context.DinosaurSpecies.Find(species);
            newDino.Name = $"Baby {randSpecies.Species}";
            newDino.AOH = randSpecies.AOH;
            newDino.Diet = randSpecies.Diet;
            newDino.Size = randSpecies.Size;
            newDino.Species = randSpecies.Species;

            //Randomly modify stats within set ranges
            int statModifyer = rand.Next(-10, 11);
            //Health
            newDino.Health = randSpecies.BaseHealth + statModifyer;
            //Stamina
            newDino.Stamina = randSpecies.BaseStamina + statModifyer;
            //Hunger
            newDino.Hunger = randSpecies.BaseHunger;
            //Thirst
            newDino.Thirst = randSpecies.BaseThirst;


            //Randomly determine Trait 1
            int randTrait = rand.Next(2, 49);
            newDino.Trait1 = randTrait;

            //Randomly determine Ability 1
            int randAbility = rand.Next(2, 19);
            newDino.Ability1 = randAbility;

            //Assign level of 1
            newDino.Level = 1;

            //Set status to 0 (available)
            newDino.Status = 0;

            //Convert dinosaur to packed dinosaur
            //and place in DB
            PackedDinosaur DBDino = dp.PackDinosaur(newDino);
            DBDino.DinosaurSpeciesID = randSpecies.DinosaurSpeciesID;
            _context.Dinosaurs.Add(DBDino);
            _context.SaveChanges();

            //Return unpacked dinosaur to FE
            return Ok(newDino);
        }

        /// <summary>
        /// This method is used to obtain a single dinosaur from the DB.
        /// </summary>
        /// <param name="DinoID"></param>
        /// <returns>Unpacked Dinosaur</returns>
        [HttpGet("{DinoID}")]
        public ActionResult GetDinoFromDB(int DinoID)
        {
            try
            {
                PackedDinosaur dbDino = _context.Dinosaurs.Find(DinoID);
                return Ok(dp.UnpackDinosaur(dbDino));
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// This method is used to update a dinosaurs aspects in the DB.
        /// </summary>
        /// <param name="inputDino"></param>
        /// <returns>Unpacked Dinosaur</returns>
        [HttpPost("UpdateDino/{id}")]
        public ActionResult UpdateDinoValue([FromBody] UnpackedDinosaur inputDino)
        {
            PackedDinosaur dbDino = dp.PackDinosaur(inputDino);
            dbDino.DinosaurSpeciesID = _context.DinosaurSpecies.Where(c => c.Species == inputDino.Species).Select(c => c.DinosaurSpeciesID).FirstOrDefault();

            _context.Entry(dbDino).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return Ok(inputDino);
        }

        //Delete
    }
}
