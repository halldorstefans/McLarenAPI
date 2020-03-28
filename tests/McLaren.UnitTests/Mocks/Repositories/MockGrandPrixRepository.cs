using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using McLaren.Core.Entities;
using McLaren.Core.Interfaces.Repositories;
using Moq;

namespace McLaren.UnitTests.Mocks.Repositories
{
    public class MockGrandPrixRepository : Mock<IGrandPrixRepository>
    {
        public MockGrandPrixRepository MockGetAll(Task<IEnumerable<GrandPrix>> GrandPrixs)
        {
            Setup(x => x.GetAll()).Returns(GrandPrixs);

            return this;
        }
        public MockGrandPrixRepository MockGetById(Task<GrandPrix> GrandPrix)
        {
            Setup(x => x.Get(It.IsAny<int>())).Returns(GrandPrix);

            return this;
        }

        public MockGrandPrixRepository MockGetByYear(Task<IEnumerable<GrandPrix>> GrandPrix)
        {
            Setup(x => x.Find(It.IsAny<Expression<Func<GrandPrix, bool>>>())).Returns(GrandPrix);

            return this;
        }

        public MockGrandPrixRepository VerifyGetAllForGrandPrix(Times times)
        {
            Verify(x => x.GetAll(), times);

            return this;
        }

        public MockGrandPrixRepository VerifyGetByIdForGrandPrix(Times times)
        {
            Verify(x => x.Get(It.IsAny<int>()), times);

            return this;
        }

        public MockGrandPrixRepository VerifyGetByYearForGrandPrix(Times times)
        {
            Verify(x => x.Find(It.IsAny<Expression<Func<GrandPrix, bool>>>()), times);

            return this;
        }
    }
}