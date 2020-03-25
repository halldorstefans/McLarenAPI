using System.Collections.Generic;
using System.Threading.Tasks;
using McLaren.Core.Models;

namespace McLaren.Core.Interfaces
{
    public interface ICarService
    {
        Task<CarDto> GetById(int id);
        Task<CarDto> GetByName(string name);
        Task<IEnumerable<CarDto>> GetByYear(int year);
        Task<IEnumerable<CarDto>> GetAll();
    }
}