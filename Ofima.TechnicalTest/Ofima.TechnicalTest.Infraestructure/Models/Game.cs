using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ofima.TechnicalTest.Infraestructure.Models
{
    [Table("Games")]
    public class Game
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime DatePlayed { get; set; } = DateTime.Now;
        
        public int PlayerOneId { get; set; }
       
        public int PlayerTwoId { get; set; }

        public int? WinnerId { get; set; }

        public virtual Player Player { get; set; }

        public virtual Player PlayerTwo { get; set; }

        public virtual Player PlayerWinner { get; set; }

        public virtual ICollection<GameMove> GameMoves { get; set;}
    }
}
