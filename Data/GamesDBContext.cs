using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TicTacToe.Data
{
    public class GamesDBContext : DbContext
    {

        public DbSet<Game> Games { get; set; }
        public DbSet<Move> Moves { get; set; }  
        public DbSet<Player> Players { get; set; }
        public string DbPath { get; private set; }
        public GamesDBContext(DbContextOptions options) : base(options)
        {
        }
        // Configures EF to create a Sqlite database file in the local folder

    }
}