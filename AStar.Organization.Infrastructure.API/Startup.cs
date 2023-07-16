using AStar.Organisation.Core.Application.Services;
using AStar.Organisation.Infrastructure.API.Middlewares;
using AStar.Organisation.Infrastructure.API.Services;
using AStar.Organisation.Infrastructure.DAL;
using AStar.Organisation.Infrastructure.DAL.Contexts;
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
            services.AddSingleton<IConfigurationService, ConfigurationService>();
            
            services.AddDbContext<OrganizationContext>(options =>
            {
                options.UseNpgsql(_configuration["DbConnection"],b => 
                    b.MigrationsAssembly("AStar.Organization.Infrastructure.API"));
            });
            
            services.AddHttpClient();
            
            services.AddDal();
            services.AddBll();

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