using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Tic_Tac_Toe.Data
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
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Player>().HasData(
                new Player
                {
                    PlayerId = 1,
                    Name = "Ajay"
                },
                new Player
                {
                    PlayerId = 2,
                    Name = "Tom",
                });

            builder.Entity<Game>().HasData(
                new Game
                {
                    GameId = 1,
                    NumberOfMoves = 2,
                    Status = -1,
                    Player1Id = 1,
                    Player2Id = 2
                });

            builder.Entity<Move>().HasData(
                new Move
                {
                    MoveId = 1,
                    XCoordinate = 0,
                    YCoordinate = 0,
                    PlayerId = 1,
                    GameId = 1
                },
                new Move
                {
                    MoveId = 2,
                    XCoordinate = 1,
                    YCoordinate = 2,
                    PlayerId = 2,
                    GameId = 1
                });

        }

    }
}