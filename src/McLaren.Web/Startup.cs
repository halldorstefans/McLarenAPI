using System;
using System.IO;
using System.Reflection;
using System.Text.Json.Serialization;
using McLaren.Core.Interfaces;
using McLaren.Core.Interfaces.Repositories;
using McLaren.Core.Services;
using McLaren.Infrastructure.Data;
using McLaren.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using McLaren.Web.Services;
using McLaren.Web.Filters;
using McLaren.Web.Configurations;
using McLaren.Infrastructure;
using McLaren.Infrastructure.Filters;
using McLaren.Infrastructure.Middleware.Logging;

namespace McLaren.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry();

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(TrackActionPerformanceFilter));

                options.Filters.Add(
                    new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));
                options.Filters.Add(
                    new ProducesResponseTypeAttribute(StatusCodes.Status406NotAcceptable));
                options.Filters.Add(
                    new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
                options.Filters.Add(
                    new ProducesDefaultResponseTypeAttribute());
                
                options.ReturnHttpNotAcceptable = true;                
            })
            .AddJsonOptions(options =>
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve
            );
            
            if(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
            {
                services.AddDbContext<McLarenContext>(options =>
                        options.UseSqlServer(Configuration.GetConnectionString("AzureSQLConnection")));
            }
            else
            {
                services.AddDbContext<McLarenContext>(options =>
                    options.UseSqlite(Configuration.GetConnectionString("SQLiteConnection"))); 
            }

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var actionExecutingContext =
                        actionContext as Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext;

                    if (actionContext.ModelState.ErrorCount > 0
                        && actionExecutingContext?.ActionArguments.Count == actionContext.ActionDescriptor.Parameters.Count)
                    {
                        return new UnprocessableEntityObjectResult(actionContext.ModelState);
                    }

                    return new BadRequestObjectResult(actionContext.ModelState);
                };
            });

            services.AddSingleton<IScopeInformation, ScopeInformation>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IGrandPrixesRepository, GrandPrixesRepository>();
            services.AddScoped<IGrandPrixesService, GrandPrixesService>();

            services.AddScoped<ICarsRepository, CarsRepository>();
            services.AddScoped<ICarsService, CarsService>();

            services.AddScoped<IDriversRepository, DriversRepository>();
            services.AddScoped<IDriversService, DriversService>();

            services.AddSingleton<ILogService>((container) =>
            {
                var logger = container.GetRequiredService<ILogger<LogService>>();
                return new LogService() { Logger = logger };
            });        
           
            services.AddVersionedApiExplorer(
                options =>
                {                    
                    options.GroupNameFormat = "'v'VVV";

                    options.SubstituteApiVersionInUrl = true;
                });

            services.AddApiVersioning(
                options =>
                {
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.DefaultApiVersion = new ApiVersion(0, 9);
                    options.ReportApiVersions = true;
                    
                    options.Conventions.Add(new VersionByNamespaceConvention());
                });

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            services.AddSwaggerGen(options =>
            {
                options.OperationFilter<SwaggerDefaultValues>();
                
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            app.UseApiExceptionHandler();
            
            app.UseStaticFiles();

            app.UseHttpsRedirection();            

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseReDoc(options => 
            {
                options.RoutePrefix = "redocs";
                foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                {
                    options.SpecUrl($"/docs/" +
                        $"{description.GroupName}/docs.json");
                }
                options.ExpandResponses("200,201");
            });

            app.UseSwagger(options =>
            {
                options.RouteTemplate = "docs/{documentName}/docs.json";
            });

            app.UseSwaggerUI(options =>
            {
                options.DocumentTitle = "McLaren F1 API Docs";
                options.InjectStylesheet("/swagger-ui/custom.css");

                foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/docs/" +
                        $"{description.GroupName}/docs.json",
                        description.GroupName.ToUpperInvariant());
                }
                options.RoutePrefix = "docs";                
            });
        }
    }
}
