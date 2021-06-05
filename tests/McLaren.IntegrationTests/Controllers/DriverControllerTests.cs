using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
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
        public async Task Get_Should_Return_AllDrivers()
        {
            // Act
            var response = await _client.GetAsync("/api/formula1/v0.9/Drivers");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var Drivers = JsonConvert.DeserializeObject<IEnumerable<DriverDto>>(await response.Content.ReadAsStringAsync());
            Drivers.Should().HaveCount(57);
        }

        [Fact]
        public async Task Get_Should_Return_OneDriverFromName()
        {
            // Act
            var response = await _client.GetAsync("/api/formula1/v0.9/Drivers?name=van rooyen");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var Drivers = JsonConvert.DeserializeObject<IEnumerable<DriverDto>>(await response.Content.ReadAsStringAsync());
            foreach (var driver in Drivers)
            {
                Assert.Equal(5, driver.id);
            }            
        }

        [Fact]
        public async Task Get_Should_Return_EmptyFromName()
        {
            // Act
            var response = await _client.GetAsync("/api/formula1/v0.9/Drivers?name=beckham");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var Drivers = JsonConvert.DeserializeObject<IEnumerable<DriverDto>>(await response.Content.ReadAsStringAsync());
            Drivers.Should().HaveCount(0);
        }

        [Fact]
        public async Task Get_Should_Return_DriversFromId()
        {
            // Act
            var response = await _client.GetAsync("/api/formula1/v0.9/Drivers/1");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var driver = JsonConvert.DeserializeObject<DriverDto>(await response.Content.ReadAsStringAsync());
            Assert.Equal("Bruce", driver.firstName);
        }

        [Fact]
        public async Task Get_Should_Return_NotFoundFromId()
        {
            // Act
            var response = await _client.GetAsync("/api/formula1/v0.9/Drivers/100");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}