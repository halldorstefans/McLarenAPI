using System.Collections.Generic;
using System.Threading.Tasks;
using McLaren.Core.Entities;

namespace McLaren.Core.Interfaces.Repositories
{
    public interface IGrandPrixesRepository : IRepository<GrandPrix>
    {
        Task<IEnumerable<GrandPrix>> GetByRaceId(int raceId);
        Task<IEnumerable<GrandPrix>> GetByCountry(string country);
        Task<IEnumerable<GrandPrix>> GetByYear(int year);
    }
}