using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ofima.TechnicalTest.Infraestructure.Models
{
    [Table("GameRules")]
    public class GameRule
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int MovePlayerOneId { get; set; }

        [Required]
        public int MovePlayerTwoId { get; set; }

        [Required]
        [StringLength(15)]
        public string Winner { get; set; }

        [Required]
        public bool Tie { get; set; }

        public virtual Move PlayerOneMove { get; set; }

        public virtual Move PlayerTwoMove { get; set; }
    }
}
