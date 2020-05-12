using Nevaa.Library.GenericRepository.Base;
using Nevaa.Library.GenericRepository.Extensions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Nevaa.Library.GenericRepository.Interfaces
{
    public interface IRepository<T> : IDisposable where T : BaseModel
    {
        T GetById(int id, Func<IIncludable<T>, IIncludable> includes = null);
        T Get(Expression<Func<T, bool>> predicate, Func<IIncludable<T>, IIncludable> includes = null);
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null, Func<IIncludable<T>, IIncludable> includes = null);
        void Add(T entity);
        void AddRange(List<T> entities);
        void Update(T entity);
        void Delete(int id, bool forceDelete = false);
        void Delete(T entity, bool forceDelete = false);
        void Delete(List<T> entities, bool forceDelete = false);
        void Delete(ICollection<T> entities, bool forceDelete = false);
        void Delete(Expression<Func<T, bool>> predicate, bool forceDelete = false);
        //IQueryable<T> GetDataWithSqlQuery(string query);
    }
}
