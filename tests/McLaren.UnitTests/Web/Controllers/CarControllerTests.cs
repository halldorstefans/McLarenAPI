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
    public class CarControllerTests
    {
        [Fact]
        public async void CarController_GetAll_Valid()
        {
            var mockCar = MockCarData.GetListDTOAsync();

            var mockCarService = new MockCarService().MockGetAll(mockCar);

            var controller = new CarController(mockCarService.Object);

            var result = await controller.Get();

            Assert.IsAssignableFrom<IActionResult>(result);
            mockCarService.VerifyGetAll(Times.Once());
        }

        [Fact]
        public async void CarController_GetAll_Empty()
        {
            var mockCar = MockCarData.GetEmptyListDTOAsync();

            var mockCarService = new MockCarService().MockGetAll(mockCar);

            var controller = new CarController(mockCarService.Object);

            var result = await controller.Get();

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var car = okResult.Value.Should().BeAssignableTo<IEnumerable<CarDto>>().Subject;
            car.Count().Should().Be(0);
            mockCarService.VerifyGetAll(Times.Once());
        }

        [Fact]
        public async void CarController_GetByYear_Valid()
        {
            var mockCarYear = 2020;
            var mockCar = MockCarData.GetListDTOAsync();

            var mockCarService = new MockCarService().MockGetByYear(mockCar);

            var controller = new CarController(mockCarService.Object);

            var result = await controller.Get(mockCarYear);

            Assert.IsAssignableFrom<IActionResult>(result);
            mockCarService.VerifyGetByYear(Times.Once());
        }

        [Fact]
        public async void CarController_GetByYear_Empty()
        {
            var mockCarYear = 2020;
            var mockCar = MockCarData.GetEmptyListDTOAsync();

            var mockCarService = new MockCarService().MockGetByYear(mockCar);

            var controller = new CarController(mockCarService.Object);

            var result = await controller.Get(mockCarYear);

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var car = okResult.Value.Should().BeAssignableTo<IEnumerable<CarDto>>().Subject;
            car.Count().Should().Be(0);
            mockCarService.VerifyGetByYear(Times.Once());
        }

        [Fact]
        public async void CarController_GetByName_Valid()
        {
            var mockCarName = "MCL35";
            var mockCar = MockCarData.GetSingleDTOAsync();

            var mockCarService = new MockCarService().MockGetByName(mockCar);

            var controller = new CarController(mockCarService.Object);

            var result = await controller.Get(mockCarName);

            Assert.IsAssignableFrom<IActionResult>(result);
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var car = okResult.Value.Should().BeAssignableTo<CarDto>().Subject;
            car.Should().NotBeNull();
            mockCarService.VerifyGetByName(Times.Once());
        }

        [Fact]
        public async void CarController_GetByName_Empty()
        {
            var mockCarName = "M7A";
            var mockCar = MockCarData.GetEmptySingleDTOAsync();

            var mockCarService = new MockCarService().MockGetByName(mockCar);

            var controller = new CarController(mockCarService.Object);

            var result = await controller.Get(mockCarName);

            var okResult = result.Should().BeOfType<NotFoundResult>().Subject;            
        }
    }
}