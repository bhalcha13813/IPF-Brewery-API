using Microsoft.EntityFrameworkCore;
using IPF.Brewery.API.Filters;
using IPF.Brewery.API.Services;
using IPF.Brewery.API.Validation;
using IPF.Brewery.Common.Configuration;
using IPF.Brewery.Common.Repositories;
using IPF.Brewery.Common;

namespace IPF.Brewery.API
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IWebHostEnvironment env)
        {
            configuration = BuildConfiguration(env);
        }

        protected IConfigurationRoot BuildConfiguration(IWebHostEnvironment hostingEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", true, true);

            builder.AddEnvironmentVariables();
            using var tempConfig = (ConfigurationRoot)builder.Build();

            return builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            var breweryDBConfiguration = new BreweryDBConfiguration();
            configuration.Bind("BreweryConfiguration", breweryDBConfiguration);
            services.AddDbContext<BreweryContext>(options =>
            {
                options.UseSqlServer(breweryDBConfiguration.ConnectionString,
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: breweryDBConfiguration.MaxRetryCount,
                            maxRetryDelay: TimeSpan.FromSeconds(breweryDBConfiguration.MaxRetryDelayInSeconds),
                            errorNumbersToAdd: null);
                    });
            });

            

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IBreweryValidator, BreweryValidator>();
            services.AddScoped<IBreweryService, BreweryService>();
            services.AddScoped<IBreweryRepository, BreweryRepository>();

            services.AddScoped<IBarValidator, BarValidator>();
            services.AddScoped<IBarService, BarService>();
            services.AddScoped<IBarRepository, BarRepository>();

            services.AddScoped<IBeerValidator, BeerValidator>();
            services.AddScoped<IBeerService, BeerService>();
            services.AddScoped<IBeerRepository, BeerRepository>();

            services.AddScoped<IBeerTypeValidator, BeerTypeValidator>();
            services.AddScoped<IBeerTypeService, BeerTypeService>();
            services.AddScoped<IBeerTypeRepository, BeerTypeRepository>();

            services.AddHealthChecks();


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory,
            IHttpContextAccessor httpContextAccessor)
        {
            loggerFactory.AddFile(configuration.GetSection("Logging"));

            app.UseCorrelationIdMiddleware(loggerFactory)
               .UseErrorLogging(loggerFactory);

            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
                endpoints.MapGet("/heartbeat", async ctx => await ctx.Response.WriteAsync("Alive"));
                endpoints.MapControllers();
            });

        }
    }
}
