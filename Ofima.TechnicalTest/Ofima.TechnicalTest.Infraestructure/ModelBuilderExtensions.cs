using Microsoft.EntityFrameworkCore;

using Ofima.TechnicalTest.Infraestructure.Models;

namespace Ofima.TechnicalTest.Infraestructure
{
    internal static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Move>().HasData(
               new Move
               {
                   Id = 1,
                   MoveName = "Piedra",
               },
               new Move
               {
                   Id = 2,
                   MoveName = "Papel",
               },
               new Move
               {
                   Id = 3,
                   MoveName = "Tijera",
               }
           );

            modelBuilder.Entity<GameRule>().HasData(
                new GameRule { Id = 1, MovePlayerOneId = 1, MovePlayerTwoId = 3, Winner = "PlayerOne", Tie = false },
                new GameRule { Id = 2, MovePlayerOneId = 1, MovePlayerTwoId = 2, Winner = "PlayerOne", Tie = false },
                new GameRule { Id = 3, MovePlayerOneId = 2, MovePlayerTwoId = 1, Winner = "PlayerOne", Tie = false },
                new GameRule { Id = 4, MovePlayerOneId = 2, MovePlayerTwoId = 3, Winner = "PlayerTwo", Tie = false },
                new GameRule { Id = 5, MovePlayerOneId = 3, MovePlayerTwoId = 1, Winner = "PlayerTwo", Tie = false },
                new GameRule { Id = 6, MovePlayerOneId = 3, MovePlayerTwoId = 2, Winner = "PlayerTwo", Tie = false },
                new GameRule { Id = 7, MovePlayerOneId = 1, MovePlayerTwoId = 1, Winner = "Tie", Tie = true },
                new GameRule { Id = 8, MovePlayerOneId = 2, MovePlayerTwoId = 2, Winner = "Tie", Tie = true },
                new GameRule { Id = 9, MovePlayerOneId = 3, MovePlayerTwoId = 3, Winner = "Tie", Tie = true }
            );
        }
    }
}
