using HategIsland___API.Models;
using HategIsland___API.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IO;

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

        //New User

        //Authenticate User

        //Delete User

        //Token Creation
        [HttpPost]
        [Route("GenerateToken")]
        public IActionResult GenerateToken(User _userData)
        {
            // All of the null checks
            if (_userData != null && _userData.UserName != null && _userData.Password != null)
            {
                // retrieve the user for these credentials
                var user = GetUser(_userData.UserName, _userData.Password);
                // If we have a user that matches the credentials
                if (user != null)
                {
                    //create claims details based on the user information
                    // TODO Converted Array of claims to a list
                    var claims = new List<Claim> {
                    // JWT Subject
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    // JWT ID
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    // JWT Date/Time
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    // JWT User ID
                    new Claim("Id", user.UserID.ToString()),
                    // JWT UserName
                    new Claim("Username", user.Username),
                   };

                    // TODO Adding the Roles to the token
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
                        expires: DateTime.UtcNow.AddMinutes(5),
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
            var user = _context.UserInfos.FirstOrDefault(u => u.UserName == userName);
            // TODO Added hashined password check
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return user;
            }
            return null;
        }
    }
}
