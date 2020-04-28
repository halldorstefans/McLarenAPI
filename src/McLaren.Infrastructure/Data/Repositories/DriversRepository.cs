using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using McLaren.Core.Entities;
using McLaren.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace McLaren.Infrastructure.Data.Repositories
{
    public class DriversRepository : Repository<Driver>, IDriversRepository
    {
        private readonly ILogger _dLogger;
        public DriversRepository(McLarenContext dbContext, ILoggerFactory loggerFactory) : base(dbContext, loggerFactory)
        {
            _dLogger = loggerFactory.CreateLogger("Driver Database");
        }

        public async Task<IEnumerable<Driver>> GetByName(string name)
        {
            using (_dLogger.BeginScope("Driver GetByName"))
            {
                _dLogger.LogInformation("GetByName");
                return await _dbContext.Set<Driver>().Where(d => d.firstName.Contains(name) || d.lastName.Contains(name)).ToListAsync();
            }
        }
    }
}