using System.Linq;
using System.Collections.Generic;
using McLaren.Core.Models;
using McLaren.UnitTests.Mocks.Data;
using McLaren.UnitTests.Mocks.Services;
using McLaren.Web.V0_9.Controller;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using FluentAssertions;
using McLaren.Core.ResourceParameters;
using Microsoft.Extensions.Logging;

namespace McLaren.UnitTests.Web.Controllers
{
    public class CarsControllerTests
    {
        [Fact]
        public async void CarsController_GetAll_Valid()
        {
            // Arrange
            var mockCar = MockCarData.GetAllModelListAsync();
            CarsResourceParameters parameters = new CarsResourceParameters{};
            var mockCarService = new MockCarService().MockGetAll(mockCar);
            var mockLogging = new Mock<ILogger<CarsController>>();
            var controller = new CarsController(mockCarService.Object, mockLogging.Object);

            // Act
            var result = await controller.Get(parameters);

            // Assert
            Assert.IsAssignableFrom<IActionResult>(result);
            mockCarService.VerifyGetAll(Times.Once());
        }

        [Fact]
        public async void CarsController_GetAll_Empty()
        {
            // Arrange
            var mockCar = MockCarData.GetEmptyModelListAsync();
            CarsResourceParameters parameters = new CarsResourceParameters{};
            var mockCarService = new MockCarService().MockGetAll(mockCar);
            var mockLogging = new Mock<ILogger<CarsController>>();
            var controller = new CarsController(mockCarService.Object, mockLogging.Object);

            // Act
            var result = await controller.Get(parameters);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var car = okResult.Value.Should().BeAssignableTo<IEnumerable<CarDto>>().Subject;
            car.Count().Should().Be(0);
            mockCarService.VerifyGetAll(Times.Once());
        }

        [Fact]
        public async void CarsController_GetAllFilter_Valid()
        {
            // Arrange
            var mockCar = MockCarData.GetAllModelListAsync();
            CarsResourceParameters parameters = new CarsResourceParameters{Name = "MCL35", Year = "2020"};
            var mockCarService = new MockCarService().MockGetAll(mockCar);
            var mockLogging = new Mock<ILogger<CarsController>>();
            var controller = new CarsController(mockCarService.Object, mockLogging.Object);

            // Act
            var result = await controller.Get(parameters);

            // Assert
            Assert.IsAssignableFrom<IActionResult>(result);
            mockCarService.VerifyGetAll(Times.Once());
        }

        [Fact]
        public async void CarsController_GetAllFilter_Empty()
        {
            // Arrange
            var mockCar = MockCarData.GetEmptyModelListAsync();
            CarsResourceParameters parameters = new CarsResourceParameters{Name = "M2B", Year = "1966"};
            var mockCarService = new MockCarService().MockGetAll(mockCar);
            var mockLogging = new Mock<ILogger<CarsController>>();
            var controller = new CarsController(mockCarService.Object, mockLogging.Object);

            // Act
            var result = await controller.Get(parameters);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var car = okResult.Value.Should().BeAssignableTo<IEnumerable<CarDto>>().Subject;
            car.Count().Should().Be(0);
            mockCarService.VerifyGetAll(Times.Once());
        }

        [Fact]
        public async void CarsController_GetById_Valid()
        {
            // Arrange
            var mockCarYear = 2020;
            var mockCar = MockCarData.GetSingleModelAsync();
            var mockCarService = new MockCarService().MockGetById(mockCar);
            var mockLogging = new Mock<ILogger<CarsController>>();
            var controller = new CarsController(mockCarService.Object, mockLogging.Object);
            
            // Act
            var result = await controller.Get(mockCarYear);

            // Assert
            Assert.IsAssignableFrom<IActionResult>(result);
            mockCarService.VerifyGetById(Times.Once());
        }

        [Fact]
        public async void CarsController_GetById_Empty()
        {
            // Arrange
            var mockCarYear = 1980;
            var mockCar = MockCarData.GetSingleEmptyModelAsync();
            var mockCarService = new MockCarService().MockGetById(mockCar);
            var mockLogging = new Mock<ILogger<CarsController>>();
            var controller = new CarsController(mockCarService.Object, mockLogging.Object);

            // Act
            var result = await controller.Get(mockCarYear);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
            mockCarService.VerifyGetById(Times.Once());
        }

    }
}