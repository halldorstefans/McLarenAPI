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
            // Act
            var response = await _client.GetAsync("/api/formula1/v0.9/cars");
            
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var cars = JsonConvert.DeserializeObject<IEnumerable<CarDto>>(await response.Content.ReadAsStringAsync());
            cars.Should().HaveCount(6);
        }

        [Fact]
        public async void Get_Should_Return_OneCarFromId()
        {
            // Act
            var response = await _client.GetAsync("/api/formula1/v0.9/cars/5");
            
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var cars = JsonConvert.DeserializeObject<CarDto>(await response.Content.ReadAsStringAsync());
            Assert.Equal(5, cars.id);
        }

        [Fact]
        public async void Get_Should_Return_OneCarFromName()
        {
            // Act
            var response = await _client.GetAsync("/api/formula1/v0.9/cars?name=m7a");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var cars = JsonConvert.DeserializeObject<IEnumerable<CarDto>>(await response.Content.ReadAsStringAsync());
            cars.Should().HaveCount(1);
        }

        [Fact]
        public async void Get_Should_Return_EmptyFromName()
        {
            // Act
            var response = await _client.GetAsync("/api/formula1/v0.9/cars?name=m6a");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var cars = JsonConvert.DeserializeObject<IEnumerable<CarDto>>(await response.Content.ReadAsStringAsync());
            cars.Should().HaveCount(0);
        }

        [Fact]
        public async void Get_Should_Return_CarsFromYear()
        {
            // Act
            var response = await _client.GetAsync("/api/formula1/v0.9/cars?year=1968");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var cars = JsonConvert.DeserializeObject<IEnumerable<CarDto>>(await response.Content.ReadAsStringAsync());
            cars.Should().HaveCount(3);
        }

        [Fact]
        public async void Get_Should_Return_EmptyFromYear()
        {
            // Act
            var response = await _client.GetAsync("/api/formula1/v0.9/cars?year=1950");
            
            // Assert            
            response.StatusCode.Should().Be(HttpStatusCode.OK);            
            var cars = JsonConvert.DeserializeObject<IEnumerable<CarDto>>(await response.Content.ReadAsStringAsync());
            cars.Should().HaveCount(0);
        }
    }
}