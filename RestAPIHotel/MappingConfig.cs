using AutoMapper;
using RestAPIHotel.Models;
using RestAPIHotel.Models.DTO;

namespace RestAPIHotel
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Room, roomDTO>();
            CreateMap<roomDTO, roomDTO>();
            CreateMap<Room,roomCreateDTO>().ReverseMap();
            CreateMap<Room, roomUpdateDTO>().ReverseMap();
        }
    }
}
