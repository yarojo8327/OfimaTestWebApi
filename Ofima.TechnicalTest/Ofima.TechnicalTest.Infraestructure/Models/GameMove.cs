using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ofima.TechnicalTest.Infraestructure.Models
{
    [Table("GameMoves")]
    public class GameMove
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("GameId")]
        public int GameId { get; set; }

        [Required]
        [ForeignKey("PlayerId")]
        public int PlayerId { get; set; }

        [Required]
        [ForeignKey("MoveId")]
        public int MoveId { get; set; }

        [Required]
        public int RoundNumber { get; set; }

        public virtual Game Game { get; set; }

        public virtual Player Player { get; set; }

        public virtual Move Move { get; set; }


    }
}
