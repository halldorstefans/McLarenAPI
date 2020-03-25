using System.Collections.Generic;
using System.Threading.Tasks;
using McLaren.Core.Entities;
using McLaren.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace McLaren.Infrastructure.Data.Repositories
{
    public class DriverRepository : Repository<Driver>, IDriverRepository
    {
        public DriverRepository(McLarenContext dbContext) : base(dbContext)
        {
        }
    }
}