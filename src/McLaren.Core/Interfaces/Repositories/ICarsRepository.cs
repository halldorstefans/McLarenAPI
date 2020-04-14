using System.Collections.Generic;
using System.Threading.Tasks;
using McLaren.Core.Entities;

namespace McLaren.Core.Interfaces.Repositories
{
    public interface ICarsRepository : IRepository<Car>
    {
        Task<IEnumerable<Car>> GetByName(string name);
        Task<IEnumerable<Car>> GetByYear(int year);
    }
}