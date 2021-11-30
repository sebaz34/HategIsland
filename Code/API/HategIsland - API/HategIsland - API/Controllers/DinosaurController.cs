using HategIsland___API.Models;
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
        public DinosaurController(HategIslandContext context)
        {
            _context = context;
        }

        //Create(Random)

        //Create(Specified Values)

        //Get

        //Update(Post)

        //Delete
    }
}
