using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ofima.TechnicalTest.Infraestructure.Models
{
    [Table("Players")]
    public class Player
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Names { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public virtual ICollection<Game> GamesOne { get; set; }

        public virtual ICollection<Game> GamesTwo { get; set; }

        public virtual ICollection<Game> GamesWinner { get; set; }

        public virtual ICollection<GameMove> GameMoves { get; set; }
    }
}
