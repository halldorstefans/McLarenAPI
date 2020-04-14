using McLaren.Core.Interfaces;
using McLaren.Core.Models;
using McLaren.Core.ResourceParameters;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace McLaren.UnitTests.Mocks.Services
{
    public class MockGrandPrixService : Mock<IGrandPrixesService>
    {
        public MockGrandPrixService MockGetById(Task<IEnumerable<GrandPrixDto>> grandPrixDto)
        {
            Setup(x => x.GetGrandPrix(It.IsAny<int>())).Returns(grandPrixDto);

            return this;
        }

        public MockGrandPrixService MockGetAll(Task<IEnumerable<GrandPrixDto>> grandPrixDto)
        {
            Setup(x => x.GetGrandPrixes(It.IsAny<GrandPrixesResourceParameters>())).Returns(grandPrixDto);

            return this;
        }

        public MockGrandPrixService VerifyGetById(Times times)
        {
            Verify(x => x.GetGrandPrix(It.IsAny<int>()), times);

            return this;
        }
        public MockGrandPrixService VerifyGetAll(Times times)
        {
            Verify(x => x.GetGrandPrixes(It.IsAny<GrandPrixesResourceParameters>()), times);

            return this;
        }
    }
}