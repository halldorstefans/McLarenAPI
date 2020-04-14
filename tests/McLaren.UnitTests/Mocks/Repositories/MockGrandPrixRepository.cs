using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using McLaren.Core.Entities;
using McLaren.Core.Interfaces.Repositories;
using Moq;

namespace McLaren.UnitTests.Mocks.Repositories
{
    public class MockGrandPrixRepository : Mock<IGrandPrixesRepository>
    {
        public MockGrandPrixRepository MockGetAll(Task<IEnumerable<GrandPrix>> GrandPrix)
        {
            Setup(x => x.GetAll()).Returns(GrandPrix);

            return this;
        }
        public MockGrandPrixRepository MockGetById(Task<IEnumerable<GrandPrix>> GrandPrix)
        {
            Setup(x => x.GetByRaceId(It.IsAny<int>())).Returns(GrandPrix);

            return this;
        }

        public MockGrandPrixRepository MockGetByYear(Task<IEnumerable<GrandPrix>> GrandPrix)
        {
            Setup(x => x.GetByYear(It.IsAny<int>())).Returns(GrandPrix);

            return this;
        }

        public MockGrandPrixRepository MockGetByCountry(Task<IEnumerable<GrandPrix>> GrandPrix)
        {
            Setup(x => x.GetByCountry(It.IsAny<string>())).Returns(GrandPrix);

            return this;
        }

        public MockGrandPrixRepository VerifyGetAllForGrandPrix(Times times)
        {
            Verify(x => x.GetAll(), times);

            return this;
        }

        public MockGrandPrixRepository VerifyGetByIdForGrandPrix(Times times)
        {
            Verify(x => x.GetByRaceId(It.IsAny<int>()), times);

            return this;
        }

        public MockGrandPrixRepository VerifyGetByYearForGrandPrix(Times times)
        {
            Verify(x => x.GetByYear(It.IsAny<int>()), times);

            return this;
        }

        public MockGrandPrixRepository VerifyGetByCountryForGrandPrix(Times times)
        {
            Verify(x => x.GetByCountry(It.IsAny<string>()), times);

            return this;
        }
    }
}