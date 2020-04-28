using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using McLaren.Core.Entities;
using McLaren.Core.Interfaces.Repositories;
using System.Linq.Expressions;
using System;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace McLaren.Infrastructure.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly McLarenContext _dbContext;
        private readonly ILogger _logger;

        public Repository(McLarenContext dbContext, ILoggerFactory loggerFactory)
        {
            _dbContext = dbContext;
            _logger = loggerFactory.CreateLogger("Database");
        }        

        public virtual async Task<T> Get(int id)
        {
            using (_logger.BeginScope("General Get"))
            {
                _logger.LogInformation("Get");
                return await _dbContext.Set<T>().FindAsync(id);
            }
        }
        
        public virtual async Task<IEnumerable<T>> GetAll()
        {
            using (_logger.BeginScope("General GetAll"))
            {
                _logger.LogInformation("GetAll");
                return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
            }
        }

        public virtual async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            using (_logger.BeginScope("General Find"))
            {
                _logger.LogInformation("Find");
                return await _dbContext.Set<T>().Where(predicate).ToListAsync();
            }
        }
    }
}