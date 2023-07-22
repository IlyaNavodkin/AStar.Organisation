using AStar.Organisation.Core.DomainServices.IUnitOfWork;
using AStar.Organisation.Infrastructure.API.Utills;
using AStar.Organisation.Infrastructure.DAL.Contexts;
using AStar.Organisation.Infrastructure.IntegrationTests.Factories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AStar.Organisation.Infrastructure.IntegrationTests.Tests
{
    [TestFixture]
    public class DalIntegrationTests
    {
        private IConfiguration _configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.Test.json")
            .Build();
        
        [Test, Order(1)]
        public async Task UnitOfWork_CreateEntities_ShouldCreateEntities()
        {
            // Arrage
            
            var webHost = new IntegrationTestsApplicationFactory(_configuration)
                .WithWebHostBuilder(_ => { });

            var products = EntityInitilizeUtill.GetProducts();
            var customers = EntityInitilizeUtill.GetCustomers();
            
            // Act

            using (var scope = webHost.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<OrganizationContext>();

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var allCustomers = await unitOfWork.CustomerRepository.GetAll();
                var allProducts  = await unitOfWork.ProductRepository.GetAll();

                foreach (var entity in products)
                {
                    await unitOfWork.ProductRepository.Create(entity);
                }
                
                foreach (var entity in customers)
                {
                    await unitOfWork.CustomerRepository.Create(entity);
                }
                
                unitOfWork.Commit();
            }
            
            using (var scope = webHost.Services.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var allCustomers = await unitOfWork.CustomerRepository.GetAll();
                var allProducts  = await unitOfWork.ProductRepository.GetAll();
                
                // Assert
                
                Assert.AreEqual(products.Count,allProducts.Count());
                Assert.AreEqual(customers.Count,allCustomers.Count());
            } 
        }

        [Test]
        public async Task UnitOfWork_DeleteEntities_ShouldDeleteEntities()
        {
            // Arrage
            
            var webHost = new IntegrationTestsApplicationFactory(_configuration)
                .WithWebHostBuilder(_ => { });

            var id = 1;
            
            // Act

            using (var scope = webHost.Services.CreateScope())
            {
                EntityInitilizeUtill.SeedTestsData(scope);
            
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var allCustomers = await unitOfWork.CustomerRepository.GetAll();
                var allProducts  = await unitOfWork.ProductRepository.GetAll();
                
                await unitOfWork.CustomerRepository.Delete(id);
                await unitOfWork.ProductRepository.Delete(id);
                unitOfWork.Commit();
            }

            using (var scope = webHost.Services.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var allCustomers = await unitOfWork.CustomerRepository.GetAll();
                var allProducts  = await unitOfWork.ProductRepository.GetAll();

                var actualCustomer = await unitOfWork.CustomerRepository.GetById(id);
                var actualProduct = await unitOfWork.CustomerRepository.GetById(id);
                
                // Assert
                
                Assert.AreEqual(null,actualCustomer);
                Assert.AreEqual(null,actualProduct);
            } 
        }
        
        [Test]
        public async Task UnitOfWork_EditEntities_ShouldEditEntities()
        {
            // Arrage
            
            var webHost = new IntegrationTestsApplicationFactory(_configuration)
                .WithWebHostBuilder(_ => { });

            var id = 1;
            var oldProduct = EntityInitilizeUtill.GetProducts().FirstOrDefault(e => e.Id == id);
            
            // Act

            using (var scope = webHost.Services.CreateScope())
            {
                EntityInitilizeUtill.SeedTestsData(scope);
            
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var allCustomers = await unitOfWork.CustomerRepository.GetAll();
                var allProducts  = await unitOfWork.ProductRepository.GetAll();

                var product = await unitOfWork.ProductRepository.GetById(id);
                product.Name = "Новое имя";
                product.Description = "Новое Описание";
                product.Price = 666;

                await unitOfWork.ProductRepository.Update(product);
                
                unitOfWork.Commit();
            }

            using (var scope = webHost.Services.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var allCustomers = await unitOfWork.CustomerRepository.GetAll();
                var allProducts  = await unitOfWork.ProductRepository.GetAll();

                var actualProduct = await unitOfWork.ProductRepository.GetById(id);
                
                // Assert
                
                Assert.AreNotEqual(oldProduct.Name,actualProduct.Name);
                Assert.AreNotEqual(oldProduct.Description,actualProduct.Description);
                Assert.AreNotEqual(oldProduct.Price,actualProduct.Price);
            } 
        }
        
        [Test]
        public async Task UnitOfWork_GetEntities_ShouldGetEntities()
        {
            // Arrage
            
            var webHost = new IntegrationTestsApplicationFactory(_configuration)
                .WithWebHostBuilder(_ => { });

            var id = 1;
            var expectedProducts = EntityInitilizeUtill.GetProducts();
            var expectedCustomers = EntityInitilizeUtill.GetCustomers();
            
            // Act

            using (var scope = webHost.Services.CreateScope())
            {
                EntityInitilizeUtill.SeedTestsData(scope);

                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var actualCustomers = await unitOfWork.CustomerRepository.GetAll();
                var actualProducts  = await unitOfWork.ProductRepository.GetAll();
                
                // Assert
                
                Assert.AreEqual(expectedProducts.Count, actualProducts.Count());
                Assert.AreEqual(expectedCustomers.Count, actualCustomers.Count());
            } 
        }
    }
}