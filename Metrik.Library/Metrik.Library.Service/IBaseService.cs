using FluentValidation.Results;
using Metrik.Library.GenericRepository.Base;
using Metrik.Library.GenericRepository.Base.Interfaces;
using Metrik.Library.GenericRepository.Extensions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Metrik.Library.Service
{
    public interface IBaseService<TEntity> where TEntity : BaseModel,IBaseModel
    {
        IQueryable<TEntity> GetAllActiveData(Expression<Func<TEntity, bool>> filter = null, Func<IIncludable<TEntity>, IIncludable> includes = null);
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate, Func<IIncludable<TEntity>, IIncludable> includes = null);
        TEntity GetById(int id, Func<IIncludable<TEntity>, IIncludable> includes = null);
        TEntity Find(Expression<Func<TEntity, bool>> predicate, Func<IIncludable<TEntity>, IIncludable> includes = null);
        void Delete(int id, bool forceDelete = false);
        ValidationResult Save(TEntity entity, bool commit = true);
        void Dispose();
        int Commit();
        IQueryable<TEntity> GetListByQuery(string query);
    }
}
