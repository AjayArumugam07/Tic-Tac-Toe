using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Tic_Tac_Toe.Data
{
    public class Game
    {
        public int GameId { get; set; }

        [Required]
        public int NumberOfMoves { get; set; }

        // -1: Incomplete
        //  0: Draw
        //  1: Player 1 Won
        //  2: Player 2 Won
        [Required]
        public int Status { get; set; } 

        public List<Move> Moves { get; set; }

        [ForeignKey(nameof(Player))]
        public int? Player1Id { get; set; }

        [ForeignKey(nameof(Player))]
        public int? Player2Id { get; set; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }

    }
}