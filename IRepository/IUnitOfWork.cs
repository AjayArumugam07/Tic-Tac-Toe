using System;
using System.Threading.Tasks;
using Tic_Tac_Toe.Data;

namespace Tic_Tac_Toe.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Game> Games { get; }
        IGenericRepository<Move> Moves { get; }
        IGenericRepository<Player> Players { get; }

        // Saves the current state of Database
        Task Save();
    }
}
