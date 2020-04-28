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
    public class GrandPrixesControllerTests
    {
        [Fact]
        public async void GrandPrixesController_GetAll_Valid()
        {
            // Arrange
            var mockGrandPrix = MockGrandPrixData.GetAllModelListAsync();
            GrandPrixesResourceParameters parameters = new GrandPrixesResourceParameters{};
            var mockGrandPrixService = new MockGrandPrixService().MockGetAll(mockGrandPrix);
            var mockLogging = new Mock<ILogger<GrandPrixesController>>();
            var controller = new GrandPrixesController(mockGrandPrixService.Object, mockLogging.Object);
            
            // Act
            var result = await controller.Get(parameters);

            // Assert
            Assert.IsAssignableFrom<IActionResult>(result);
            mockGrandPrixService.VerifyGetAll(Times.Once());
        }

        [Fact]
        public async void GrandPrixesController_GetAll_Empty()
        {
            // Arrange
            var mockGrandPrix = MockGrandPrixData.GetEmptyModelListAsync();
            GrandPrixesResourceParameters parameters = new GrandPrixesResourceParameters{};
            var mockGrandPrixService = new MockGrandPrixService().MockGetAll(mockGrandPrix);
            var mockLogging = new Mock<ILogger<GrandPrixesController>>();
            var controller = new GrandPrixesController(mockGrandPrixService.Object, mockLogging.Object);

            // Act
            var result = await controller.Get(parameters);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var GrandPrix = okResult.Value.Should().BeAssignableTo<IEnumerable<GrandPrixDto>>().Subject;
            GrandPrix.Count().Should().Be(0);
            mockGrandPrixService.VerifyGetAll(Times.Once());
        }

        [Fact]
        public async void GrandPrixesController_GetAllFilter_Valid()
        {
            // Arrange
            var mockGrandPrix = MockGrandPrixData.GetAllModelListAsync();
            GrandPrixesResourceParameters parameters = new GrandPrixesResourceParameters{Country = "Spain", Year = "1970"};
            var mockGrandPrixService = new MockGrandPrixService().MockGetAll(mockGrandPrix);
            var mockLogging = new Mock<ILogger<GrandPrixesController>>();
            var controller = new GrandPrixesController(mockGrandPrixService.Object, mockLogging.Object);

            // Act
            var result = await controller.Get(parameters);

            // Assert
            Assert.IsAssignableFrom<IActionResult>(result);
            mockGrandPrixService.VerifyGetAll(Times.Once());
        }

        [Fact]
        public async void GrandPrixesController_GetAllFilter_Empty()
        {
            // Arrange
            var mockGrandPrix = MockGrandPrixData.GetEmptyModelListAsync();
            GrandPrixesResourceParameters parameters = new GrandPrixesResourceParameters{Country = "USA", Year = "2010"};
            var mockGrandPrixService = new MockGrandPrixService().MockGetAll(mockGrandPrix);
            var mockLogging = new Mock<ILogger<GrandPrixesController>>();
            var controller = new GrandPrixesController(mockGrandPrixService.Object, mockLogging.Object);
            
            // Act
            var result = await controller.Get(parameters);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var GrandPrix = okResult.Value.Should().BeAssignableTo<IEnumerable<GrandPrixDto>>().Subject;
            GrandPrix.Count().Should().Be(0);
            mockGrandPrixService.VerifyGetAll(Times.Once());
        }

        [Fact]
        public async void GrandPrixesController_GetById_Valid()
        {
            // Arrange
            var mockGrandPrixId = 15;
            var mockGrandPrix = MockGrandPrixData.GetAllModelListAsync();
            var mockGrandPrixService = new MockGrandPrixService().MockGetById(mockGrandPrix);
            var mockLogging = new Mock<ILogger<GrandPrixesController>>();
            var controller = new GrandPrixesController(mockGrandPrixService.Object, mockLogging.Object);

            // Act
            var result = await controller.Get(mockGrandPrixId);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var GrandPrix = okResult.Value.Should().BeAssignableTo<IEnumerable<GrandPrixDto>>().Subject;
            GrandPrix.Count().Should().Be(1);
            mockGrandPrixService.VerifyGetById(Times.Once());
        }

        [Fact]
        public async void GrandPrixesController_GetById_Empty()
        {
            // Arrange
            var mockGrandPrixId = 5;
            var mockGrandPrix = MockGrandPrixData.GetNullModelListAsync();
            var mockGrandPrixService = new MockGrandPrixService().MockGetById(mockGrandPrix);
            var mockLogging = new Mock<ILogger<GrandPrixesController>>();
            var controller = new GrandPrixesController(mockGrandPrixService.Object, mockLogging.Object);

            // Act
            var result = await controller.Get(mockGrandPrixId);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
            mockGrandPrixService.VerifyGetById(Times.Once());
        }
    }
}