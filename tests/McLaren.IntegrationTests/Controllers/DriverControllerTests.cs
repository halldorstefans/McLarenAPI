using System.Collections.Generic;
using System.Net;
using FluentAssertions;
using McLaren.Core.Models;
using Newtonsoft.Json;
using Xunit;

namespace McLaren.IntegrationTests
{
    public class DriverControllerTests : IntegrationTest
    {
        public DriverControllerTests(ApiWebApplicationFactory fixture) : base(fixture)
        {
        }

        [Fact]
        public async void Get_Should_Return_AllDrivers()
        {
            // Act
            var response = await _client.GetAsync("/api/formula1/v1/Drivers");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var Drivers = JsonConvert.DeserializeObject<IEnumerable<DriverDto>>(await response.Content.ReadAsStringAsync());
            Drivers.Should().HaveCount(7);
        }

        [Fact]
        public async void Get_Should_Return_OneDriverFromName()
        {
            // Act
            var response = await _client.GetAsync("/api/formula1/v1/Drivers?name=van rooyen");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var Drivers = JsonConvert.DeserializeObject<IEnumerable<DriverDto>>(await response.Content.ReadAsStringAsync());
            foreach (var driver in Drivers)
            {
                Assert.Equal(5, driver.id);
            }            
        }

        [Fact]
        public async void Get_Should_Return_EmptyFromName()
        {
            // Act
            var response = await _client.GetAsync("/api/formula1/v1/Drivers?name=hamilton");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var Drivers = JsonConvert.DeserializeObject<IEnumerable<DriverDto>>(await response.Content.ReadAsStringAsync());
            Drivers.Should().HaveCount(0);
        }

        [Fact]
        public async void Get_Should_Return_DriversFromId()
        {
            // Act
            var response = await _client.GetAsync("/api/formula1/v1/Drivers/1");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var driver = JsonConvert.DeserializeObject<DriverDto>(await response.Content.ReadAsStringAsync());
            Assert.Equal("Bruce", driver.firstName);
        }

        [Fact]
        public async void Get_Should_Return_NotFoundFromId()
        {
            // Act
            var response = await _client.GetAsync("/api/formula1/v1/Drivers/100");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}