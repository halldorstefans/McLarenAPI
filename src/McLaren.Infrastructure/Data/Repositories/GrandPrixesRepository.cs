using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using McLaren.Core.Entities;
using McLaren.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace McLaren.Infrastructure.Data.Repositories
{
    public class GrandPrixesRepository : Repository<GrandPrix>, IGrandPrixesRepository
    {
        public GrandPrixesRepository(McLarenContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<GrandPrix>> GetByRaceId(int raceId)
        {
            return await _dbContext.Set<GrandPrix>().Where(gp => gp.raceid == raceId).ToListAsync();
        }
        
        public async Task<IEnumerable<GrandPrix>> GetByCountry(string country)
        {
            return await _dbContext.Set<GrandPrix>().Where(gp => gp.country == country).ToListAsync();
        }

        public async Task<IEnumerable<GrandPrix>> GetByYear(int year)
        {
            return await _dbContext.Set<GrandPrix>().Where(gp => gp.year == year).ToListAsync();
        }
    }
}