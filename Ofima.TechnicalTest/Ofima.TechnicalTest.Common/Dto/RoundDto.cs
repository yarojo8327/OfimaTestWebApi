namespace Ofima.TechnicalTest.Common.Dto
{
    public class RoundDto
    {
        public int GameId { get; set; }

        public int PlayerOneId { get; set; }

        public string PlayerOne { get; set; }

        public int PlayerTwoId { get; set; }

        public string PlayerTwo { get; set; }

        public string MovePlayerOne { get; set; }

        public string MovePlayerTwo { get; set; }

        public int? WinnerId { get; set; }

        public string? Winner { get; set; }
    }
}
