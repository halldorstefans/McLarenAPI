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
    public class GrandPrixControllerTests
    {
        [Fact]
        public async void GrandPrixController_GetAll_Valid()
        {
            var mockGrandPrix = MockGrandPrixData.GetListDTOAsync();

            var mockGrandPrixService = new MockGrandPrixService().MockGetAll(mockGrandPrix);

            var controller = new GrandPrixController(mockGrandPrixService.Object);

            var result = await controller.Get();

            Assert.IsAssignableFrom<IActionResult>(result);
            mockGrandPrixService.VerifyGetAll(Times.Once());
        }

        [Fact]
        public async void GrandPrixController_GetAll_Empty()
        {
            var mockGrandPrix = MockGrandPrixData.GetEmptyListDTOAsync();

            var mockGrandPrixService = new MockGrandPrixService().MockGetAll(mockGrandPrix);

            var controller = new GrandPrixController(mockGrandPrixService.Object);

            var result = await controller.Get();

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var GrandPrix = okResult.Value.Should().BeAssignableTo<IEnumerable<GrandPrixDto>>().Subject;
            GrandPrix.Count().Should().Be(0);
            mockGrandPrixService.VerifyGetAll(Times.Once());
        }

        [Fact]
        public async void GrandPrixController_GetByYear_Valid()
        {
            var mockGrandPrixYear = 2020;
            var mockGrandPrix = MockGrandPrixData.GetListDTOAsync();

            var mockGrandPrixService = new MockGrandPrixService().MockGetByYear(mockGrandPrix);

            var controller = new GrandPrixController(mockGrandPrixService.Object);

            var result = await controller.Get(mockGrandPrixYear);

            Assert.IsAssignableFrom<IActionResult>(result);
            mockGrandPrixService.VerifyGetByYear(Times.Once());
        }

        [Fact]
        public async void GrandPrixController_GetByYear_Empty()
        {
            var mockGrandPrixYear = 2020;
            var mockGrandPrix = MockGrandPrixData.GetEmptyListDTOAsync();

            var mockGrandPrixService = new MockGrandPrixService().MockGetByYear(mockGrandPrix);

            var controller = new GrandPrixController(mockGrandPrixService.Object);

            var result = await controller.Get(mockGrandPrixYear);

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var GrandPrix = okResult.Value.Should().BeAssignableTo<IEnumerable<GrandPrixDto>>().Subject;
            GrandPrix.Count().Should().Be(0);
            mockGrandPrixService.VerifyGetByYear(Times.Once());
        }
    }
}