using McLaren.Core.Interfaces;
using McLaren.Core.Models;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace McLaren.UnitTests.Mocks.Services
{
    public class MockGrandPrixService : Mock<IGrandPrixService>
    {
        public MockGrandPrixService MockGetByYear(Task<IEnumerable<GrandPrixDto>> grandPrixDto)
        {
            Setup(x => x.GetByYear(It.IsAny<int>())).Returns(grandPrixDto);

            return this;
        }

        public MockGrandPrixService MockGetAll(Task<IEnumerable<GrandPrixDto>> grandPrixDto)
        {
            Setup(x => x.GetAll()).Returns(grandPrixDto);

            return this;
        }

        public MockGrandPrixService VerifyGetByYear(Times times)
        {
            Verify(x => x.GetByYear(It.IsAny<int>()), times);

            return this;
        }
        public MockGrandPrixService VerifyGetAll(Times times)
        {
            Verify(x => x.GetAll(), times);

            return this;
        }
    }
}