using ClassStudent.DAL.Context;
using ClassStudent.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClassStudent.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StudentRoomContext _studentRoomContext;
        public UnitOfWork(StudentRoomContext studentRoomContext)
        {
            _studentRoomContext = studentRoomContext;
        }
        public void Add<T>(T entity) where T : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _studentRoomContext.Set<T>().Add(entity);
        }

        public async Task AddAsync<T>(T entity) where T: class
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            await _studentRoomContext.Set<T>().AddAsync(entity);

        }

        public void Commit()
        {
            _studentRoomContext.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _studentRoomContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _studentRoomContext.Dispose();
        }

        public IQueryable<T> Get<T>() where T : class
        {
            return _studentRoomContext.Set<T>();
        }

        public DbContext GetContext()
        {
            return _studentRoomContext;
        }

        public virtual T GetSingle<T>(Expression<Func<T, Boolean>> predicate) where T : class
        {
            return _studentRoomContext.Set<T>().FirstOrDefault(predicate);
        }

        public async Task<T> GetSingleAsync<T>(Expression<Func<T, Boolean>> predicate) where T : class
        {
            return await _studentRoomContext.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public void Remove<T>(T entity) where T : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _studentRoomContext.Set<T>().Remove(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            if(entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _studentRoomContext.Set<T>().Update(entity);
        }
        
    }
}
