using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ClassStudent.DAL.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        IQueryable<T> Get<T>() where T:class;
        void Add<T>(T entity) where T : class;
        Task AddAsync<T>(T entity) where T : class;
        void Remove<T>(T entity) where T : class;
        T GetSingle<T>(Expression<Func<T, bool>> predicate) where T : class;
        Task<T> GetSingleAsync<T>(Expression<Func<T, bool>> predicate) where T : class;
        DbContext GetContext();
        void Commit();
        Task CommitAsync();

        void Update<T>(T entity) where T : class;
    }
}
