using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HategIsland___API.Models
{
    /// <summary>
    /// Data structure of a battle.
    /// </summary>
    public class Battle
    {
        public int BattleID { get; set; }
        public int InitiatingPlayerID { get; set; }
        public int ReceivingPlayerID { get; set; }
        public int CurrentPlayerTurn { get; set; }
        public string InitiatingPlayerPack { get; set; }
        public string ReceivingPlayerPack { get; set; }
        public int? Winner { get; set; }

        //Navigation Properties
        public Player Player { get; set; }
    }
}
