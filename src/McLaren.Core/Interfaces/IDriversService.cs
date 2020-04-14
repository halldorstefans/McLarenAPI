using System.Collections.Generic;
using System.Threading.Tasks;
using McLaren.Core.Models;
using McLaren.Core.ResourceParameters;

namespace McLaren.Core.Interfaces
{
    public interface IDriversService
    {
        Task<DriverDto> GetDriver(int id);
        Task<IEnumerable<DriverDto>> GetDrivers();
        Task<IEnumerable<DriverDto>> GetDrivers(DriversResourceParameters driversResourceParameters);
    }
}