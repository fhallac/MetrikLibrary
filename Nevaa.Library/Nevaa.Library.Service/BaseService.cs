using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Nevaa.Library.GenericRepository;
using Nevaa.Library.GenericRepository.Base;
using Nevaa.Library.GenericRepository.Extensions.Interfaces;
using Nevaa.Library.GenericRepository.Interfaces;

namespace Nevaa.Library.Service
{
    public class BaseService<T> : IBaseService<T> where T : BaseModel
    {
        public IUnitOfWork _unitOfWork;
        private readonly IRepository<T> _repository;
        public BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<T>();
        }
        public int Commit()
        {
            return _unitOfWork._dbContext.SaveChanges();
        }

        public void Delete(int id, bool forceDelete = false)
        {
            _repository.Delete(id, forceDelete);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public T Find(Expression<Func<T, bool>> predicate, Func<IIncludable<T>, IIncludable> includes = null)
        {
            return _repository.Get(predicate,includes);
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate, Func<IIncludable<T>, IIncludable> includes = null)
        {
            return _repository.GetAll(predicate, includes);
        }

        public IQueryable<T> GetAllActiveData(Expression<Func<T, bool>> filter = null, Func<IIncludable<T>, IIncludable> includes = null)
        {
            return _repository.GetAll(filter, includes);
        }

        public T GetById(int id, Func<IIncludable<T>, IIncludable> includes = null)
        {
            return _repository.GetById(id,includes);
        }

        public IQueryable<T> GetListByQuery(string query)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Save(T entity, bool commit = true)
        {
            bool result = true;
            if (entity.Id == 0)
            {
                entity.IsActive = true;
                entity.CreatedBy = 1;
                entity.CreatedDate = DateTime.Now;
                _repository.Add(entity);

            }
            else
            {
                entity.UpdatedBy = 1;
                entity.UpdatedDate = DateTime.Now;
                _repository.Update(entity);
            }

            if (commit)
                result = _unitOfWork.Commit() > 0;
            if (result)
            {
                return new ValidationResult();
            }
            else
            {
                return new ValidationResult(new List<ValidationFailure>() { new ValidationFailure("Test", "Hata") });
            }
        }
    }
}
