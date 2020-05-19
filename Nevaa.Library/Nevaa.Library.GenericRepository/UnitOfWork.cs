using Nevaa.Library.GenericRepository.Base;
using Nevaa.Library.GenericRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Nevaa.Library.GenericRepository
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        private bool disposed = false;

        public DbContext _dbContext { get; set; }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }

            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IRepository<T> GetRepository<T>() where T : BaseModel => new GenericRepository<T>(_dbContext);

        public int Commit()
        {
            return _dbContext.SaveChanges();
        }

        public List<T> GetDataWithSqlQuery<T>(string query) where T : BaseModel, new()
        {
            return new List<T>();// _dbContext.Database.Connection.Query<T>(query).ToList();
        }

        public DbContext GetDbContext()
        {
            return _dbContext;
        }
    }
}
