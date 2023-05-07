using AutoMapper;

using Ofima.TechnicalTest.Common.Dto;
using Ofima.TechnicalTest.Common.Models;
using Ofima.TechnicalTest.Infraestructure.Models;

namespace Ofima.TechnicalTest.Common
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Move, MoveDto>().ReverseMap();
            CreateMap<Game, RegisterGame>().ReverseMap();
            CreateMap<GameMove, RegisterMove>().ReverseMap();
        }
    }
}
