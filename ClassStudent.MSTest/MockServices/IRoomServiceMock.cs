using ClassStudent.DAL.Entity;
using ClassStudent.DAL.Services.RoomsService;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using ClassStudent.DTO.RoomDto;

namespace ClassStudent.MSTest.MockServices
{
    public class IRoomServiceMock
    {
        private Mock<IRoomService> roomServiceMock = new Mock<IRoomService>();
        private List<Room> roomList = new List<Room>();
        private Room r = new Room() { Id = 3, Number = 320, Students = null, Teacher = null };
        public IRoomServiceMock()
        {
            InitializeRoomList();
            SetupIRoomServiceMock();
        }

        public Mock<IRoomService> GetIRoomServiceMock()
        {
            return roomServiceMock;
        }

        private void InitializeRoomList()
        {
            roomList.AddRange( 
                new[] {
                    new Room()
                    {
                        Id = 1,
                        Number = 500,
                        Students = null,
                        Teacher = new Teacher ()
                        {
                            Id = 1, 
                            FirstName = "Doctor",
                            SecondName = "Strange"
                        }
                    },
                    new Room()
                    {
                        Id = 2,
                        Number = 324,
                        Students = null,
                        Teacher = null

                    }
                });
        
    }
        delegate Task CreateAsyncResult(Room r, bool saveChanges);
        private void SetupIRoomServiceMock()
        {
            var task = new Task<Room>(() => { return roomList.FirstOrDefault(r => r.Id == 1 && r.Number == It.IsAny<int>()); });

            roomServiceMock.Setup(srv => srv.GetAsync(It.IsAny<int>()))
                .Returns((int roomId)=> 
                {
                    return Task.Run(()=> 
                    {
                        return roomList.FirstOrDefault(r => r.Id == roomId);
                    });
                });
           // roomServiceMock.Setup(srv => srv.GetAsync(1)).Returns(
           //    Task.Run(() => { return roomList.FirstOrDefault(r => r.Id == 1); })
           // );
            roomServiceMock.Setup(srv => srv.GetAll()).Returns(roomList.AsQueryable);

            roomServiceMock.Setup(src => src.CreateAsync(It.IsAny<Room>(), It.IsAny<bool>()))
                .Returns(
                    new CreateAsyncResult((Room r, bool saveChanges) => {
                        if (saveChanges)
                        {
                            r.Id = roomList.Count+1;
                            roomList.Add(r);
                        }
                        return Task.Run(() => { });
                    })
                );

            roomServiceMock.Setup(src => src.Remove(It.IsAny<Room>(), It.IsAny<bool>())).Callback((Room roomModel, bool saveChanges )=> 
            {
                if (saveChanges)
                {
                    var removedEl = roomList.FirstOrDefault(r => r.Id == roomModel.Id);
                   
                    roomList.Remove(removedEl);
                }
            });
           
        }
    }
}
