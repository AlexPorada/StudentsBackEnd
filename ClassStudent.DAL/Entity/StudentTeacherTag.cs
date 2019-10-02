using ClassStudent.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassStudent.DAL.Entity
{
    public class StudentTeacherTag : IEntity<int>
    {
        public Int32 Id { get; set; }

        public int StudentId { get; set; }

        public virtual Student Student { get; set; }

        public int TeacherId { get; set; }
        public virtual Teacher Teacher { get;set; }
    }
}
