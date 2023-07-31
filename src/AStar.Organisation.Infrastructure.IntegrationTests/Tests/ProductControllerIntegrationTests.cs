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
    public class ProductControllerIntegrationTests
    {
        private IConfiguration _configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.Test.json")
            .Build();
        
        [Test, Order(1)]
        public async Task ProductController_GetEntities_ShouldReturnStatusCode200AndValidCustomersCount()
        {
            // Arrage
            
            var webHost = new IntegrationTestsApplicationFactory(_configuration)
                .WithWebHostBuilder(_ => { });

            var entities = EntityInitilizeUtill.GetProducts();
            
            using (var scope = webHost.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<OrganizationContext>();

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var productService = scope.ServiceProvider.GetRequiredService<IProductService>();
                
                var dtos = entities.Select(e => new ProductDto()
                {
                    Name = e.Name,
                    Description = e.Description,
                    Price = e.Price
                });

                foreach (var dto in dtos)
                {
                    productService.Create(dto);
                }
            }
            
            // Act

            var client = webHost.CreateClient();
            var response = await client.GetAsync("api/Product/GetAll");

            var contentString = await response.Content.ReadAsStringAsync();
            var getDtos = JsonSerializer.Deserialize<List<ProductDto>>(contentString, 
                new JsonSerializerOptions{PropertyNameCaseInsensitive  = true});

            // Assert
            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(entities.Count, getDtos.Count);
        }
        
        [Test, Order(2)]
        public async Task ProductController_GetEntityById_ShouldReturnStatusCode200AndValidEntity()
        {
            // Arrage
            
            var webHost = new IntegrationTestsApplicationFactory(_configuration)
                .WithWebHostBuilder(_ => { });

            var entity = EntityInitilizeUtill.GetProducts().First();
            
            using (var scope = webHost.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<OrganizationContext>();

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var productService = scope.ServiceProvider.GetRequiredService<IProductService>();
                
                var dto = new ProductDto()
                {
                    Name = entity.Name,
                    Description = entity.Description,
                    Price = entity.Price
                };

                productService.Create(dto);
            }
            
            // Act

            var client = webHost.CreateClient();
            var response = await client.GetAsync($"api/Product/GetById?id={entity.Id}");

            var contentString = await response.Content.ReadAsStringAsync();
            var getDto = JsonSerializer.Deserialize<ProductDto>(contentString, 
                new JsonSerializerOptions{PropertyNameCaseInsensitive  = true});

            // Assert
            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(entity.Name, getDto.Name);
            Assert.AreEqual(entity.Description, getDto.Description);
            Assert.AreEqual(entity.Price, getDto.Price);
        }
        
        [Test, Order(3)]
        public async Task ProductController_DeleteEntity_ShouldReturnStatusCode200AndDeleteEntity()
        {
            // Arrage
            
            var webHost = new IntegrationTestsApplicationFactory(_configuration)
                .WithWebHostBuilder(_ => { });

            var entity = EntityInitilizeUtill.GetProducts().First();
            
            using (var scope = webHost.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<OrganizationContext>();

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var productService = scope.ServiceProvider.GetRequiredService<IProductService>();
                
                var dto = new ProductDto
                {
                    Name = entity.Name,
                    Description = entity.Description,
                    Price = entity.Price
                };

                productService.Create(dto);
            }
            
            // Act

            var client = webHost.CreateClient();
            var deleteResponse = await client.DeleteAsync($"api/Product/Delete?id={entity.Id}");
            var getResponse = await client.GetAsync($"api/Product/GetById?id={entity.Id}");
            
            // Assert
            
            Assert.AreEqual(HttpStatusCode.OK, deleteResponse.StatusCode);
            Assert.AreEqual(HttpStatusCode.BadRequest, getResponse.StatusCode);
        }
        
        [Test, Order(4)]
        public async Task ProductController_UpdateEntity_ShouldReturnStatusCode200AndUpdateEntity()
        {
            // Arrage
            
            var webHost = new IntegrationTestsApplicationFactory(_configuration)
                .WithWebHostBuilder(_ => { });

            var customer = EntityInitilizeUtill.GetProducts().First();
            
            using (var scope = webHost.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<OrganizationContext>();

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var productService = scope.ServiceProvider.GetRequiredService<IProductService>();
                
                var dto = new ProductDto
                {
                    Name = customer.Name,
                    Description = customer.Description,
                    Price = customer.Price
                };

                productService.Create(dto);
            }
            
            // Act

            var client = webHost.CreateClient();
            var getResponse = await client.GetAsync($"api/Product/GetById?id={customer.Id}");
            var contentString = await getResponse.Content.ReadAsStringAsync();
            var newDto = JsonSerializer.Deserialize<ProductDto>(contentString, 
                new JsonSerializerOptions{PropertyNameCaseInsensitive  = true});

            newDto.Name = "Новое имя";
            newDto.Description = "Описание";
            newDto.Price = 2222;

            var updateResponse = await client.PutAsync("api/Product/Update", 
                new StringContent(JsonSerializer.Serialize(newDto), Encoding.UTF8, "application/json"));
            
            // Assert
            
            Assert.AreEqual(HttpStatusCode.OK, updateResponse.StatusCode);
            Assert.AreNotEqual(customer.Name, newDto.Name);
            Assert.AreNotEqual(customer.Description, newDto.Description);
            Assert.AreNotEqual(customer.Price, newDto.Price);
        }
    }
}