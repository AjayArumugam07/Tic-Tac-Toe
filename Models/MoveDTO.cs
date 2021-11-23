using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Tic_Tac_Toe.Data;


namespace Tic_Tac_Toe.Models
{
    public class CreateMoveDTO
    {
        [Required]
        public int XCoordinate { get; set; }

        [Required]
        public int YCoordinate { get; set; }

        public int PlayerId { get; set; }

        public int GameId { get; set; }
    }

    public class MoveDTO : CreateMoveDTO
    {
        public int MoveId { get; set; }

        public Player Player { get; set; }

        public Game Game { get; set; }
    }
}
