using ClassStudent.API.Controllers;
using ClassStudent.DAL.AutoMapperProfiles;
using ClassStudent.MSTest.MockServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Linq;
using ClassStudent.DTO.RoomDto;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ClassStudent.MSTest
{
    [TestClass]
    public class RoomControllerTest
    {
        RoomController roomController;
        public RoomControllerTest()
        {
            var iRoomServiceMock = new IRoomServiceMock();
            roomController = new RoomController(iRoomServiceMock.GetIRoomServiceMock().Object);
        }

        [ClassInitialize()]
        public static void InitializationForTest(TestContext context)
        {
            AutoMapper.Mapper.Initialize(mp => {
                mp.AddProfile(new RoomProfile());
                mp.AddProfile(new TeacherProfile());
            });
        }

         [TestMethod]
         public async Task GetRoomByIdAction_ReturnDtoModel()
         {
             var room = await roomController.Get(1);
             Assert.AreNotEqual(null, room, $"room can't be null");
             Assert.AreEqual(room.Id, 1, "roomDto id should be 1");
             Assert.AreEqual(room.Number, 500, "roomDto number should be 500");
         }

        [TestMethod]
        public void GetAllRoom_ListOfRoom()
        {
            var roomList = roomController.GetRooms().ToList();
            Assert.AreNotEqual(null, roomList, "roomList can't be null");

            Assert.AreEqual(2, roomList.Count, "roomList should have 2 objects inside");
        }
        [TestMethod]
        public void CreateNewRoom_ReturnIdNot0()
        {
            var roomDto = new RoomDto()
            {
                Id = 0, Number = 320
            };
            var Id = roomController.Post(roomDto).Result;
            Assert.AreNotEqual(null, Id, "id can't ne null");
            Assert.AreNotEqual(0, Id.Value, $"id should not have 0 value but it is have {Id.Value} value");
        }

        [TestMethod]
        public void CreateNewRoom_ReturnNull()
        {
            var roomDto = new RoomDto();
            roomController.ModelState.AddModelError("Number", "Number of room should be on range between 1 and 1000");
            var Id = roomController.Post(roomDto).Result;
            Assert.AreEqual(null, Id, $"Id should be null. But it is has {Id} value");
        }

        [TestMethod]
        public void DeleteRoom_ReturnTrue()
        {
            var id = 1;
            var isSuccess = roomController.Delete(id).Result;
            Assert.AreEqual(true, isSuccess, $"Result should be true. But it is {isSuccess}");
        }
        [TestMethod]
        public async Task DeleteRoom_ReturnFalse()
        {
            var id = 3;
            var isSucess = await roomController.Delete(id);
            Assert.AreEqual(false, isSucess, $"Result should be false. But it is {isSucess}");
        }

        [TestMethod]
        public async Task GetRoomTeacherTestRoomWithTeacher_ReturnNotNull()
        {
            var teacherDto = await roomController.GetRoomTeacher(1);
            Assert.IsNotNull(teacherDto, $"roomDto should not be null");
        }
        [TestMethod]
        public async Task GetRoomTeacherTestRoomWithoutTeacher_ReturnNull()
        {
            var teacherDto = await roomController.GetRoomTeacher(2);
            Assert.IsNull(teacherDto, "teacherDto should be null");
        }
        [TestMethod]
        public async Task GetRoomTeacherByBadId_ReturnNull()
        {
            var teacherDto = await roomController.GetRoomTeacher(3);
            Assert.IsNull(teacherDto, "teacherDto should be null");
        }
    }
}
