using System.Collections.Generic;
using System.Net;
using FluentAssertions;
using McLaren.Core.Models;
using Newtonsoft.Json;
using Xunit;

namespace McLaren.IntegrationTests
{
    public class CarControllerTests : IntegrationTest
    {
        public CarControllerTests(ApiWebApplicationFactory fixture) : base(fixture)
        {
        }

        [Fact]
        public async void Get_Should_Return_AllCars()
        {
            var response = await _client.GetAsync("/api/formula1/v1/car");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var cars = JsonConvert.DeserializeObject<IEnumerable<CarDto>>(await response.Content.ReadAsStringAsync());
            cars.Should().HaveCount(6);
        }

        [Fact]
        public async void Get_Should_Return_OneCarFromName()
        {
            var response = await _client.GetAsync("/api/formula1/v1/car/m7a");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var cars = JsonConvert.DeserializeObject<CarDto>(await response.Content.ReadAsStringAsync());
            Assert.Equal(4, cars.id);
        }

        [Fact]
        public async void Get_Should_Return_NotFoundFromName()
        {
            var response = await _client.GetAsync("/api/formula1/v1/car/m6a");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async void Get_Should_Return_CarsFromYear()
        {
            var response = await _client.GetAsync("/api/formula1/v1/car/1968");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var cars = JsonConvert.DeserializeObject<IEnumerable<CarDto>>(await response.Content.ReadAsStringAsync());
            cars.Should().HaveCount(3);
        }

        [Fact]
        public async void Get_Should_Return_NotFoundFromYear()
        {
            var response = await _client.GetAsync("/api/formula1/v1/car/1950");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}