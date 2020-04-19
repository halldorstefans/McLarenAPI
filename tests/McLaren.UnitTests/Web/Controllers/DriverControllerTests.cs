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

namespace McLaren.UnitTests.Web.Controllers
{
    public class DriversControllerTests
    {
        [Fact]
        public async void DriversController_GetAll_Valid()
        {
            // Arrange
            var mockDriver = MockDriverData.GetAllModelListAsync();
            DriversResourceParameters parameters = new DriversResourceParameters{};
            var mockDriverService = new MockDriverService().MockGetAll(mockDriver);
            var controller = new DriversController(mockDriverService.Object);

            // Act
            var result = await controller.Get(parameters);

            // Assert
            Assert.IsAssignableFrom<IActionResult>(result);
            mockDriverService.VerifyGetAll(Times.Once());
        }

        [Fact]
        public async void DriversController_GetAll_Empty()
        {
            // Arrange
            var mockDriver = MockDriverData.GetEmptyModelListAsync();
            DriversResourceParameters parameters = new DriversResourceParameters{};
            var mockDriverService = new MockDriverService().MockGetAll(mockDriver);
            var controller = new DriversController(mockDriverService.Object);

            // Act
            var result = await controller.Get(parameters);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var Driver = okResult.Value.Should().BeAssignableTo<IEnumerable<DriverDto>>().Subject;
            Driver.Count().Should().Be(0);
            mockDriverService.VerifyGetAll(Times.Once());
        }

        [Fact]
        public async void DriversController_GetAllFilter_Valid()
        {
            // Arrange
            var mockDriver = MockDriverData.GetEmptyModelListAsync();
            DriversResourceParameters parameters = new DriversResourceParameters{Name = "Niki"};
            var mockDriverService = new MockDriverService().MockGetAll(mockDriver);
            var controller = new DriversController(mockDriverService.Object);
            
            // Act
            var result = await controller.Get(parameters);

            // Assert
            Assert.IsAssignableFrom<IActionResult>(result);
            mockDriverService.VerifyGetAll(Times.Once());
        }

        [Fact]
        public async void DriversController_GetAllFilter_Empty()
        {
            // Arrange
            var mockDriver = MockDriverData.GetEmptyModelListAsync();
            DriversResourceParameters parameters = new DriversResourceParameters{Name = "Vettel"};
            var mockDriverService = new MockDriverService().MockGetAll(mockDriver);
            var controller = new DriversController(mockDriverService.Object);
            
            // Act
            var result = await controller.Get(parameters);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var Driver = okResult.Value.Should().BeAssignableTo<IEnumerable<DriverDto>>().Subject;
            Driver.Count().Should().Be(0);
            mockDriverService.VerifyGetAll(Times.Once());
        }

        [Fact]
        public async void DriversController_GetById_Valid()
        {
            // Arrange
            var mockDriverId = 13;
            var mockDriver = MockDriverData.GetSingleModelAsync();
            var mockDriverService = new MockDriverService().MockGetById(mockDriver);
            var controller = new DriversController(mockDriverService.Object);
            
            // Act
            var result = await controller.Get(mockDriverId);

            // Assert
            Assert.IsAssignableFrom<IActionResult>(result);
            mockDriverService.VerifyGetById(Times.Once());
        }

        [Fact]
        public async void DriversController_GetById_Empty()
        {
            // Arrange
            var mockDriverId = 10;
            var mockDriver = MockDriverData.GetSingleEmptyModelAsync();
            var mockDriverService = new MockDriverService().MockGetById(mockDriver);
            var controller = new DriversController(mockDriverService.Object);
            
            // Act
            var result = await controller.Get(mockDriverId);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}