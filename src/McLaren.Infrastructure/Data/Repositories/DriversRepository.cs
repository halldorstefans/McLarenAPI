using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using McLaren.Core.Entities;
using McLaren.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace McLaren.Infrastructure.Data.Repositories
{
    public class DriversRepository : Repository<Driver>, IDriversRepository
    {
        public DriversRepository(McLarenContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Driver>> GetByName(string name)
        {
            return await _dbContext.Set<Driver>().Where(d => d.firstName.Contains(name) || d.lastName.Contains(name)).ToListAsync();
        }
    }
}