using Metrik.Library.GenericRepository.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Metrik.Library.GenericRepository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext _dbContext { get; set; }
        List<T> GetDataWithSqlQuery<T>(string query) where T : BaseModel, new();
        IRepository<T> GetRepository<T>() where T : BaseModel;
        int Commit();
        DbContext GetDbContext();

    }
}
