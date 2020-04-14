using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using McLaren.Core.Entities;
using McLaren.Core.Interfaces.Repositories;
using Moq;

namespace McLaren.UnitTests.Mocks.Repositories
{
    public class MockCarRepository : Mock<ICarsRepository>
    {
        public MockCarRepository MockGetAll(Task<IEnumerable<Car>> car)
        {
            Setup(x => x.GetAll()).Returns(car);

            return this;
        }

        public MockCarRepository MockGetById(Task<Car> car)
        {
            Setup(x => x.Get(It.IsAny<int>())).Returns(car);

            return this;
        }

        public MockCarRepository MockGetByYear(Task<IEnumerable<Car>> car)
        {
            Setup(x => x.GetByYear(It.IsAny<int>())).Returns(car);

            return this;
        }

        public MockCarRepository MockGetByName(Task<IEnumerable<Car>> car)
        {
            Setup(x => x.GetByName(It.IsAny<string>())).Returns(car);

            return this;
        }

        public MockCarRepository VerifyGetAllForCar(Times times)
        {
            Verify(x => x.GetAll(), times);

            return this;
        }

        public MockCarRepository VerifyGetByIdForCar(Times times)
        {
            Verify(x => x.Get(It.IsAny<int>()), times);

            return this;
        }

        public MockCarRepository VerifyGetByYearForCar(Times times)
        {
            Verify(x => x.GetByYear(It.IsAny<int>()), times);

            return this;
        }

        public MockCarRepository VerifyGetByNameForCar(Times times)
        {
            Verify(x => x.GetByName(It.IsAny<string>()), times);

            return this;
        }
    }
}