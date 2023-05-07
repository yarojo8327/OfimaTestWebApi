using Microsoft.EntityFrameworkCore;

using Ofima.TechnicalTest.Infraestructure.Models;

namespace Ofima.TechnicalTest.Infraestructure
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Player> Player { get; set; }

        public DbSet<Game> Game { get; set; }

        public DbSet<Move> Move { get; set; }

        public DbSet<GameMove> GameMove { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
          

            modelBuilder.Entity<Game>()
                .HasOne(x => x.Player)
                .WithMany(x => x.GamesOne)
                .HasForeignKey(x => x.PlayerOneId).OnDelete(DeleteBehavior.NoAction)
                .HasPrincipalKey(x => x.Id);

            modelBuilder.Entity<Game>()
                .HasOne(x => x.PlayerTwo)
                .WithMany(x => x.GamesTwo)
                .HasForeignKey(x => x.PlayerTwoId).OnDelete(DeleteBehavior.NoAction)
                .HasPrincipalKey(x => x.Id);

            modelBuilder.Entity<Game>()
                .HasOne(x => x.PlayerWinner)
                .WithMany(x => x.GamesWinner)
                .HasForeignKey(x => x.WinnerId).OnDelete(DeleteBehavior.NoAction)
                .HasPrincipalKey(x => x.Id);

            modelBuilder.Entity<GameRule>()
               .HasOne(x => x.PlayerOneMove)
               .WithMany(x => x.GameRulesOne)
               .HasForeignKey(x => x.MovePlayerOneId).OnDelete(DeleteBehavior.NoAction)
               .HasPrincipalKey(x => x.Id);

            modelBuilder.Entity<GameRule>()
              .HasOne(x => x.PlayerTwoMove)
              .WithMany(x => x.GameRulesTwo)
              .HasForeignKey(x => x.MovePlayerTwoId).OnDelete(DeleteBehavior.NoAction)
              .HasPrincipalKey(x => x.Id);

            modelBuilder.Seed();

        }
    }
}
