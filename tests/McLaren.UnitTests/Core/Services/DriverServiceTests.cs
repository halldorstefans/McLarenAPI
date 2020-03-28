using McLaren.Core.Services;
using McLaren.UnitTests.Mocks.Repositories;
using McLaren.UnitTests.Mocks.Data;
using Moq;
using Xunit;

namespace McLaren.UnitTests.Core.Services
{
    public class DriverServiceTests
    {
        [Fact]
        public async void DriverService_GetAll_Valid()
        {
            var mockDriver = MockDriverData.GetListAsync();
                        
            var mockDriverRepo = new MockDriverRepository().MockGetAll(mockDriver);
            var mockDriverLoggerRepo = new MockLoggerRepository<DriverService>();

            var mockDriverService = new DriverService(mockDriverRepo.Object, mockDriverLoggerRepo.Object);

            var drivers = await mockDriverService.GetAll();        

            Assert.NotEmpty(drivers);
            mockDriverRepo.VerifyGetAllForDriver(Times.Once());
        }        

        [Fact]
        public async void DriverService_GetAll_NoDrivers()
        {
            var mockDriver = MockDriverData.GetEmptyListAsync();
                        
            var mockDriverRepo = new MockDriverRepository().MockGetAll(mockDriver);
            var mockDriverLoggerRepo = new MockLoggerRepository<DriverService>();

            var mockDriverService = new DriverService(mockDriverRepo.Object, mockDriverLoggerRepo.Object);

            var drivers = await mockDriverService.GetAll();        

            Assert.Empty(drivers);
            mockDriverRepo.VerifyGetAllForDriver(Times.Once());
        }

        [Fact]
        public async void DriverService_GetById_ValidId()
        {
            var mockId = 2;
            var mockDriver = MockDriverData.GetSingleAsync();
                        
            var mockDriverRepo = new MockDriverRepository().MockGetById(mockDriver);
            var mockDriverLoggerRepo = new MockLoggerRepository<DriverService>();

            var mockDriverService = new DriverService(mockDriverRepo.Object, mockDriverLoggerRepo.Object);

            var driver = await mockDriverService.GetById(mockId);        

            Assert.Equal(mockId, driver.id);
            mockDriverRepo.VerifyGetByIdForDriver(Times.Once());
        }        

        [Fact]
        public async void DriverService_GetById_NoDrivers()
        {
            var mockId = 1;
            var mockDriver = MockDriverData.GetSingleAsync();
                        
            var mockDriverRepo = new MockDriverRepository().MockGetById(mockDriver);
            var mockDriverLoggerRepo = new MockLoggerRepository<DriverService>();

            var mockDriverService = new DriverService(mockDriverRepo.Object, mockDriverLoggerRepo.Object);

            var driver = await mockDriverService.GetById(mockId);        

            Assert.NotEqual(mockId, driver.id);
                       
            mockDriverRepo.VerifyGetByIdForDriver(Times.Once());
        }    

        [Fact]
        public async void DriverService_GetByName_ValidName()
        {
            var mockDriverName = "Lauda";
            var mockDriver = MockDriverData.GetListAsync();
                        
            var mockDriverRepo = new MockDriverRepository().MockGetByName(mockDriver);
            var mockDriverLoggerRepo = new MockLoggerRepository<DriverService>();

            var mockDriverService = new DriverService(mockDriverRepo.Object, mockDriverLoggerRepo.Object);

            var drivers = await mockDriverService.GetByLastName(mockDriverName);        

            Assert.NotEmpty(drivers);
            mockDriverRepo.VerifyGetByNameForDriver(Times.Once());
        }        

        [Fact]
        public async void DriverService_GetByName_NoDrivers()
        {
            var mockDriverName = "Hamilton";
            var mockDriver = MockDriverData.GetListAsync();
                        
            var mockDriverRepo = new MockDriverRepository().MockGetByName(mockDriver);
            var mockDriverLoggerRepo = new MockLoggerRepository<DriverService>();

            var mockDriverService = new DriverService(mockDriverRepo.Object, mockDriverLoggerRepo.Object);

            var drivers = await mockDriverService.GetByLastName(mockDriverName);        

            foreach (var driver in drivers)
            {
                Assert.NotEqual(mockDriverName, driver.lastName);
            } 
            mockDriverRepo.VerifyGetByNameForDriver(Times.Once());
        }    
    }
}