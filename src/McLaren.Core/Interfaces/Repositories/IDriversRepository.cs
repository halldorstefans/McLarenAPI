using System.Collections.Generic;
using System.Threading.Tasks;
using McLaren.Core.Entities;

namespace McLaren.Core.Interfaces.Repositories
{
    public interface IDriversRepository : IRepository<Driver>
    {
        Task<IEnumerable<Driver>> GetByName(string name);
    }
}