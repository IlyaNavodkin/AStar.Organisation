using System.Net;
using System.Text;
using System.Text.Json;
using AStar.Organisation.Core.Application.Dtos;
using AStar.Organisation.Core.Application.IServices;
using AStar.Organisation.Infrastructure.API.Utills;
using AStar.Organisation.Infrastructure.DAL.Contexts;
using AStar.Organisation.Infrastructure.IntegrationTests.Factories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AStar.Organisation.Infrastructure.IntegrationTests.Tests
{
    [TestFixture]
    public class CustomerControllerIntegrationTests
    {
        private IConfiguration _configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.Test.json")
            .Build();
        
        [Test, Order(1)]
        public async Task CustomerController_GetEntities_ShouldReturnStatusCode200AndValidCustomersCount()
        {
            // Arrage
            
            var webHost = new IntegrationTestsApplicationFactory(_configuration)
                .WithWebHostBuilder(_ => { });

            var customers = EntityInitilizeUtill.GetCustomers();
            
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
            
            // Act

            var client = webHost.CreateClient();
            var response = await client.GetAsync("api/Customer/GetAll");

            var contentString = await response.Content.ReadAsStringAsync();
            var customerDtos = JsonSerializer.Deserialize<List<CustomerDto>>(contentString, 
                new JsonSerializerOptions{PropertyNameCaseInsensitive  = true});

            // Assert
            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(customers.Count, customerDtos.Count);
        }
        
        [Test, Order(2)]
        public async Task CustomerController_GetEntityById_ShouldReturnStatusCode200AndValidCustomer()
        {
            // Arrage
            
            var webHost = new IntegrationTestsApplicationFactory(_configuration)
                .WithWebHostBuilder(_ => { });

            var customer = EntityInitilizeUtill.GetCustomers().First();
            
            using (var scope = webHost.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<OrganizationContext>();

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var customerService = scope.ServiceProvider.GetRequiredService<ICustomerService>();
                
                var dto = new CustomerDto
                {
                    Name = customer.Name,
                    Email = customer.Email,
                    Phone = customer.Phone
                };

                customerService.Create(dto);
            }
            
            // Act

            var client = webHost.CreateClient();
            var response = await client.GetAsync($"api/Customer/GetById?id={customer.Id}");

            var contentString = await response.Content.ReadAsStringAsync();
            var customerDtos = JsonSerializer.Deserialize<CustomerDto>(contentString, 
                new JsonSerializerOptions{PropertyNameCaseInsensitive  = true});

            // Assert
            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(customer.Name, customerDtos.Name);
            Assert.AreEqual(customer.Phone, customerDtos.Phone);
            Assert.AreEqual(customer.Email, customerDtos.Email);
        }
        
        [Test, Order(3)]
        public async Task CustomerController_DeleteEntity_ShouldReturnStatusCode200AndDeleteEntity()
        {
            // Arrage
            
            var webHost = new IntegrationTestsApplicationFactory(_configuration)
                .WithWebHostBuilder(_ => { });

            var customer = EntityInitilizeUtill.GetCustomers().First();
            
            using (var scope = webHost.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<OrganizationContext>();

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var customerService = scope.ServiceProvider.GetRequiredService<ICustomerService>();
                
                var dto = new CustomerDto
                {
                    Name = customer.Name,
                    Email = customer.Email,
                    Phone = customer.Phone
                };

                customerService.Create(dto);
            }
            
            // Act

            var client = webHost.CreateClient();
            var deleteResponse = await client.DeleteAsync($"api/Customer/Delete?id={customer.Id}");
            var getResponse = await client.GetAsync($"api/Customer/GetById?id={customer.Id}");
            
            // Assert
            
            Assert.AreEqual(HttpStatusCode.OK, deleteResponse.StatusCode);
            Assert.AreEqual(HttpStatusCode.BadRequest, getResponse.StatusCode);
        }
        
        [Test, Order(4)]
        public async Task CustomerController_UpdateEntity_ShouldReturnStatusCode200AndUpdateEntity()
        {
            // Arrage
            
            var webHost = new IntegrationTestsApplicationFactory(_configuration)
                .WithWebHostBuilder(_ => { });

            var customer = EntityInitilizeUtill.GetCustomers().First();
            
            using (var scope = webHost.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<OrganizationContext>();

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var customerService = scope.ServiceProvider.GetRequiredService<ICustomerService>();
                
                var dto = new CustomerDto
                {
                    Name = customer.Name,
                    Email = customer.Email,
                    Phone = customer.Phone
                };

                customerService.Create(dto);
            }
            
            // Act

            var client = webHost.CreateClient();
            var getResponse = await client.GetAsync($"api/Customer/GetById?id={customer.Id}");
            var contentString = await getResponse.Content.ReadAsStringAsync();
            var customerDtos = JsonSerializer.Deserialize<CustomerDto>(contentString, 
                new JsonSerializerOptions{PropertyNameCaseInsensitive  = true});

            customerDtos.Name = "Новое имя";
            customerDtos.Email = "newEmail@gmail.com";
            customerDtos.Phone = "880021212";

            var updateResponse = await client.PutAsync("api/Customer/Update", 
                new StringContent(JsonSerializer.Serialize(customerDtos), Encoding.UTF8, "application/json"));
            
            // Assert
            
            Assert.AreEqual(HttpStatusCode.OK, updateResponse.StatusCode);
            Assert.AreNotEqual(customer.Name, customerDtos.Name);
            Assert.AreNotEqual(customer.Email, customerDtos.Email);
            Assert.AreNotEqual(customer.Phone, customerDtos.Phone);
        }
    }
}