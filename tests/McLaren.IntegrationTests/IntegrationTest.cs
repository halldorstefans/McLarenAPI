using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace McLaren.IntegrationTests
{
    public class IntegrationTest : IClassFixture<ApiWebApplicationFactory>
    {
        protected readonly ApiWebApplicationFactory _factory;
        protected readonly HttpClient _client;
        protected readonly IConfiguration _configuration;

        public IntegrationTest(ApiWebApplicationFactory fixture)
        {
            _factory = fixture;
            _client = _factory.CreateClient();
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            _configuration.GetConnectionString("SQLiteConnection");
        }
    }
}