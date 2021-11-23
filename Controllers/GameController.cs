using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tic_Tac_Toe.Data;
using Tic_Tac_Toe.IRepository;
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

            var player1DTO = new CreatePlayerDTO();
            var player2DTO = new CreatePlayerDTO();

            player1DTO.Name = playersDTO.Player1Name;
            player2DTO.Name = playersDTO.Player2Name;

            var player1 = _mapper.Map<Player>(player1DTO);
            var player2 = _mapper.Map<Player>(player2DTO);

            List<Player> playersToAdd = new List<Player>()
            {
                player1,
                player2
            };

            await _unitOfWork.Players.InsertRange(playersToAdd);
            await _unitOfWork.Save();

            var gameDTO = new CreateGameDTO();
            gameDTO.Player1Id = player1.PlayerId;
            gameDTO.Player2Id = player2.PlayerId;

            var game = _mapper.Map<Game>(gameDTO);
            await _unitOfWork.Games.Insert(game);
            await _unitOfWork.Save();

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
                }
            };

            return Created("Game", response);
        }
    }
}
