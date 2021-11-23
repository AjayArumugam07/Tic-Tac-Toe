using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
namespace Tic_Tac_Toe.Data
{
    public class Player
    {
        [ForeignKey(nameof(Game))]
        public int PlayerId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [InverseProperty("Player1")]
        public virtual ICollection<Game> Player1Games { get; set; }

        [InverseProperty("Player2")]
        public virtual ICollection<Game> Player2Games { get; set; }

    }
}