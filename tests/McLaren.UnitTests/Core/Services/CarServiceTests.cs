using McLaren.Core.Services;
using McLaren.UnitTests.Mocks.Repositories;
using McLaren.UnitTests.Mocks.Data;
using Moq;
using Xunit;

namespace McLaren.UnitTests.Core.Services
{
    public class CarServiceTests
    {
        [Fact]
        public async void CarService_GetAll_Valid()
        {
            var mockCar = MockCarData.GetListAsync();
                        
            var mockCarRepo = new MockCarRepository().MockGetAll(mockCar);
            var mockCarLoggerRepo = new MockLoggerRepository<CarService>();

            var mockCarService = new CarService(mockCarRepo.Object, mockCarLoggerRepo.Object);

            var cars = await mockCarService.GetAll();        

            Assert.NotEmpty(cars);
            mockCarRepo.VerifyGetAllForCar(Times.Once());
        }        

        [Fact]
        public async void CarService_GetAll_NoCars()
        {
            var mockCar = MockCarData.GetEmptyListAsync();
                        
            var mockCarRepo = new MockCarRepository().MockGetAll(mockCar);
            var mockCarLoggerRepo = new MockLoggerRepository<CarService>();

            var mockCarService = new CarService(mockCarRepo.Object, mockCarLoggerRepo.Object);

            var cars = await mockCarService.GetAll();        

            Assert.Empty(cars);
            mockCarRepo.VerifyGetAllForCar(Times.Once());
        }

        [Fact]
        public async void CarService_GetByYear_ValidYear()
        {
            var mockYear = 2020;
            var mockCar = MockCarData.GetListAsync();
                        
            var mockCarRepo = new MockCarRepository().MockGetByYear(mockCar);
            var mockCarLoggerRepo = new MockLoggerRepository<CarService>();

            var mockCarService = new CarService(mockCarRepo.Object, mockCarLoggerRepo.Object);

            var cars = await mockCarService.GetByYear(mockYear);        

            Assert.NotEmpty(cars);
            mockCarRepo.VerifyGetByYearForCar(Times.Once());
        }        

        [Fact]
        public async void CarService_GetByYear_NoCars()
        {
            var mockYear = 1980;
            var mockCar = MockCarData.GetListAsync();
                        
            var mockCarRepo = new MockCarRepository().MockGetByYear(mockCar);
            var mockCarLoggerRepo = new MockLoggerRepository<CarService>();

            var mockCarService = new CarService(mockCarRepo.Object, mockCarLoggerRepo.Object);

            var cars = await mockCarService.GetByYear(mockYear);        

            foreach (var car in cars)
            {
                Assert.NotEqual(mockYear, car.fromYear);
            }            
            mockCarRepo.VerifyGetByYearForCar(Times.Once());
        }    

        [Fact]
        public async void CarService_GetByName_ValidName()
        {
            var mockCarName = "MCL35";
            var mockCar = MockCarData.GetListAsync();
                        
            var mockCarRepo = new MockCarRepository().MockGetByName(mockCar);
            var mockCarLoggerRepo = new MockLoggerRepository<CarService>();

            var mockCarService = new CarService(mockCarRepo.Object, mockCarLoggerRepo.Object);

            var cars = await mockCarService.GetByName(mockCarName);        

            Assert.Equal(mockCarName, cars.name);
            mockCarRepo.VerifyGetByNameForCar(Times.Once());
        }        

        [Fact]
        public async void CarService_GetByName_NoCars()
        {
            var mockCarName = "M7C";
            var mockCar = MockCarData.GetListAsync();
                        
            var mockCarRepo = new MockCarRepository().MockGetByName(mockCar);
            var mockCarLoggerRepo = new MockLoggerRepository<CarService>();

            var mockCarService = new CarService(mockCarRepo.Object, mockCarLoggerRepo.Object);

            var cars = await mockCarService.GetByName(mockCarName);        

            Assert.NotEqual(mockCarName, cars.name);
            mockCarRepo.VerifyGetByNameForCar(Times.Once());
        }    
    }
}