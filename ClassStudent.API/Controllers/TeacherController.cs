using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ClassStudent.DAL.Entity;
using ClassStudent.DAL.Services.StudentService;
using ClassStudent.DAL.Services.TeachersService;
using ClassStudent.DTO.StudentDto;
using ClassStudent.DTO.TeacherDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClassStudent.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;
        private readonly IStudentService _studentService;
        public TeacherController(ITeacherService teacherService, IStudentService studentService)
        {
            _teacherService = teacherService;
            _studentService = studentService;

        }
        // GET: api/Teacher
        [HttpGet("getTeachers")]
        public IEnumerable<TeacherDto> GetTeachers()
        {
            var teachers = _teacherService.GetAll().ToList();
            var tDtos = Mapper.Map<IEnumerable<Teacher>, IEnumerable<TeacherDto>>(teachers);
            return tDtos;
        }

        [HttpPost("addStudents/{teacherId}", Name = "AddStudents")]
        public async Task<bool> AddStudents(int teacherId, [FromBody] int[] studentIds)
        {
            var teacher = await _teacherService.GetAsync(teacherId);
            if (teacher != null)
            {
                var students = _studentService.GetAll().Where(s => studentIds.Contains(s.Id)).ToList();
                foreach (var st in students)
                {
                    teacher.StudentTeacherTags.Add(new StudentTeacherTag { StudentId = st.Id, TeacherId = teacher.Id});
                }
                await _teacherService.CommitAsync(true);
                return true;
            }
            return false;
        }

        [HttpGet("getTeacherStudents/{teacherId}")]
        public async Task<IEnumerable<StudentDto>> GetTeacherStudents(int teacherId)
        {
            var teacher = await _teacherService.GetAsync(teacherId);
            if (teacher != null)
            {
                var students = teacher.StudentTeacherTags.Select(st => st.Student).ToList();
                var sDto = Mapper.Map<IEnumerable<Student>, IEnumerable<StudentDto>>(students);
                return sDto;
            }
            return new StudentDto[] { };
        }

        // GET: api/Teacher/5
        [HttpGet("getById/{id}", Name = "GetById")]
        public async Task<TeacherDto> GetById(int id)
        {
            var teacher = await _teacherService.GetAsync(id);
            var tDto = Mapper.Map<Teacher, TeacherDto>(teacher);
            return tDto;
        }

        // POST: api/Teacher
        [HttpPost]
        public async Task<int?> Post([FromBody] TeacherDto value)
        {
            if (ModelState.IsValid)
            {
                var teacher = Mapper.Map<TeacherDto, Teacher>(value);
                await _teacherService.CreateAsync(teacher, true);
                return teacher.Id;
            }
            return null;
        }

        // PUT: api/Teacher/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            var teacher = await _teacherService.GetAsync(id);
            if (teacher != null)
            {
                _teacherService.Remove(teacher, true);
                return true;
            }
            return false;
        }
    }
}
