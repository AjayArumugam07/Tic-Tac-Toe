using AutoMapper;
using Tic_Tac_Toe.Data;
using Tic_Tac_Toe.Models;

namespace Tic_Tac_Toe.Configurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Game, GameDTO>().ReverseMap();
            CreateMap<Game, CreateGameDTO>().ReverseMap();
            CreateMap<Game, ActiveGamesDTO>().ReverseMap();
            CreateMap<Player, PlayerDTO>().ReverseMap();
            CreateMap<Player, CreatePlayerDTO>().ReverseMap();
            CreateMap<Move, MoveDTO>().ReverseMap();
            CreateMap<Move, CreateMoveDTO>().ReverseMap();
        }
    }
}
