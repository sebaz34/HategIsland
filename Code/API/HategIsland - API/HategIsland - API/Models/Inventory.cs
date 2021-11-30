using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HategIsland___API.Models
{
    /// <summary>
    /// Data structure of a players inventory.
    /// </summary>
    public class Inventory
    {
        public int InventoryID { get; set; }
        public int PlayerID { get; set; }
        public int Money { get; set; }
        public int HerbFood { get; set; }
        public int CarnFood { get; set; }
        public int Medicine { get; set; }
        public int Herb { get; set; }

        //Navigation Properties
        public Player Player { get; set; }
    }
}
