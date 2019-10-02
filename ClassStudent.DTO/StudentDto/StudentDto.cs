using System;
using System.ComponentModel.DataAnnotations;

namespace ClassStudent.DTO.StudentDto
{
    public class StudentDto
    {
        public int? Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string SecondName { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }
        public int? RoomId { get; set; }
    }
}
