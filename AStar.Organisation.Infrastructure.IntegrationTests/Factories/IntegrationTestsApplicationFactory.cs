using AStar.Organisation.Infrastructure.API;
using AStar.Organisation.Infrastructure.DAL.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AStar.Organisation.Infrastructure.IntegrationTests.Factories
{
    public class IntegrationTestsApplicationFactory : WebApplicationFactory<Startup>
    {
        private const string TestConnectionString =
            "Server=localhost;Port=5432;Database=IntegrationTestsDb;User Id=postgres;Password=diakonttest228;Encoding=UTF8;";

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == 
                                                               typeof(DbContextOptions<OrganizationContext>));
                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddDbContext<OrganizationContext>(opts => 
                    opts.UseNpgsql(TestConnectionString));

                var serviceProvider = services.BuildServiceProvider();
                
                using (serviceProvider.CreateScope())
                {
                    var context = serviceProvider.GetService<OrganizationContext>();

                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();

                    SeedTestsData(context);
                }
            });
        }

        private void SeedTestsData(OrganizationContext organizationContext)
        {
            var products = EntityFactory.GetProducts();
            var customers = EntityFactory.GetCustomers();

            organizationContext.Product.AddRange(products);
            organizationContext.Customer.AddRange(customers);

            organizationContext.SaveChanges();
        }

    }
}