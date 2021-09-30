using Metrik.Library.GenericRepository;
using Metrik.Library.GenericRepository.Interfaces;
using Metrik.Library.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrik.Library.Data.Extensions
{
    public static class DependencyManager
    {
        public static IConfiguration Configuration {  get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <param name="Configuration"></param>
        /// <param name="connectionName"></param>
        public static void AddDbContextDependencies<T>(this IServiceCollection services,IConfiguration Configuration,string connectionName) where T : DbContext
        {
            services.AddDbContext<T>(s =>
            {
                s.UseSqlServer(Configuration.GetConnectionString(connectionName));
            });
            services.AddTransient(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddTransient(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddTransient<DbContext, T>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
            
        public static void CreateDatabase(object dbContext)
        {
            
            ((DbContext)dbContext).Database.EnsureCreated();
            //var databaseCreator = dbContext.GetService<IRelationalDatabaseCreator>();
            //databaseCreator.CreateTables();
        }

        public static void DropDatabase(object dbContext)
        {
            ((DbContext)dbContext).Database.EnsureDeleted();
        }

    }
}
