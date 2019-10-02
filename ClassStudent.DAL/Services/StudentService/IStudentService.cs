using ClassStudent.DAL.Entity;
using ClassStudent.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassStudent.DAL.Services.StudentService
{
    public interface IStudentService:IBaseService<Student, int>
    {
    }
}
