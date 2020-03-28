using Microsoft.Extensions.Logging;
using Moq;

namespace McLaren.UnitTests.Mocks.Repositories
{
    public class MockLoggerRepository<T> : Mock<ILogger<T>> where T : class
    {
    }
}