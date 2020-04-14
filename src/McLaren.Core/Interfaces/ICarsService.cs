using System.Collections.Generic;
using System.Threading.Tasks;
using McLaren.Core.Models;
using McLaren.Core.ResourceParameters;

namespace McLaren.Core.Interfaces
{
    public interface ICarsService
    {
        Task<CarDto> GetCar(int id);
        Task<IEnumerable<CarDto>> GetCars();
        Task<IEnumerable<CarDto>> GetCars(CarsResourceParameters carsResourceParameters);
    }
}