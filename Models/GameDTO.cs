using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Tic_Tac_Toe.Data;

namespace Tic_Tac_Toe.Models
{
    public class CreateGameDTO
    {
        [Required]
        public int NumberOfMoves { get; set; }

        // -1: Incomplete, 0: Draw, 1: Player 1 Won, 2: Player 2 Won
        [Required]
        public int Status { get; set; }

        public int? Player1Id { get; set; }

        public int? Player2Id { get; set; }
    }

    public class GameDTO : CreateGameDTO
    {
        public int GameId { get; set; }

        public List<Move> Moves { get; set; }

        public Player Player1 { get; set; }

        public Player Player2 { get; set; }
    }
}
