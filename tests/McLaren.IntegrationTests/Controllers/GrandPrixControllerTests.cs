using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using McLaren.Core.Models;
using Newtonsoft.Json;
using Xunit;

namespace McLaren.IntegrationTests
{
    public class GrandPrixControllerTests : IntegrationTest
    {
        public GrandPrixControllerTests(ApiWebApplicationFactory fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task Get_Should_Return_AllGrandPrixes()
        {
            // Act
            var response = await _client.GetAsync("/api/formula1/v0.9/GrandPrixes");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var GrandPrixs = JsonConvert.DeserializeObject<IEnumerable<GrandPrixDto>>(await response.Content.ReadAsStringAsync());
            GrandPrixs.Should().HaveCount(863);
        }

        [Fact]
        public async Task Get_Should_Return_GrandPrixesFromId()
        {
            // Act
            var response = await _client.GetAsync("/api/formula1/v0.9/GrandPrixes/10");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var GrandPrixs = JsonConvert.DeserializeObject<IEnumerable<GrandPrixDto>>(await response.Content.ReadAsStringAsync());
            GrandPrixs.Should().HaveCount(1);
        }

        [Fact]
        public async Task Get_Should_Return_NotFoundFromId()
        {
            // Act
            var response = await _client.GetAsync("/api/formula1/v0.9/GrandPrixes/1000");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Get_Should_Return_GrandPrixesFromYear()
        {
            // Act
            var response = await _client.GetAsync("/api/formula1/v0.9/GrandPrixes?year=1969");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var GrandPrixs = JsonConvert.DeserializeObject<IEnumerable<GrandPrixDto>>(await response.Content.ReadAsStringAsync());
            GrandPrixs.Should().HaveCount(11);
        }

        [Fact]
        public async Task Get_Should_Return_EmptyFromYear()
        {
            // Act
            var response = await _client.GetAsync("/api/formula1/v0.9/GrandPrixes?year=1950");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var grandPrixes = JsonConvert.DeserializeObject<IEnumerable<GrandPrixDto>>(await response.Content.ReadAsStringAsync());
            grandPrixes.Should().HaveCount(0);
        }

        [Fact]
        public async Task Get_Should_Return_GrandPrixesFromCountry()
        {
            // Act
            var response = await _client.GetAsync("/api/formula1/v0.9/GrandPrixes?country=spain");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Get_Should_Return_EmptyFromCountry()
        {
            // Act
            var response = await _client.GetAsync("/api/formula1/v0.9/GrandPrixes?country=hello");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var grandPrixes = JsonConvert.DeserializeObject<IEnumerable<GrandPrixDto>>(await response.Content.ReadAsStringAsync());
            grandPrixes.Should().HaveCount(0);
        }
    }
}