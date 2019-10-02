using ClassStudent.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClassStudent.DAL.Services
{
    public class BaseService<TEntity, TKey> : IBaseService<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        protected readonly IUnitOfWork _unitOfWork;
        public BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public virtual void Commit(Boolean shouldBeCommited = false)
        {
            if (shouldBeCommited)
            {
                _unitOfWork.Commit();
            }
        }

        public virtual async Task CommitAsync(Boolean shouldBeCommited = false)
        {
            if (shouldBeCommited)
            {
                await _unitOfWork.CommitAsync();
            }
        }

        public virtual void Create(TEntity entity, Boolean shouldBeCommited = false)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _unitOfWork.Add<TEntity>(entity);
            Commit(shouldBeCommited);
        }

        public virtual async Task CreateAsync(TEntity entity, Boolean shouldBeCommited = false)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            await _unitOfWork.AddAsync(entity);
            await CommitAsync(shouldBeCommited);

        }

        public virtual TEntity Get(TKey id)
        {
            return GetAll().FirstOrDefault(e => (object)e.Id == (object)id);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return GetList();
        }

        public virtual async Task<TEntity> GetAsync(TKey id)
        {
            return await GetAll().FirstOrDefaultAsync(e => (object)e.Id == (object)id);
        }

        protected virtual IQueryable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate)
        {
            var list = GetList();
            if (predicate != null)
            {
                list = list.Where(predicate);
            }
            return list;
        }

        protected virtual IQueryable<TEntity> GetList<TOrderKey>(Expression<Func<TEntity, bool>> filterPredicate, Expression<Func<TEntity, TOrderKey>> orderPredicate)
        {
            return GetList(filterPredicate).OrderBy(orderPredicate);
        }

        protected virtual IQueryable<TEntity> GetList()
        {
            return _unitOfWork.Get<TEntity>();
        }

        public virtual void Remove(TEntity entity, Boolean shouldBeCommited = false)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _unitOfWork.Remove(entity);
            Commit(shouldBeCommited);
        }

        public virtual void Update(TEntity entity, Boolean shouldBeCommited = false)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _unitOfWork.Update(entity);
            Commit(shouldBeCommited);
        }
    }
}
