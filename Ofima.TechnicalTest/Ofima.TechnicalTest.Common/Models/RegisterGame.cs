namespace Ofima.TechnicalTest.Common.Models
{
    public class RegisterGame
    {
        public int Id { get; set; }

        public int PlayerOneId { get; set; }

        public int PlayerTwoId { get; set; }

        public int? WinnerId { get; set; } = null;
    }
}
