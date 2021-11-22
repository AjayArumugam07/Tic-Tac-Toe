using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicTacToe.Data
{
    public class Game {
        public int GameId { get; set; }
        public int NumberOfMoves { get; set; }
        public bool Status { get; set; }
        
        [ForeignKey(nameof(Player))]
        public int Player1Id { get; set; }
        public Player Player1 { get; set; }

        [ForeignKey(nameof(Player))]
        public int Player2Id { get; set; }
        public Player Player2 { get; set; }
    }
}