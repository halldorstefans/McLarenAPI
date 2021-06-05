using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using McLaren.Core.Entities;
using McLaren.Core.Interfaces.Repositories;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace McLaren.Infrastructure.Data.Repositories
{
    public class CarsRepository : Repository<Car>, ICarsRepository
    {        
        private readonly ILogger _cLogger;
        public CarsRepository(McLarenContext dbContext, ILoggerFactory loggerFactory) : base(dbContext, loggerFactory)
        {            
            _cLogger = loggerFactory.CreateLogger("Car Database");
        }

        public async Task<IEnumerable<Car>> GetByName(string name)
        {
            using (_cLogger.BeginScope("Car GetByName"))
            {
                _cLogger.LogInformation("GetByName");
                return await _dbContext.Set<Car>().Where(c => c.name == name).ToListAsync();
            }
        }
        public async Task<IEnumerable<Car>> GetByYear(int year)
        {
            using (_cLogger.BeginScope("Car GetByYear"))
            {
                _cLogger.LogInformation("GetByYear");
                return await _dbContext.Set<Car>().Where(c => c.fromyear <= year && c.toyear >= year).ToListAsync();
            }
        }
    }
}