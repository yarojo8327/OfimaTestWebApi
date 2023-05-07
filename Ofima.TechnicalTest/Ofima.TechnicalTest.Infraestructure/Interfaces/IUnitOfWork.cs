using Ofima.TechnicalTest.Infraestructure.Models;

namespace Ofima.TechnicalTest.Infraestructure.Interfaces
{
    public interface IUnitOfWork
    {
        Repository<Player> Player { get; }
        Repository<Game> Game { get; }
        Repository<GameMove> GameMove { get; }
        Repository<GameRule> GameRule { get; }
        Repository<Move> Move { get; }

        void Dispose();
        int Save();
        void SaveAsync();
    }
}