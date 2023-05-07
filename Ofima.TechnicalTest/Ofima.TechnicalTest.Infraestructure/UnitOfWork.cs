using Ofima.TechnicalTest.Infraestructure.Interfaces;
using Ofima.TechnicalTest.Infraestructure.Models;

namespace Ofima.TechnicalTest.Infraestructure
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private DataBaseContext _context;

        public UnitOfWork(DataBaseContext context)
        {
            this._context = context;
        }

        private Repository<Player> _player;
        private Repository<Game> _game;
        private Repository<GameMove> _gameMove;
        private Repository<GameRule> _gameRule;
        private Repository<Move> _move;

        public Repository<Player> Player => _player ??= new Repository<Player>(_context);
        public Repository<Game> Game => _game ??= new Repository<Game>(_context);
        public Repository<GameMove> GameMove => _gameMove ??= new Repository<GameMove>(_context);
        public Repository<GameRule> GameRule => _gameRule ??= new Repository<GameRule>(_context);
        public Repository<Move> Move => _move ??= new Repository<Move>(_context);

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void SaveAsync()
        {
            _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;
    }
}
