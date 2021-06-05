using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using McLaren.Core.Entities;
using McLaren.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace McLaren.Infrastructure.Data.Repositories
{
    public class GrandPrixesRepository : Repository<GrandPrix>, IGrandPrixesRepository
    {
        private readonly ILogger _gpLogger;
        public GrandPrixesRepository(McLarenContext dbContext, ILoggerFactory loggerFactory) : base(dbContext, loggerFactory)
        {
            _gpLogger = loggerFactory.CreateLogger("GrandPrix Database");
        }

        public async Task<IEnumerable<GrandPrix>> GetByRaceId(int raceId)
        {
            using (_gpLogger.BeginScope("Grand Prix GetByRaceId"))
            {
                _gpLogger.LogInformation("GetByRaceId");
                return await _dbContext.Set<GrandPrix>().Where(gp => gp.raceid == raceId).ToListAsync();
            }
        }
        
        public async Task<IEnumerable<GrandPrix>> GetByCountry(string country)
        {
            using (_gpLogger.BeginScope("Grand Prix GetByCountry"))
            {
                _gpLogger.LogInformation("GetByCountry");
                return await _dbContext.Set<GrandPrix>().Where(gp => gp.country == country).ToListAsync();
            }
        }

        public async Task<IEnumerable<GrandPrix>> GetByYear(int year)
        {
            using (_gpLogger.BeginScope("Grand Prix GetByYear"))
            {
                _gpLogger.LogInformation("GetByYear");
                return await _dbContext.Set<GrandPrix>().Where(gp => gp.year == year).ToListAsync();
            }
        }
    }
}