using McLaren.Core.Interfaces;
using McLaren.Core.Models;
using McLaren.Core.ResourceParameters;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace McLaren.UnitTests.Mocks.Services
{
    public class MockDriverService : Mock<IDriversService>
    {
        public MockDriverService MockGetById(Task<DriverDto> driverDto)
        {
            Setup(x => x.GetDriver(It.IsAny<int>())).Returns(driverDto);

            return this;
        }

        public MockDriverService MockGetAll(Task<IEnumerable<DriverDto>> driverDto)
        {
            Setup(x => x.GetDrivers(It.IsAny<DriversResourceParameters>())).Returns(driverDto);

            return this;
        }

        public MockDriverService VerifyGetById(Times times)
        {
            Verify(x => x.GetDriver(It.IsAny<int>()), times);

            return this;
        }
        public MockDriverService VerifyGetAll(Times times)
        {
            Verify(x => x.GetDrivers(It.IsAny<DriversResourceParameters>()), times);

            return this;
        }
    }
}