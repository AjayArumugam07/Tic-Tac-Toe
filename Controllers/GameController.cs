using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tic_Tac_Toe.Data;
using Tic_Tac_Toe.IRepository;
using static Tic_Tac_Toe.Repository.GameLogic;
using Tic_Tac_Toe.Models;

namespace Tic_Tac_Toe.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GameController> _logger;
        private readonly IMapper _mapper;

        public GameController(IUnitOfWork unitOfWork, ILogger<GameController> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        // Endpoint 1: Creates two new players and adds them to a game
        //             Returns ID of both players and game
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateGame([FromBody] CreatePlayersDTO playersDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(CreateGame)}");
                return BadRequest(ModelState);
            }

            var player1DTO = new CreatePlayerDTO { Name = playersDTO.Player1Name };
            var player2DTO = new CreatePlayerDTO { Name = playersDTO.Player2Name };

            var player1 = _mapper.Map<Player>(player1DTO);
            var player2 = _mapper.Map<Player>(player2DTO);

            List<Player> playersToAdd = new List<Player>()
            {
                player1,
                player2
            };

            await _unitOfWork.Players.InsertRange(playersToAdd);
            await _unitOfWork.Save();

            var gameDTO = new CreateGameDTO 
            { 
                Player1Id = player1.PlayerId, 
                Player2Id = player2.PlayerId 
            };

            var game = _mapper.Map<Game>(gameDTO);

            await _unitOfWork.Games.Insert(game);
            await _unitOfWork.Save();

            List<string> emptyBoardStringRepresentation = CreateBoardStringRepresentation(new int[3, 3]);

            var response = new
            {
                game = new
                {
                    id = game.GameId
                },
                player1 = new
                {
                    id = player1.PlayerId,
                    name = player1.Name
                },
                player2 = new
                {
                    id = player2.PlayerId,
                    name = player2.Name
                },
                message = "Player 1's turn",
                row0 = emptyBoardStringRepresentation[0],
                row1 = emptyBoardStringRepresentation[1],
                row2 = emptyBoardStringRepresentation[2]
            };

            return Created("Game", response);
        }

        // Endpoint 3: Gets all active games and displays its players and the number of moves registered
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetActiveGames()
        {
            var games = await _unitOfWork.Games.GetAll(game => game.Status == (int) GameStatus.INCOMPLETE, null, new List<string> { "Player1", "Player2"});
            var activeGames = _mapper.Map<IList<ActiveGamesDTO>>(games);

            return Ok(activeGames);
        }
    }
}
