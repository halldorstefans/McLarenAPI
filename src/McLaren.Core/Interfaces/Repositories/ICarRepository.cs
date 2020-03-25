using System.Collections.Generic;
using System.Threading.Tasks;
using McLaren.Core.Entities;

namespace McLaren.Core.Interfaces.Repositories
{
    public interface ICarRepository : IRepository<Car>
    {
        Task<IEnumerable<Car>> GetByYear(int year);
    }
}