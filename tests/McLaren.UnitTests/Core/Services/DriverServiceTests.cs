using McLaren.Core.Services;
using McLaren.UnitTests.Mocks.Repositories;
using McLaren.UnitTests.Mocks.Data;
using Moq;
using Xunit;
using McLaren.Core.ResourceParameters;

namespace McLaren.UnitTests.Core.Services
{
    public class DriversServiceTests
    {
        [Fact]
        public async void DriversService_GetAll_Valid()
        {
            // Arrange
            var mockDriver = MockDriverData.GetAllEntitiesListAsync();                        
            var mockDriverRepo = new MockDriverRepository().MockGetAll(mockDriver);
            var mockDriverLoggerRepo = new MockLoggerRepository<DriversService>();
            var mockDriversService = new DriversService(mockDriverRepo.Object, mockDriverLoggerRepo.Object);

            // Act
            var drivers = await mockDriversService.GetDrivers();        

            // Assert
            Assert.NotEmpty(drivers);
            mockDriverRepo.VerifyGetAllForDriver(Times.Once());
        }        

        [Fact]
        public async void DriversService_GetAll_NoDrivers()
        {
            // Arrange
            var mockDriver = MockDriverData.GetEmptyEntityListAsync();                        
            var mockDriverRepo = new MockDriverRepository().MockGetAll(mockDriver);
            var mockDriverLoggerRepo = new MockLoggerRepository<DriversService>();
            var mockDriversService = new DriversService(mockDriverRepo.Object, mockDriverLoggerRepo.Object);

            // Act
            var drivers = await mockDriversService.GetDrivers();        

            // Assert
            Assert.Empty(drivers);
            mockDriverRepo.VerifyGetAllForDriver(Times.Once());
        }

        [Fact]
        public async void DriversService_GetAllFilter_Valid()
        {
            // Arrange
            var mockDriver = MockDriverData.GetAllEntitiesListAsync();
            DriversResourceParameters parameters = new DriversResourceParameters{Name = "Niki"};                        
            var mockDriverRepo = new MockDriverRepository().MockGetByName(mockDriver);
            var mockDriverLoggerRepo = new MockLoggerRepository<DriversService>();
            var mockDriversService = new DriversService(mockDriverRepo.Object, mockDriverLoggerRepo.Object);

            // Act
            var drivers = await mockDriversService.GetDrivers(parameters);        

            // Assert
            Assert.NotEmpty(drivers);
            mockDriverRepo.VerifyGetByNameForDriver(Times.Once());
        }        

        [Fact]
        public async void DriversService_GetAllFilter_NoDrivers()
        {
            // Arrange
            var mockDriver = MockDriverData.GetEmptyEntityListAsync();
            DriversResourceParameters parameters = new DriversResourceParameters{Name = "Vettel"};                        
            var mockDriverRepo = new MockDriverRepository().MockGetByName(mockDriver);
            var mockDriverLoggerRepo = new MockLoggerRepository<DriversService>();
            var mockDriversService = new DriversService(mockDriverRepo.Object, mockDriverLoggerRepo.Object);

            // Act
            var drivers = await mockDriversService.GetDrivers(parameters);        

            //Assert
            Assert.Empty(drivers);
            mockDriverRepo.VerifyGetByNameForDriver(Times.Once());
        }

        [Fact]
        public async void DriversService_GetById_ValidId()
        {
            // Arrange
            var mockId = 13;
            var mockDriver = MockDriverData.GetSingleEntityAsync();                        
            var mockDriverRepo = new MockDriverRepository().MockGetById(mockDriver);
            var mockDriverLoggerRepo = new MockLoggerRepository<DriversService>();
            var mockDriversService = new DriversService(mockDriverRepo.Object, mockDriverLoggerRepo.Object);

            // Act
            var driver = await mockDriversService.GetDriver(mockId);        

            // Assert
            Assert.Equal(mockId, driver.id);
            mockDriverRepo.VerifyGetByIdForDriver(Times.Once());
        }        

        [Fact]
        public async void DriversService_GetById_NoDrivers()
        {
            // Arrange
            var mockId = 1;
            var mockDriver = MockDriverData.GetSingleEmptyEntityAsync();                        
            var mockDriverRepo = new MockDriverRepository().MockGetById(mockDriver);
            var mockDriverLoggerRepo = new MockLoggerRepository<DriversService>();
            var mockDriversService = new DriversService(mockDriverRepo.Object, mockDriverLoggerRepo.Object);

            // Act
            var driver = await mockDriversService.GetDriver(mockId);        

            // Assert
            Assert.Null(driver); 
            mockDriverRepo.VerifyGetByIdForDriver(Times.Once());
        }        
    }
}