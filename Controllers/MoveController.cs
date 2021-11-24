using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Tic_Tac_Toe.Data;
using Tic_Tac_Toe.IRepository;
using static Tic_Tac_Toe.Repository.GameLogic;
using Tic_Tac_Toe.Models;

namespace Tic_Tac_Toe.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MoveController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<MoveController> _logger;
        private readonly IMapper _mapper;

        public MoveController(IUnitOfWork unitOfWork, ILogger<MoveController> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        // Endpoint 2: Processes a Move
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateMove([FromBody] CreateMoveDTO moveDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(CreateMove)}");
                return BadRequest(ModelState);
            }

            var currentMove = _mapper.Map<Move>(moveDTO);

            // Get the player's game data
            var game = await _unitOfWork.Games.Get(game => 
                game.Player1Id == currentMove.PlayerId || game.Player2Id == currentMove.PlayerId);

            if (isGameComplete(game))
            {
                return BadRequest("This game is already complete. Please create a new one");
            }

            if (!isPlayersTurn(game, currentMove))
            {
                return BadRequest("Wait for opponent to make a move before you play");
            }

            var movesList = await _unitOfWork.Moves.GetAll(move => move.GameId == game.GameId);

            int[,] board = CreateBoard(movesList, game);

            bool isAlreadyOccupied = board[currentMove.RowNumber, currentMove.ColumnNumber] != 0;
            if (isAlreadyOccupied)
            {
                string responseMessage = "Cell (" + currentMove.RowNumber +
                              ", " + currentMove.ColumnNumber + ") is already occupied." +
                              " Please choose an empty cell";
                return BadRequest(responseMessage);
            }

            // Player's Move is now valid
            game.NumberOfMoves++;
            _unitOfWork.Games.Update(game);

            currentMove.GameId = game.GameId;
            await _unitOfWork.Moves.Insert(currentMove);

            board[currentMove.RowNumber, currentMove.ColumnNumber] =
                    currentMove.PlayerId == game.Player1Id ? 1 : 2;

            GameStatus gameStatus = CheckGameStatus(currentMove, game, board);

            string message = "";
            string player = currentMove.PlayerId == game.Player1Id ? "Player 1" : "Player 2";
            switch (gameStatus)
            {
                case GameStatus.INCOMPLETE:
                    message = player + " has registered their move";
                    break;
                case GameStatus.DRAW:
                    game.Status = (int) GameStatus.DRAW;
                    message = "Game is drawn!";
                    break;
                case GameStatus.PLAYER1WON:
                    game.Status = (int) GameStatus.PLAYER1WON;
                    message = "Player 1 has won the game!";
                    break;
                case GameStatus.PLAYER2WON:
                    game.Status = (int)GameStatus.PLAYER2WON;
                    message = "Player 2 has won the game!";
                    break;
            }

            await _unitOfWork.Save();

            List<string> boardStringRepresentation = CreateBoardRepresentation(board);

            var responseObject = new
            {
                Message = message,
                row0 = boardStringRepresentation[0],
                row1 = boardStringRepresentation[1],
                row2 = boardStringRepresentation[2]
            };

            return Created("Move", responseObject);

        }
    }
}
