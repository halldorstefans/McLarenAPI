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
            var response = await _client.GetAsync("/api/f1/Driver");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var Drivers = JsonConvert.DeserializeObject<IEnumerable<DriverDto>>(await response.Content.ReadAsStringAsync());
            Drivers.Should().HaveCount(7);
        }

        [Fact]
        public async void Get_Should_Return_OneDriverFromLastName()
        {
            var response = await _client.GetAsync("/api/f1/Driver/van_rooyen");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var Drivers = JsonConvert.DeserializeObject<IEnumerable<DriverDto>>(await response.Content.ReadAsStringAsync());
            foreach (var driver in Drivers)
            {
                Assert.Equal(5, driver.id);
            }            
        }

        [Fact]
        public async void Get_Should_Return_NotFoundFromLastName()
        {
            var response = await _client.GetAsync("/api/f1/Driver/hamilton");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async void Get_Should_Return_DriversFromId()
        {
            var response = await _client.GetAsync("/api/f1/Driver/1");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var driver = JsonConvert.DeserializeObject<DriverDto>(await response.Content.ReadAsStringAsync());
            Assert.Equal("Bruce", driver.firstName);
        }

        [Fact]
        public async void Get_Should_Return_NotFoundFromId()
        {
            var response = await _client.GetAsync("/api/f1/Driver/100");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}