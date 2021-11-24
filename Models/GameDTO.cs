using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Tic_Tac_Toe.Data;

namespace Tic_Tac_Toe.Models
{
    public class CreateGameDTO
    {

        // -1: Incomplete
        //  0: Draw
        //  1: Player 1 Won
        //  2: Player 2 Won
        [Range(-1, 2)]
        public int Status { get; set; }

        public int Player1Id { get; set; }

        public int Player2Id { get; set; }

        public int NumberOfMoves { get; set; }

        public CreateGameDTO()
        {
            Status = -1;
            NumberOfMoves = 0;
        }
    }

    public class GameDTO : CreateGameDTO
    {
        public int GameId { get; set; }

        public List<Move> Moves { get; set; }

        public Player Player1 { get; set; }

        public Player Player2 { get; set; }
    }

    public class ActiveGamesDTO
    {
        public int GameId { get; set; }

        public int Status { get; set; }

        public int NumberOfMoves { get; set; }

        public string Player1Name { get; set; }

        public string Player2Name { get; set; }
    }
}
