using AutoMapper;
using ClassStudent.DAL.Entity;
using ClassStudent.DTO.StudentDto;

namespace ClassStudent.DAL.AutoMapperProfiles
{
    public class StudentProfile: Profile
    {
        public StudentProfile()
        {
            // update map
            CreateMap<Student, Student>()
                .ForMember(s => s.Id, opt => opt.Ignore())
                .ForMember(s => s.Room, opt => opt.Ignore())
                .ForMember(s => s.StudentTeacherTags, opt=>opt.Ignore());

            CreateMap<Student, StudentDto>();
            CreateMap<StudentDto, Student>();
        }
    }
}
