using AutoMapper;
using ClassStudent.DAL.Entity;
using ClassStudent.DTO.TeacherDto;
using System.Collections.Generic;

namespace ClassStudent.DAL.AutoMapperProfiles
{
    public class TeacherProfile:Profile
    {
        public TeacherProfile()
        {
            CreateMap<Teacher, Teacher>()
                .ForMember(t => t.Id, opt => opt.Ignore())
                .ForMember(t => t.Room, opt => opt.Ignore())
                .ForMember(t => t.StudentTeacherTags, opt => opt.Ignore());

            CreateMap<Teacher, TeacherDto>();
            CreateMap<TeacherDto, Teacher>();
        }
    }
}
