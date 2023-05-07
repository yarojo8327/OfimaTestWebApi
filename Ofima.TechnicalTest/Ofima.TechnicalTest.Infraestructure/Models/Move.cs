using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ofima.TechnicalTest.Infraestructure.Models
{
    [Table("Moves")]
    public class Move
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string MoveName { get; set; }

        public virtual ICollection<GameMove> GameMoves { get; set; }

        public virtual ICollection<GameRule> GameRulesTwo { get; set; }

        public virtual ICollection<GameRule> GameRulesOne{ get; set; }
    }
}
