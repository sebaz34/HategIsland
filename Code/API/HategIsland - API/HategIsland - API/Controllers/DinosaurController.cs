using HategIsland___API.Models;
using HategIsland___API.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        ILogger<DinosaurController> _logger;

        public DinosaurController(HategIslandContext context, ILogger<DinosaurController> logger)
        {
            _context = context;
            _logger = logger;
        }

        //TODO: Apply Authentication to Entire Controller

        /// <summary>
        /// This method is used to create a new dinosaur.
        /// Random values are used to determine the dinosaurs aspects.
        /// </summary>
        /// <param name="PlayerID"></param>
        /// <returns>Unpacked Dinosaur</returns>
        [HttpGet("NewRandom/{PlayerID}")]
        public ActionResult NewRandomDinosaur(int PlayerID)
        {
            try
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

                //Log New Dinosaur Creation
                _logger.LogInformation($"New Dinosaur Created! ID: {newDino.DinosaurID}, PlayerID: {newDino.PlayerID}, Dinosaur Species: {newDino.Species}");

                //Return unpacked dinosaur to FE
                return Ok(newDino);
            }
            catch (Exception e)
            {
                _logger.LogError($"Dinosaur Controller -> NewRandomDinosaur() -> Exception Caught: {e}");
                return StatusCode(500);
            }
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
                if (dbDino != null)
                {
                    return Ok(dp.UnpackDinosaur(dbDino));
                }
                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError($"Dinosaur Controller -> GetDinoFromDB -> Exception caught: {e}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Obtains all dinosaurs that belong to the inputted player.
        /// </summary>
        /// <param name="PlayerID"></param>
        /// <returns>IEnumerable of type UnpackedDinosaur</returns>
        [HttpGet("AllDinos/{PlayerID}")]
        public IEnumerable<UnpackedDinosaur> GetAllPlayersDinos(int PlayerID)
        {
            try
            {
                IEnumerable<PackedDinosaur> packedDinos = _context.Dinosaurs.Where(c => c.PlayerID == PlayerID).ToList();
                List<UnpackedDinosaur> returnList = new List<UnpackedDinosaur>();
                foreach (PackedDinosaur dinosaur in packedDinos)
                {
                    returnList.Append(dp.UnpackDinosaur(dinosaur));
                }

                return returnList;
            }
            catch (Exception e)
            {
                _logger.LogError($"Dinosaur Controller -> GetAllPlayersDinos() -> Exception Caught: {e}");
                throw;
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
            try
            {
                PackedDinosaur dbDino = dp.PackDinosaur(inputDino);
                dbDino.DinosaurSpeciesID = _context.DinosaurSpecies.Where(c => c.Species == inputDino.Species).Select(c => c.DinosaurSpeciesID).FirstOrDefault();

                _context.Entry(dbDino).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
                return Ok(inputDino);
            }
            catch (Exception e)
            {
                _logger.LogError($"Dinosaur Controller -> UpdateDinoValue() -> Exception caught: {e}");
                throw;
            }
        }

        //TODO: Apply Authorisation
        /// <summary>
        /// This method is used to remove a dinosaur from the DB permenatley.
        /// </summary>
        /// <param name="DinoID"></param>
        /// <returns>None</returns>
        [HttpDelete("{DinoID}")]
        public ActionResult DeleteDinosaur(int DinoID)
        {
            try
            {
                PackedDinosaur deletedDino = _context.Dinosaurs.Where(c => c.PackedDinosaurID == DinoID).FirstOrDefault();
                _context.Dinosaurs.Remove(deletedDino);
                _context.SaveChanges();
                _logger.LogInformation($"Dinosaur Deleted: DinosaurID: {deletedDino.PackedDinosaurID}, PlayerID: {deletedDino.PlayerID}, Dinosaur Species: {deletedDino.DinosaurSpecies}");
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"Dinosaur Controller -> DeleteDinosaur() -> Exception Caught: {e}");
                return StatusCode(500);
            }
        }
    }
}
