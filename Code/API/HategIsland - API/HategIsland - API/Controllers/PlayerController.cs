using HategIsland___API.Models;
using HategIsland___API.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace HategIsland___API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        HategIslandContext _context;
        DinoPacker dp = new DinoPacker();
        ILogger<PlayerController> _logger;

        public PlayerController(HategIslandContext context, ILogger<PlayerController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new Player entry and new Inventory in the DB.
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="PlayerName"></param>
        /// <returns>Player</returns>
        [Authorize]
        [HttpPost("New/{UserID}/{PlayerName}")]
        public ActionResult AddNewPlayer(int UserID, string PlayerName)
        {
            try
            {
                Player newPlayer = new Player();

                newPlayer.UserID = UserID;
                newPlayer.PlayerName = PlayerName;

                newPlayer.UnlockedLocations = "1#2#3";

                _context.Players.Add(newPlayer);
                _context.SaveChanges();

                Inventory newInventory = new Inventory();

                newInventory.PlayerID = newPlayer.PlayerID;
                newInventory.Money = 200;
                newInventory.HerbFood = 100;
                newInventory.CarnFood = 100;
                newInventory.Medicine = 100;
                newInventory.Herb = 50;

                _context.Inventories.Add(newInventory);
                _context.SaveChanges();

                _logger.LogInformation($"New Player Created! PlayerID: {newPlayer.PlayerID}, PlayerName: {newPlayer.PlayerName} InventoryID: {newInventory.InventoryID}");

                return Ok(newPlayer);
            }
            catch (Exception e)
            {
                _logger.LogError($"Player Controller -> AddNewPlayer() -> Exeception Caught: {e}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Updates a players details in the DB.
        /// </summary>
        /// <param name="inputPlayer"></param>
        /// <returns>Player</returns>
        [Authorize]
        [HttpPost("{id}")]
        public ActionResult UpdatePlayerDetails([FromBody] Player inputPlayer)
        {
            try
            {
                _context.Entry(inputPlayer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
                return Ok(inputPlayer);
            }
            catch (Exception e)
            {
                _logger.LogError($"Player Controller -> UpdatePlayerDetails() -> Exception Caught: {e}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Obtains a players details from the DB.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Player</returns>
        [Authorize]
        [HttpGet("{id}")]
        public ActionResult GetPlayerDetails(int id)
        {
            try
            {
                return Ok(_context.Players.Where(c => c.PlayerID == id).FirstOrDefault());
            }
            catch (Exception e)
            {
                _logger.LogError($"Player Controller -> GetPlayerDetails() -> Exception Caught: {e}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Updates an Inventories details in the DB as per the inputted inventory.
        /// </summary>
        /// <param name="inputInventory"></param>
        /// <returns>Inventory</returns>
        [Authorize]
        [HttpPost("Inventory/{id}")]
        public ActionResult UpdateInventoryDetails([FromBody] Inventory inputInventory)
        {
            try
            {
                _context.Entry(inputInventory).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
                return Ok(inputInventory);
            }
            catch (Exception e)
            {
                _logger.LogError($"Player Controller -> UpdateInventoryDetails() -> Exception Caught: {e}");
                return StatusCode(500);
            }

        }

        /// <summary>
        /// Obtains an Inventory from the DB that belongs to inputted player.
        /// </summary>
        /// <param name="PlayerID"></param>
        /// <returns>Inventory</returns>
        [Authorize]
        [HttpGet("Inventory/{PlayerID}")]
        public ActionResult GetInventoryDetails(int PlayerID)
        {
            try
            {
                return Ok(_context.Inventories.Where(c => c.PlayerID == PlayerID).FirstOrDefault());
            }
            catch (Exception e)
            {
                _logger.LogError($"Player Controller -> GetInventoryDetails() -> Exception Caught: {e}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Removes a player and inventory from the DB that belong to inputted PlayerID.
        /// </summary>
        /// <param name="PlayerID"></param>
        /// <returns>None</returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{PlayerID}")]
        public ActionResult DeletePlayer(int PlayerID)
        {
            try
            {
                Inventory deletedInventory = _context.Inventories.Where(c => c.PlayerID == PlayerID).FirstOrDefault();
                Player deletedPlayer = _context.Players.Where(c => c.PlayerID == PlayerID).FirstOrDefault();

                _context.Inventories.Remove(deletedInventory);
                _context.Players.Remove(deletedPlayer);
                _context.SaveChanges();

                _logger.LogInformation($"Player and Inventory Deleted: PlayerID: {deletedPlayer.PlayerID}, InventoryID: {deletedInventory.InventoryID}");

                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"Player Controller -> DeletePlayer() -> Exception Caught: {e}");
                return StatusCode(500);
            }

        }
    }
}
