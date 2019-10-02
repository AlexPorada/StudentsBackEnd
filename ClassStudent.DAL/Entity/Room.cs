using ClassStudent.DAL.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClassStudent.DAL.Entity
{
    public class Room: IEntity<int>
    {
        public int Id { get; set; }

        [Required]
        public int Number { get; set; }

        public virtual Teacher Teacher { get; set; }

        public virtual IEnumerable<Student> Students { get; set; }

        public Room()
        {
            Students = new List<Student>();
        }
    }
}
