using AStar.Application.Services;
using AStar.Organisation.Infrastructure.API.Middlewares;
using AStar.Organisation.Infrastructure.API.Services;
using AStar.Organisation.Infrastructure.DAL;
using AStar.Organisation.Infrastructure.DAL.Repositories.Contexts;
using AStar.Organization.Infrastructure.BLL;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace AStar.Organisation.Infrastructure.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = _configuration["DbConnection"];

            services.AddDbContext<OrganizationContext>(options =>
            {
                options.UseNpgsql(connectionString,b => 
                    b.MigrationsAssembly("AStar.Organization.Infrastructure.API"));
            });
            
            services.AddHttpClient();
            services.AddSingleton<IConfigurationService, ConfigurationService>();
            services.AddDal(_configuration);
            services.AddBll(_configuration);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = _configuration["ApiName"], Version = "v1" });
            });
            
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment hostEnvironment)
        {
            if (hostEnvironment.IsDevelopment()) app.UseDeveloperExceptionPage();
            
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{_configuration["ApiName"]} v1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}