using System.Collections.Generic;
using System.Threading.Tasks;
using McLaren.Core.Models;

namespace McLaren.Core.Interfaces
{
    public interface IDriverService
    {
        Task<DriverDto> GetById(int id);
        Task<IEnumerable<DriverDto>> GetByLastName(string lastName);
        Task<IEnumerable<DriverDto>> GetAll();
    }
}