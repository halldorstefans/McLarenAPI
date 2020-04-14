using McLaren.Core.Services;
using McLaren.UnitTests.Mocks.Repositories;
using McLaren.UnitTests.Mocks.Data;
using Moq;
using Xunit;
using McLaren.Core.ResourceParameters;

namespace McLaren.UnitTests.Core.Services
{
    public class CarsServiceTests
    {
        [Fact]
        public async void CarsService_GetAll_Valid()
        {
            // Arrange
            var mockCar = MockCarData.GetAllEntitiesListAsync();                        
            var mockCarRepo = new MockCarRepository().MockGetAll(mockCar);
            var mockCarLoggerRepo = new MockLoggerRepository<CarsService>();
            var mockCarsService = new CarsService(mockCarRepo.Object, mockCarLoggerRepo.Object);

            // Act
            var cars = await mockCarsService.GetCars();        

            // Assert
            Assert.NotEmpty(cars);
            mockCarRepo.VerifyGetAllForCar(Times.Once());
        }        

        [Fact]
        public async void CarsService_GetAll_NoCars()
        {
            // Arrange
            var mockCar = MockCarData.GetEmptyEntityListAsync();                        
            var mockCarRepo = new MockCarRepository().MockGetAll(mockCar);
            var mockCarLoggerRepo = new MockLoggerRepository<CarsService>();
            var mockCarsService = new CarsService(mockCarRepo.Object, mockCarLoggerRepo.Object);

            // Act
            var cars = await mockCarsService.GetCars();        

            // Assert
            Assert.Empty(cars);
            mockCarRepo.VerifyGetAllForCar(Times.Once());
        }

        [Fact]
        public async void CarsService_GetAllFilter_Valid()
        {
            // Arrange
            var mockCar = MockCarData.GetAllEntitiesListAsync();
            CarsResourceParameters parameters = new CarsResourceParameters{Name = "MCL35", Year = "2020"};                        
            var mockCarFilterRepo = new MockCarRepository().MockGetByYear(mockCar);
            var mockCarLoggerRepo = new MockLoggerRepository<CarsService>();
            var mockCarsService = new CarsService(mockCarFilterRepo.Object, mockCarLoggerRepo.Object);

            // Act
            var cars = await mockCarsService.GetCars(parameters);        

            // Assert
            Assert.NotEmpty(cars);
            mockCarFilterRepo.VerifyGetByNameForCar(Times.Once());
            mockCarFilterRepo.VerifyGetByYearForCar(Times.Once());
        }        

        [Fact]
        public async void CarsService_GetAllFilter_NoCars()
        {
            // Arrange
            var mockCar = MockCarData.GetEmptyEntityListAsync();
            CarsResourceParameters parameters = new CarsResourceParameters{Name = "M2B", Year = "1966"};                        
            var mockCarFilterRepo = new MockCarRepository().MockGetByYear(mockCar);
            var mockCarLoggerRepo = new MockLoggerRepository<CarsService>();
            var mockCarsService = new CarsService(mockCarFilterRepo.Object, mockCarLoggerRepo.Object);

            // Act
            var cars = await mockCarsService.GetCars(parameters);        

            // Assert
            Assert.Empty(cars);
            mockCarFilterRepo.VerifyGetByNameForCar(Times.Once());
            mockCarFilterRepo.VerifyGetByYearForCar(Times.Once());
        }

        [Fact]
        public async void CarsService_GetById_ValidId()
        {
            // Arrange
            var mockId = 2;
            var mockCar = MockCarData.GetSingleEntityAsync();                        
            var mockCarRepo = new MockCarRepository().MockGetById(mockCar);
            var mockCarLoggerRepo = new MockLoggerRepository<CarsService>();
            var mockCarsService = new CarsService(mockCarRepo.Object, mockCarLoggerRepo.Object);

            // Act
            var cars = await mockCarsService.GetCar(mockId);        

            // Assert
            Assert.Equal(mockId, cars.id);
            mockCarRepo.VerifyGetByIdForCar(Times.Once());
        }        

        [Fact]
        public async void CarsService_GetById_NoCars()
        {
            // Arrange
            var mockId = 5;
            var mockCar = MockCarData.GetSingleEmptyEntityAsync();                        
            var mockCarRepo = new MockCarRepository().MockGetById(mockCar);
            var mockCarLoggerRepo = new MockLoggerRepository<CarsService>();
            var mockCarsService = new CarsService(mockCarRepo.Object, mockCarLoggerRepo.Object);
            
            // Act
            var cars = await mockCarsService.GetCar(mockId);        

            // Assert
            Assert.Null(cars);
            mockCarRepo.VerifyGetByIdForCar(Times.Once());
        }    
    }
}