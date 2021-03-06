using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Tic_Tac_Toe.Data;

namespace Tic_Tac_Toe.Models
{
    public class CreatePlayerDTO
    {
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Name must be less than 50 characters")]
        public string Name { get; set; }
    }

    public class PlayerDTO : CreatePlayerDTO
    {
        public int PlayerId { get; set; }

        public virtual ICollection<Game> Player1Games { get; set; }

        public virtual ICollection<Game> Player2Games { get; set; }
    }

    public class CreatePlayersDTO
    {
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Name must be less than 50 characters")]
        public string Player1Name { get; set; }
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Name must be less than 50 characters")]
        public string Player2Name { get; set; }
    }
}
