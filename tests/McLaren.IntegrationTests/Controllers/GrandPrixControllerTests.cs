using System.Collections.Generic;
using System.Net;
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
        public async void Get_Should_Return_AllGrandPrixs()
        {
            var response = await _client.GetAsync("/api/formula1/v1/GrandPrix");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var GrandPrixs = JsonConvert.DeserializeObject<IEnumerable<GrandPrixDto>>(await response.Content.ReadAsStringAsync());
            GrandPrixs.Should().HaveCount(33);
        }

        [Fact]
        public async void Get_Should_Return_GrandPrixsFromYear()
        {
            var response = await _client.GetAsync("/api/formula1/v1/GrandPrix/1969");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var GrandPrixs = JsonConvert.DeserializeObject<IEnumerable<GrandPrixDto>>(await response.Content.ReadAsStringAsync());
            GrandPrixs.Should().HaveCount(11);
        }

        [Fact]
        public async void Get_Should_Return_NotFoundFromYear()
        {
            var response = await _client.GetAsync("/api/formula1/v1/GrandPrix/1950");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async void Get_Should_Return_NotFoundInvalidParameter()
        {
            var response = await _client.GetAsync("/api/formula1/v1/GrandPrix/hello");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}