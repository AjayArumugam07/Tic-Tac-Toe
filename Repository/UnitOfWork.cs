using System;
using System.Threading.Tasks;
using Tic_Tac_Toe.Data;
using Tic_Tac_Toe.IRepository;

namespace Tic_Tac_Toe.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GamesDBContext _context;

        private IGenericRepository<Game> _games;
        private IGenericRepository<Move> _moves;
        private IGenericRepository<Player> _players;

        public UnitOfWork(GamesDBContext context)
        {
            _context = context;
        }

        public IGenericRepository<Game> Games => _games ??= new GenericRepository<Game>(_context);
        public IGenericRepository<Move> Moves => _moves ??= new GenericRepository<Move>(_context);
        public IGenericRepository<Player> Players => _players ??= new GenericRepository<Player>(_context);

        public void Dispose()
        {
            _context.DisposeAsync();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
