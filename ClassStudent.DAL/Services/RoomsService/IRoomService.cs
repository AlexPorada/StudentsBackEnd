using ClassStudent.DAL.Entity;
using ClassStudent.DAL.Interfaces;
using System.Collections.Generic;


namespace ClassStudent.DAL.Services.RoomsService
{
    public interface IRoomService: IBaseService<Room, int>
    {
        IEnumerable<Student> GetRoomStudents(int roomId);
    }
}
