using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ClassStudent.DAL.Entity;
using ClassStudent.DAL.Services.StudentService;
using ClassStudent.DTO.StudentDto;
using Microsoft.AspNetCore.Mvc;

namespace ClassStudent.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        } 
        // GET api/values
        [HttpGet]
        public IEnumerable<StudentDto> GetStudents()
        {
            var studentsDto = Mapper.Map<IEnumerable<Student>, IEnumerable<StudentDto>>(_studentService.GetAll().ToList());
            return studentsDto;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDto>> Get(int id)
        {
            var student = await _studentService.GetAsync(id);
            var dto = Mapper.Map<Student, StudentDto>(student);
            return dto;
        }

        // POST api/values
        [HttpPost]
        public async Task<int?>  Post([FromBody] StudentDto value)
        {
            if (ModelState.IsValid)
            {
                var student = Mapper.Map<StudentDto, Student>(value);
                await _studentService.CreateAsync(student, true);
                return student.Id; 

            }
            return null;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var student = await _studentService.GetAsync(id);
            if (student != null)
            {
                _studentService.Remove(student, true);
                return true;
            }
            return false;
        }
    }
}
