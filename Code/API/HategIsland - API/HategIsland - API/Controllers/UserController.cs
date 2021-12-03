using HategIsland___API.Models;
using HategIsland___API.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace HategIsland___API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        HategIslandContext _context;
        DinoPacker dp = new DinoPacker();
        ILogger<UserController> _logger;
        IConfiguration _configuration;

        public UserController(HategIslandContext context, ILogger<UserController> logger, IConfiguration config)
        {
            _context = context;
            _logger = logger;
            _configuration = config;
        }

        /// <summary>
        /// This method is used to create a new user in the DB with the inputted
        /// username and password. Passwords are hashed.
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        /// <returns>User</returns>
        [HttpPost("New/{Username}/{Password}")]
        public ActionResult CreateNewUser(string Username, string Password)
        {
            try
            {
                if (_context.Users.Where(c => c.Username == Username).FirstOrDefault() == null)
                {
                    User newUser = new User();

                    newUser.Username = Username;
                    newUser.Password = BCrypt.Net.BCrypt.HashPassword(Password);
                    newUser.Roles = "User";

                    _context.Users.Add(newUser);
                    _context.SaveChanges();

                    _logger.LogInformation($"New User Created! UserID: {newUser.UserID}, Username: {newUser.Username}");

                    return Ok(newUser);
                }
                else
                {
                    return BadRequest("Username is already taken.");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"User Controller -> CreateNewUser() -> Exception Caught: {e}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Removes a User from the DB that corresponds to inputted UserID.
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns>None</returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{UserID}")]
        public ActionResult DeleteActiveUser(int UserID)
        {
            try
            {
                User deletedUser = _context.Users.Where(c => c.UserID == UserID).FirstOrDefault();

                _context.Users.Remove(deletedUser);
                _context.SaveChanges();

                _logger.LogInformation($"User Deleted: UserID: {deletedUser.UserID}, Username: {deletedUser.Username}");

                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"User Controller -> DeleteActiveUser() -> Exception Caught: {e}");
                return StatusCode(500);
            }

        }

        #region Token Creation and Authentication
        [HttpPost]
        [Route("Login")]
        public IActionResult GenerateToken(User _userData)
        {
            // All of the null checks
            if (_userData != null && _userData.Username != null && _userData.Password != null)
            {
                // retrieve the user for these credentials
                var user = GetUser(_userData.Username, _userData.Password);
                // If we have a user that matches the credentials
                if (user != null)
                {
                    //create claims details based on the user information
                    var claims = new List<Claim> {
                    // JWT Subject
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    // JWT ID
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    // JWT Date/Time
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    // JWT User ID
                    new Claim("ID", user.UserID.ToString()),
                    // JWT UserName
                    new Claim("Username", user.Username),
                   };

                    if (user.Roles != null)
                    {
                        foreach (var role in user.Roles.Split(','))
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role));
                        }
                    }


                    // Generate a new key based on the Key we created and stored in appsettings.json
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    // use the generated key to generate new Signing Credentials
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    // Generate a new token based on all of the details generated so far
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims: claims,
                        // How long the JWT will be valid for
                        expires: DateTime.UtcNow.AddMinutes(30),
                        signingCredentials: signIn);
                    // Return the Token via JSON
                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        private User GetUser(string userName, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == userName);

            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return user;
            }
            return null;
        }

        #endregion
    }
}
