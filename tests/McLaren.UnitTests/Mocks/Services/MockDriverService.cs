using McLaren.Core.Interfaces;
using McLaren.Core.Models;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace McLaren.UnitTests.Mocks.Services
{
    public class MockDriverService : Mock<IDriverService>
    {
        public MockDriverService MockGetById(Task<DriverDto> driverDto)
        {
            Setup(x => x.GetById(It.IsAny<int>())).Returns(driverDto);

            return this;
        }

        public MockDriverService MockGetByLastName(Task<IEnumerable<DriverDto>> driverDto)
        {
            Setup(x => x.GetByLastName(It.IsAny<string>())).Returns(driverDto);

            return this;
        }

        public MockDriverService MockGetAll(Task<IEnumerable<DriverDto>> driverDto)
        {
            Setup(x => x.GetAll()).Returns(driverDto);

            return this;
        }

        public MockDriverService VerifyGetByLastName(Times times)
        {
            Verify(x => x.GetByLastName(It.IsAny<string>()), times);

            return this;
        }
        public MockDriverService VerifyGetById(Times times)
        {
            Verify(x => x.GetById(It.IsAny<int>()), times);

            return this;
        }
        public MockDriverService VerifyGetAll(Times times)
        {
            Verify(x => x.GetAll(), times);

            return this;
        }
    }
}