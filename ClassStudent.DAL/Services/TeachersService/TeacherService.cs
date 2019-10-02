using ClassStudent.DAL.Entity;
using ClassStudent.DAL.Interfaces;
using System;

namespace ClassStudent.DAL.Services.TeachersService
{
    public class TeacherService:BaseService<Teacher, int>, ITeacherService
    {
        public TeacherService(IUnitOfWork unitOfWork):base(unitOfWork)
        {
        }

        public override void Update(Teacher entity, Boolean shouldBeCommited = false)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var updateEntity = Get(entity.Id);
            if (updateEntity == null)
            {
                throw new ArgumentException("Can't find entity with id " + entity.Id);
            }

            AutoMapper.Mapper.Map(entity, updateEntity);
            Commit(shouldBeCommited);
        }
    }
}
