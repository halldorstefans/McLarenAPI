using System.Linq;
using System.Collections.Generic;
using McLaren.Core.Models;
using McLaren.UnitTests.Mocks.Data;
using McLaren.UnitTests.Mocks.Services;
using McLaren.Web.V1.Controller;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using FluentAssertions;

namespace McLaren.UnitTests.Web.Controllers
{
    public class DriverControllerTests
    {
        [Fact]
        public async void DriverController_GetAll_Valid()
        {
            var mockDriver = MockDriverData.GetListDTOAsync();

            var mockDriverService = new MockDriverService().MockGetAll(mockDriver);

            var controller = new DriverController(mockDriverService.Object);

            var result = await controller.Get();

            Assert.IsAssignableFrom<IActionResult>(result);
            mockDriverService.VerifyGetAll(Times.Once());
        }

        [Fact]
        public async void DriverController_GetAll_Empty()
        {
            var mockDriver = MockDriverData.GetEmptyListDTOAsync();

            var mockDriverService = new MockDriverService().MockGetAll(mockDriver);

            var controller = new DriverController(mockDriverService.Object);

            var result = await controller.Get();

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var Driver = okResult.Value.Should().BeAssignableTo<IEnumerable<DriverDto>>().Subject;
            Driver.Count().Should().Be(0);
            mockDriverService.VerifyGetAll(Times.Once());
        }

        [Fact]
        public async void DriverController_GetById_Valid()
        {
            var mockDriverId = 2;
            var mockDriver = MockDriverData.GetSingleDTOAsync();

            var mockDriverService = new MockDriverService().MockGetById(mockDriver);

            var controller = new DriverController(mockDriverService.Object);

            var result = await controller.Get(mockDriverId);

            Assert.IsAssignableFrom<IActionResult>(result);
            mockDriverService.VerifyGetById(Times.Once());
        }

        [Fact]
        public async void DriverController_GetById_Empty()
        {
            var mockDriverId = 10;
            var mockDriver = MockDriverData.GetEmptySingleDTOAsync();

            var mockDriverService = new MockDriverService().MockGetById(mockDriver);

            var controller = new DriverController(mockDriverService.Object);

            var result = await controller.Get(mockDriverId);

            var okResult = result.Should().BeOfType<NotFoundResult>().Subject;
        }

        [Fact]
        public async void DriverController_GetByName_Valid()
        {
            var mockDriverLastName = "Lauda";
            var mockDriver = MockDriverData.GetListDTOAsync();

            var mockDriverService = new MockDriverService().MockGetByLastName(mockDriver);

            var controller = new DriverController(mockDriverService.Object);

            var result = await controller.Get(mockDriverLastName);

            Assert.IsAssignableFrom<IActionResult>(result);
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var Driver = okResult.Value.Should().BeAssignableTo<IEnumerable<DriverDto>>().Subject;
            Driver.Should().NotBeNull();
            mockDriverService.VerifyGetByLastName(Times.Once());
        }

        [Fact]
        public async void DriverController_GetByName_Empty()
        {
            var mockDriverLastName = "Hamilton";
            var mockDriver = MockDriverData.GetEmptyListDTOAsync();

            var mockDriverService = new MockDriverService().MockGetByLastName(mockDriver);

            var controller = new DriverController(mockDriverService.Object);

            var result = await controller.Get(mockDriverLastName);

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var Driver = okResult.Value.Should().BeAssignableTo<IEnumerable<DriverDto>>().Subject;
            Driver.Count().Should().Be(0);
            mockDriverService.VerifyGetByLastName(Times.Once());
        }
    }
}