using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HategIsland___API.Models
{
    public class HategIslandContext : DbContext
    {
        public HategIslandContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<PackedDinosaur> Dinosaurs { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<LocationVisit> LocationVisits { get; set; }
        public DbSet<Battle> Battles { get; set; }
    }
}
