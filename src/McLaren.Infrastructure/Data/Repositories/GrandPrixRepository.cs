using McLaren.Core.Entities;
using McLaren.Core.Interfaces.Repositories;

namespace McLaren.Infrastructure.Data.Repositories
{
    public class GrandPrixRepository : Repository<GrandPrix>, IGrandPrixRepository
    {
        public GrandPrixRepository(McLarenContext dbContext) : base(dbContext)
        {
        }

    }
}