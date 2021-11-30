using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HategIsland___API.Models
{
    /// <summary>
    /// Data structure of a user.
    /// User connected to player.
    /// Player used for game purposes, user used for authentication
    /// and authorisation purposes.
    /// </summary>
    public class User
    {
        public int UserID { get; set; }
        //Username is a hashed value
        public string Username { get; set; }
        //Password is a hashed value
        public string Password { get; set; }
        //Email is an encrypted value
        public string Email { get; set; }

        //Navigation Properties
        public Player Player { get; set; }
    }
}
