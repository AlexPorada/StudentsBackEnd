using ClassStudent.DAL.Entity;
using ClassStudent.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassStudent.DAL.Services.RoomsService
{
    public class RoomService:BaseService<Room, int>, IRoomService
    {
        public RoomService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

       // public override void Update(Room entity, Boolean shouldBeCommited = false)
       // {
       //     if (entity == null)
       //     {
       //         throw new ArgumentNullException(nameof(entity));
       //     }
       //     var updateEntity = Get(entity.Id);
       //     if (updateEntity == null)
       //     {
       //         throw new ArgumentException("Can't find entity with Id " + entity.Id );
       //     }
       //     AutoMapper.Mapper.Map(entity, updateEntity);
       //     Commit(shouldBeCommited);
       // }

        public IEnumerable<Student> GetRoomStudents(int roomId)
        {
            var room = Get(roomId);
            if (room == null)
            {
                throw new ArgumentException("Can't find room with id " + roomId);
            }

            return room.Students;
        }
    }
}
