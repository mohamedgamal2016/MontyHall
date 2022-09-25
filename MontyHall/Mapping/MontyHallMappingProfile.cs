using AutoMapper;
using MontyHall.Core.Commands;
using MontyHall.Models.Request;

namespace MontyHall.Mapping
{
    public class MontyHallMappingProfile : Profile
    {
        public MontyHallMappingProfile()
        {
            CreateMap<PlayGameRequest, PlayGameCommand>();
        }
    }
}
