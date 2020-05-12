using Nevaa.Library.GenericRepository.Base;
using Nevaa.Library.GenericRepository.Extensions;
using Nevaa.Library.GenericRepository.Extensions.Interfaces;
using Nevaa.Library.GenericRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Nevaa.Library.GenericRepository
{
    public class GenericRepository<T> : IRepository<T> where T : BaseModel
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void AddRange(List<T> entities)
        {
            _dbSet.AddRange(entities);
        }

        public void Delete(int id, bool forceDelete = false)
        {
            var entity = GetById(id);
            if (entity == null) return;
            else
            {
                if (!forceDelete)
                {
                    T _entity = entity;
                    _entity.GetType().GetProperty("Aktif").SetValue(_entity, true);

                    this.Update(_entity);
                }
                else
                {
                    Delete(entity);
                }
            }
        }

        public void Delete(T entity, bool forceDelete = false)
        {
            //entity.GetType().GetProperty("Aktif") != null
            if (!forceDelete)
            {
                //T _entity = entity;

                //_entity.GetType().GetProperty("Aktif").SetValue(_entity, true);

                this.Update(entity);
            }
            else
            {
                // Önce entity'nin state'ini kontrol etmeliyiz.
                EntityEntry dbEntityEntry = _dbContext.Entry(entity);

                if (dbEntityEntry.State != EntityState.Deleted)
                {
                    dbEntityEntry.State = EntityState.Deleted;
                }
                else
                {
                    _dbSet.Attach(entity);
                    _dbSet.Remove(entity);
                }
            }
        }

        public void Delete(List<T> entities, bool forceDelete = false)
        {
            foreach (var entity in entities)
            {
                Delete(entity, forceDelete);
            }
            //_dbSet.RemoveRange(entities.AsEnumerable());
        }

        public void Delete(ICollection<T> entities, bool forceDelete = false)
        {
            throw new NotImplementedException();
        }

        public void Delete(Expression<Func<T, bool>> predicate, bool forceDelete = false)
        {
            Delete(_dbSet.Where(predicate).ToList(), forceDelete);
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public T Get(Expression<Func<T, bool>> predicate, Func<IIncludable<T>, IIncludable> includes = null)
        {
            IQueryable<T> query = _dbSet.AsQueryable();
            if (includes != null)
                query = _dbSet.IncludeMultiple(includes);
            return query.FirstOrDefault(predicate);
        }

        public T GetById(int id, Func<IIncludable<T>, IIncludable> includes = null)
        {
            IQueryable<T> query = _dbSet.AsQueryable();
            if (includes != null)
                query = _dbSet.IncludeMultiple(includes);
            return query.FirstOrDefault(s => s.Id == id);
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null, Func<IIncludable<T>, IIncludable> includes = null)
        {
            IQueryable<T> query = _dbSet.AsQueryable();
            if (includes != null)
                query = _dbSet.IncludeMultiple(includes);
            if (predicate != null)
                query = query.Where(predicate);
            return query;
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public T Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var query = _dbSet.AsQueryable().Where(predicate);
            if (includes != null)
            {
                foreach (var include in includes)
                    query = query.Include(include);
                return query.SingleOrDefault();
            }
            return _dbSet.Where(predicate).SingleOrDefault();
        }

        //public IQueryable<T> GetDataWithSqlQuery(FormattableString query)
        //{
        //    return _dbSet.FromSql<T>()
        //}
    }
}
