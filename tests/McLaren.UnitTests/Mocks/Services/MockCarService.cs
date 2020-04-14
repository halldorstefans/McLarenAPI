using McLaren.Core.Interfaces;
using McLaren.Core.Models;
using McLaren.Core.ResourceParameters;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace McLaren.UnitTests.Mocks.Services
{
    public class MockCarService : Mock<ICarsService>
    {
        public MockCarService MockGetById(Task<CarDto> carDto)
        {
            Setup(x => x.GetCar(It.IsAny<int>())).Returns(carDto);

            return this;
        }

        public MockCarService MockGetAll(Task<IEnumerable<CarDto>> carDto)
        {
            Setup(x => x.GetCars(It.IsAny<CarsResourceParameters>())).Returns(carDto);

            return this;
        }

        public MockCarService VerifyGetById(Times times)
        {
            Verify(x => x.GetCar(It.IsAny<int>()), times);

            return this;
        }
        public MockCarService VerifyGetAll(Times times)
        {
            Verify(x => x.GetCars(It.IsAny<CarsResourceParameters>()), times);

            return this;
        }
    }
}