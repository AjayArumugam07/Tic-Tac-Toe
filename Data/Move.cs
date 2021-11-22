using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
namespace TicTacToe.Data
{
    public class Move
    {
        public int MoveId { get; set; }

        [Required]
        public int XCoordinate { get; set; }

        [Required]
        public int YCoordinate { get; set; }

        [ForeignKey(nameof(Player))]
        public int PlayerId { get; set; }
        public Player Player { get; set; }
    }
}