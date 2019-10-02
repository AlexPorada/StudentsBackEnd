using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ClassStudent.DAL.Entity;
using ClassStudent.DAL.Services.RoomsService;
using ClassStudent.DTO.RoomDto;
using ClassStudent.DTO.StudentDto;
using ClassStudent.DTO.TeacherDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClassStudent.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }
        // GET: api/Room
        [HttpGet]
        public IEnumerable<RoomDto> GetRooms()
        {
            var rooms = _roomService.GetAll().ToList();
            var response = Mapper.Map<IEnumerable<Room>, IEnumerable<RoomDto>>(rooms);
            return response;
        }

        // GET: api/Room/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<RoomDto> Get(int id)
        {
            var room = await _roomService.GetAsync(id);
            var dto = Mapper.Map<Room, RoomDto>(room);
            return dto;
        }

        [HttpGet("GetStudents/{roomId}", Name = "GetStudents")]
        public async Task<IEnumerable<StudentDto>> GetStudents(int roomId)
        {
            var room = await _roomService.GetAsync(roomId);
            if (room != null && room.Students != null)
            {
                var students = room.Students.ToList();
                var stDto = Mapper.Map<IEnumerable<Student>, IEnumerable<StudentDto>>(students);
                return stDto;
            }
            return new StudentDto[] { };
        }

        [HttpGet("GetRoomTeacher/{roomId}", Name = "GetRoomTeacher")]
        public async Task<TeacherDto> GetRoomTeacher(int roomId)
        {
            var room = await _roomService.GetAsync(roomId);
            if (room != null && room.Teacher != null)
            {
                var tDto = Mapper.Map<Teacher, TeacherDto>(room.Teacher);
                return tDto;
            }
            return null;
        }

        // POST: api/Room
        [HttpPost]
       
        public async Task<int?> Post([FromBody] RoomDto value)
        {
            if (ModelState.IsValid)
            {
                var room = Mapper.Map<RoomDto, Room>(value);
                await _roomService.CreateAsync(room, true);
                return room.Id;
            }
            return null;
        }

        // PUT: api/Room/5
        [HttpPut("{id}")]
        public bool Put(int id, [FromBody] RoomDto value)
        {
            if (ModelState.IsValid)
            {
                var updateItem = Mapper.Map<RoomDto, Room>(value);
                try
                {
                    _roomService.Update(updateItem, true);
                    return true;
                }
                catch(Exception ex)
                {
                    return false;
                }
            }
            return false;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            var room = await _roomService.GetAsync(id);
            if (room != null)
            {
                _roomService.Remove(room, true);
                return true;
            }
            return false; 
        }

        
    }
    
}
