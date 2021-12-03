using HategIsland___API.Models;
using HategIsland___API.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HategIsland___API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        HategIslandContext _context;
        DinoPacker dp = new DinoPacker();
        ILogger<LocationController> _logger;

        public LocationController(HategIslandContext context, ILogger<LocationController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Retrives a single location from the database matching LocationID.
        /// </summary>
        /// <param name="LocationID"></param>
        /// <returns>Location</returns>
        [Authorize]
        [HttpGet("{LocationID}")]
        public ActionResult GetLocationDetails(int LocationID)
        {
            try
            {
                Location returnLocation = _context.Locations.Where(c => c.LocationID == LocationID).FirstOrDefault();
                return Ok(returnLocation);
            }
            catch (Exception e)
            {
                _logger.LogError($"Location Controller -> GetLocationDetails() -> Exception Caught: {e}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Obtains a list of all locations that are avaliable to the inputted player.
        /// </summary>
        /// <param name="PlayerID"></param>
        /// <returns>IEnumerable of type Location</returns>
        [Authorize]
        [HttpGet("AvailableLocations/{PlayerID}")]
        public IEnumerable<Location> GetAvailableLocations(int PlayerID)
        {
            try
            {
                Player currentPlayer = _context.Players.Where(c => c.PlayerID == PlayerID).FirstOrDefault();
                string[] unlockedLocations = currentPlayer.UnlockedLocations.Split("#");

                IEnumerable<Location> returnList = new List<Location>();

                foreach (string location in unlockedLocations)
                {
                    Location thisLocation = _context.Locations.Where(c => c.LocationID == int.Parse(location)).FirstOrDefault();
                    returnList.Append(thisLocation);
                }
                
                return returnList;

            }
            catch (Exception e)
            {
                _logger.LogError($"Location Controller -> GetAvailableLocations() -> Exception Caught: {e}");
                throw;
            }
        }

        /// <summary>
        /// Obtains a list of all locations.
        /// </summary>
        /// <returns>IEnumerable of type Location</returns>
        [Authorize]
        [HttpGet]
        public IEnumerable<Location> GetAllLocations()
        {
            try
            {
                return _context.Locations.ToList();
            }
            catch (Exception e)
            {
                _logger.LogError($"Location Controller -> GetAllLocations() -> Exeception Caught: {e}");
                throw;
            }
        }

        /// <summary>
        /// Adds the specified location to the specifed players list of unlocked locations.
        /// </summary>
        /// <param name="PlayerID"></param>
        /// <param name="LocationID"></param>
        /// <returns>None</returns>
        [Authorize]
        [HttpPost("UnlockLocation/{PlayerID}/{LocationID}")]
        public ActionResult UnlockNewLocation(int PlayerID, int LocationID)
        {
            try
            {
                Player currentPlayer = _context.Players.Where(c => c.PlayerID == PlayerID).FirstOrDefault();

                currentPlayer.UnlockedLocations += $"#{LocationID}";

                _context.Entry(currentPlayer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();

                _logger.LogInformation($"Player: {currentPlayer.PlayerID} has unlocked a new location: {LocationID}.");

                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"Location Controller -> UnlockNewLocation() -> Exception Caught: {e}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Creates a new location visit record with the inputted details.
        /// </summary>
        /// <param name="PlayerID"></param>
        /// <param name="DinosaurID"></param>
        /// <param name="LocationID"></param>
        /// <returns>None</returns>
        [Authorize]
        [HttpPost("Visit/New/{PlayerID}/{DinosaurID}/{LocationID}")]
        public ActionResult NewLocationVisit(int PlayerID, int DinosaurID, int LocationID)
        {
            try
            {
                Location location = _context.Locations.Where(c => c.LocationID == LocationID).FirstOrDefault();

                LocationVisit newVisit = new LocationVisit();

                newVisit.PlayerID = PlayerID;
                newVisit.DinosaurID = DinosaurID;
                newVisit.LocationID = location.LocationID;

                DateTime visitEnd = DateTime.Now.AddMinutes(location.BaseDuration);
                newVisit.EndTime = visitEnd;

                string[] baseRewards = location.BaseReward.Split("#");
                int counter = 2;
                foreach (string element in baseRewards)
                {
                    if (counter%2 == 0)
                    {
                        newVisit.CompletionReward += $"#{element}";
                    }
                    else
                    {
                        Random rand = new Random();
                        int baseReward = int.Parse(element);
                        int modifierValue = baseReward / 2;
                        int rewardValue = rand.Next(1, modifierValue + 1);

                        newVisit.CompletionReward += $"#{rewardValue}";
                    }
                }

                _context.LocationVisits.Add(newVisit);
                _context.SaveChanges();

                _logger.LogInformation($"Player {PlayerID} has sent Dinosaur {DinosaurID} to location {LocationID}");
                
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"Location Controller -> NewLocationVisit() -> Exception Caught: {e}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Obtains a single location visit from DB.
        /// </summary>
        /// <param name="LocationVisitID"></param>
        /// <returns>LocationVisit</returns>
        [Authorize]
        [HttpGet("Visit/{LocationVisitID}")]
        public ActionResult GetSingleLocationVisit(int LocationVisitID)
        {
            try
            {
                return Ok(_context.LocationVisits.Where(c => c.LocationVisitID == LocationVisitID).FirstOrDefault());
            }
            catch (Exception e)
            {
                _logger.LogError($"Location Controller -> GetSingleLocationVisit() -> Exception Caught: {e}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Obtains all location visits with a PlayerID of the inputted player.
        /// </summary>
        /// <param name="PlayerID"></param>
        /// <returns>IEnumerable of type LocationVisit</returns>
        [Authorize]
        [HttpGet("Visit/{PlayerID}")]
        public IEnumerable<LocationVisit> GetAllPlayerLocationVisit(int PlayerID)
        {
            try
            {
                return _context.LocationVisits.Where(c => c.PlayerID == PlayerID).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError($"Location Controller -> GetAllPlayerLocationVisit() -> Exception Caught: {e}");
                throw;
            }
        }

        /// <summary>
        /// Deletes a locationvisit entry from the DB.
        /// </summary>
        /// <param name="LocationVisitID"></param>
        /// <returns>None</returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("Visit/{LocationVisitID}")]
        public ActionResult DeleteLocationVisit(int LocationVisitID)
        {
            try
            {
                LocationVisit deletedVisit = _context.LocationVisits.Where(c => c.LocationVisitID == LocationVisitID).FirstOrDefault();
                _context.LocationVisits.Remove(deletedVisit);
                _context.SaveChanges();
                _logger.LogInformation($"LocationVisit Deleted: LocationVisitID: {deletedVisit.LocationVisitID}, PlayerID: {deletedVisit.PlayerID}, LocationID: {deletedVisit.LocationID}, EndTime: {deletedVisit.EndTime}");
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"Location Controller -> DeleteLocationVisit() -> Exception Caught: {e}");
                return StatusCode(500);
            }
        }
    }
}
