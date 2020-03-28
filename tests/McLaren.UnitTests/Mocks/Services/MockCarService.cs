using McLaren.Core.Interfaces;
using McLaren.Core.Models;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace McLaren.UnitTests.Mocks.Services
{
    public class MockCarService : Mock<ICarService>
    {
        public MockCarService MockGetByName(Task<CarDto> carDto)
        {
            Setup(x => x.GetByName(It.IsAny<string>())).Returns(carDto);

            return this;
        }

        public MockCarService MockGetByYear(Task<IEnumerable<CarDto>> carDto)
        {
            Setup(x => x.GetByYear(It.IsAny<int>())).Returns(carDto);

            return this;
        }

        public MockCarService MockGetAll(Task<IEnumerable<CarDto>> carDto)
        {
            Setup(x => x.GetAll()).Returns(carDto);

            return this;
        }

        public MockCarService VerifyGetByName(Times times)
        {
            Verify(x => x.GetByName(It.IsAny<string>()), times);

            return this;
        }
        public MockCarService VerifyGetByYear(Times times)
        {
            Verify(x => x.GetByYear(It.IsAny<int>()), times);

            return this;
        }
        public MockCarService VerifyGetAll(Times times)
        {
            Verify(x => x.GetAll(), times);

            return this;
        }
    }
}