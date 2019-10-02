using AutoMapper;
using ClassStudent.DAL.Entity;
using ClassStudent.DTO.RoomDto;
using System.Collections.Generic;

namespace ClassStudent.DAL.AutoMapperProfiles
{
    public class RoomProfile: Profile
    {
        public RoomProfile()
        {
            CreateMap<Room, Room>()
                .ForMember(r => r.Id, opt => opt.Ignore())
                .ForMember(r => r.Teacher, opt => opt.MapFrom(r=> Mapper.Map<Teacher, Teacher>(r.Teacher)))
                .ForMember(r => r.Students, opt => opt.Ignore());

            CreateMap<Room, RoomDto>();
            CreateMap<RoomDto, Room>();
        }
    }
}
