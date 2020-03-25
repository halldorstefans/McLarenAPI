using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using McLaren.Core.Entities;
using McLaren.Core.Interfaces.Repositories;
using System.Linq.Expressions;
using System;
using System.Linq;

namespace McLaren.Infrastructure.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly McLarenContext _dbContext;

        public Repository(McLarenContext dbContext)
        {
            _dbContext = dbContext;
        }        

        public virtual async Task<T> Get(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }
        
        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }
    }
}