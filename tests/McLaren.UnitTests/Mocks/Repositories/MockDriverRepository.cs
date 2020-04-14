using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using McLaren.Core.Entities;
using McLaren.Core.Interfaces.Repositories;
using Moq;

namespace McLaren.UnitTests.Mocks.Repositories
{
    public class MockDriverRepository : Mock<IDriversRepository>
    {
        public MockDriverRepository MockGetAll(Task<IEnumerable<Driver>> drivers)
        {
            Setup(x => x.GetAll()).Returns(drivers);

            return this;
        }
        public MockDriverRepository MockGetById(Task<Driver> driver)
        {
            Setup(x => x.Get(It.IsAny<int>())).Returns(driver);

            return this;
        }

        public MockDriverRepository MockGetByName(Task<IEnumerable<Driver>> driver)
        {
            Setup(x => x.GetByName(It.IsAny<string>())).Returns(driver);

            return this;
        }

        public MockDriverRepository VerifyGetAllForDriver(Times times)
        {
            Verify(x => x.GetAll(), times);

            return this;
        }

        public MockDriverRepository VerifyGetByIdForDriver(Times times)
        {
            Verify(x => x.Get(It.IsAny<int>()), times);

            return this;
        }

        public MockDriverRepository VerifyGetByNameForDriver(Times times)
        {
            Verify(x => x.GetByName(It.IsAny<string>()), times);

            return this;
        }
    }
}