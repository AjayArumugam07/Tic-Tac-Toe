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

            var movesList = await _unitOfWork.Moves.GetAll(move => move.GameId == game.GameId);

            int[,] board = CreateBoard(movesList, game);

            bool isAlreadyOccupied = board[currentMove.RowNumber, currentMove.ColumnNumber] != 0;
            if (isAlreadyOccupied)
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                string responseMessage = "Cell (" + currentMove.RowNumber +
                              ", " + currentMove.ColumnNumber + ") is already occupied. Please choose an empty cell";
                response.Content = new StringContent(responseMessage);
                return BadRequest(responseMessage);
            }

            // Player's Move is now valid
            game.NumberOfMoves++;
            _unitOfWork.Games.Update(game);

            currentMove.GameId = game.GameId;
            await _unitOfWork.Moves.Insert(currentMove);

            await _unitOfWork.Save();

            string player = currentMove.PlayerId == game.Player1Id ? "Player 1" : "Player 2";
            GameStatus gameStatus = CheckGameStatus(currentMove, game, board);

            board[currentMove.RowNumber, currentMove.ColumnNumber] =
                currentMove.PlayerId == game.Player1Id ? 1 : 2;

            string message = "";
            switch (gameStatus)
            {
                case GameStatus.INCOMPLETE:
                    message = player + " has registered their move";
                    break;
                case GameStatus.DRAW:
                    message = "Game is drawn!";
                    break;
                case GameStatus.PLAYER1WON:
                    message = "Player 1 has won the game!";
                    break;
                case GameStatus.PLAYER2WON:
                    message = "Player 2 has won the game!";
                    break;
            }

            List<string> boardRepresentation = CreateBoardRepresentation(board);

            var responseObject = new
            {
                Message = message,
                row0 = boardRepresentation[0],
                row1 = boardRepresentation[1],
                row2 = boardRepresentation[2]
            };

            if(gameStatus == GameStatus.INCOMPLETE) return Created("Move", responseObject);

            return Created("Move", responseObject);

        }
    }
}
