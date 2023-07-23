using AStar.Organisation.Core.Application.Dtos;
using AStar.Organisation.Core.Application.IServices;
using AStar.Organisation.Core.DomainServices.IUnitOfWork;
using AStar.Organisation.Infrastructure.API.Utills;
using AStar.Organisation.Infrastructure.DAL.Contexts;
using AStar.Organisation.Infrastructure.IntegrationTests.Factories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AStar.Organisation.Infrastructure.IntegrationTests.Tests
{
    [TestFixture]
    public class CustomerServiceIntegrationTests
    {
        private IConfiguration _configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.Test.json")
            .Build();
        
        [Test, Order(1)]
        public async Task CustomerService_CreateEntities_ShouldCreateEntities()
        {
            // Arrage
            
            var webHost = new IntegrationTestsApplicationFactory(_configuration)
                .WithWebHostBuilder(_ => { });

            var customers = EntityInitilizeUtill.GetCustomers();
            
            // Act

            using (var scope = webHost.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<OrganizationContext>();

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var customerService = scope.ServiceProvider.GetRequiredService<ICustomerService>();
                
                var dtos = customers.Select(e => new CustomerDto
                {
                    Name = e.Name,
                    Email = e.Email,
                    Phone = e.Phone
                });

                foreach (var dto in dtos)
                {
                    customerService.Create(dto);
                }
            }
            
            using (var scope = webHost.Services.CreateScope())
            {
                var customerService = scope.ServiceProvider.GetRequiredService<ICustomerService>();
                var allCustomers = await customerService.GetAll();
                
                // Assert
                
                Assert.AreEqual(customers.Count,allCustomers.Count());
            } 
        }
    }
}