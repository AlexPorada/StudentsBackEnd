using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassStudent.DAL.Interfaces
{
    public interface IBaseService<TEntity, TKey> where TEntity: class
    {
        IQueryable<TEntity> GetAll();

        TEntity Get(TKey id);
        Task<TEntity> GetAsync(TKey id);

        void Create(TEntity entity, bool shouldBeCommited = false);
        Task CreateAsync(TEntity entity, bool shouldBeCommited = false);

        void Remove(TEntity entity, bool shouldBeCommited = false);

        void Commit(bool shouldBeCommited = false);

        Task CommitAsync(bool shouldBeCommited = false);
        void Update(TEntity entity, bool shouldBeCommited = false);
    }
}
