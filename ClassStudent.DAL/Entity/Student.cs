using ClassStudent.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClassStudent.DAL.Entity
{
    public class Student:IEntity<int>
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } 

        [Required]
        [StringLength(100)]
        public string SecondName { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        public int? RoomId { get; set; }
        public virtual Room Room { get; set; }

        public virtual IList<StudentTeacherTag> StudentTeacherTags { get; set; }

        public Student()
        {
            StudentTeacherTags = new List<StudentTeacherTag>();
        }
    }
}
