using System.ComponentModel.DataAnnotations;

namespace ClassStudent.DTO.TeacherDto
{
    public class TeacherDto
    {
        public int? Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string SecondName { get; set; }

        public int? RoomId { get; set; }
    }
}
