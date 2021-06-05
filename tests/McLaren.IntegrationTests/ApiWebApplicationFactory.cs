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
        public IConfiguration Configuration { get; private set; }
        
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(config =>        
            {            
                Configuration = new ConfigurationBuilder().AddJsonFile("integrationsettings.json").Build();
                config.AddConfiguration(Configuration);        
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