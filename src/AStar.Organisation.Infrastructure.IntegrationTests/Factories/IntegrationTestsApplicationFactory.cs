using AStar.Organisation.Infrastructure.API;
using AStar.Organisation.Infrastructure.DAL.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;

namespace AStar.Organisation.Infrastructure.IntegrationTests.Factories
{
    public class IntegrationTestsApplicationFactory : WebApplicationFactory<Startup>
    {
        private readonly IConfiguration _configuration;
        public IntegrationTestsApplicationFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((hostingContext, config) =>
            {
                var configDevelopmentSource = config.Sources
                    .FirstOrDefault(s => s.GetType() == typeof(JsonConfigurationSource) &&
                    string.Equals(((JsonConfigurationSource)s).Path, "appsettings.Development.json"));
                
                var configCommonSource = config.Sources
                    .FirstOrDefault(s => s.GetType() == typeof(JsonConfigurationSource) &&
                    string.Equals(((JsonConfigurationSource)s).Path, "appsettings.json"));

                config.Sources.Remove(configDevelopmentSource);
                config.Sources.Remove(configCommonSource);
                
                config.AddConfiguration(_configuration);
            });
            
            builder.ConfigureTestServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == 
                    typeof(DbContextOptions<OrganizationContext>));
                if (descriptor != null)
                    services.Remove(descriptor);
                
                services.AddDbContext<OrganizationContext>(opts => 
                    opts.UseNpgsql(_configuration["DbConnection"]));
                
                services.BuildServiceProvider();
            });
        }
    }
}