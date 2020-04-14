using McLaren.Core.Interfaces;
using McLaren.Core.Services;
using McLaren.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace McLaren.IntegrationTests
{
    public class ApiWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(config =>        
            {            
                var integrationConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                config.AddConfiguration(integrationConfig);        
            });

            builder.ConfigureTestServices(services =>
            {
                services.AddScoped<ICarsService, CarsService>();
                services.AddScoped<IDriversService, DriversService>();
                services.AddScoped<IGrandPrixesService, GrandPrixesService>();
            });
        }
    }
}