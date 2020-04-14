using System.Linq.Expressions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using McLaren.Core.Entities;
using McLaren.Core.Interfaces.Repositories;
using System.Collections.Generic;

namespace McLaren.Infrastructure.Data.Repositories
{
    public class CarsRepository : Repository<Car>, ICarsRepository
    {
        public CarsRepository(McLarenContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Car>> GetByName(string name)
        {
            return await _dbContext.Set<Car>().Where(c => c.name == name).ToListAsync();
        }
        public async Task<IEnumerable<Car>> GetByYear(int year)
        {
            return await _dbContext.Set<Car>().Where(c => c.fromyear <= year && c.toyear >= year).ToListAsync();
        }
    }
}