using AutoMapper;
using ClassStudent.DAL.Entity;
using ClassStudent.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassStudent.DAL.Services.StudentService
{
    public class StudentService:BaseService<Student, int>, IStudentService
    {
        public StudentService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override void Update(Student entity, Boolean shouldBeCommited = false)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var updateItem = Get(entity.Id);
            if (updateItem == null)
            {
                throw new ArgumentException("Can't find item with id " + entity.Id);
            }
            Mapper.Map(entity, updateItem);
            Commit(shouldBeCommited);

        }
    }
}
