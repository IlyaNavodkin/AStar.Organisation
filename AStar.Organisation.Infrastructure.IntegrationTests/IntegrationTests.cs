using System.Net;
using AStar.Organisation.Core.DomainServices.IUnitOfWork;
using AStar.Organisation.Infrastructure.API;
using AStar.Organisation.Infrastructure.DAL.Contexts;
using AStar.Organisation.Infrastructure.IntegrationTests.Factories;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace AStar.Organisation.Infrastructure.IntegrationTests
{
    [TestFixture]
    [Parallelizable(ParallelScope.None)]
    public class IntegrationTests
    {
        private IntegrationTestsApplicationFactory _factory = new();
        private OrganizationContext _context = null!;
        private IUnitOfWork _unitOfWork = null!;
        private HttpClient _client = null!;
        private IServiceScope _scope = null!;
        private WebApplicationFactory<Startup> _webHost = null!;
        
        [SetUp] 
        public void Setup()
        {
            _scope = _factory.Services.CreateScope();
            _context = _scope.ServiceProvider.GetRequiredService<OrganizationContext>();
            _unitOfWork = _scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            _client = _factory.CreateClient();
            _webHost = new IntegrationTestsApplicationFactory().WithWebHostBuilder(_ => { });
        }

        [Test]
        public void CheckDbContext_GetProducts_ShouldReturnProducts()
        {
            // Arrage
            
            var expectedEntities = EntityFactory.GetProducts();
            
            // Act
            
            var actualEntities = _context.Product.ToList();
            
            // Assert
        
            Assert.AreEqual(expectedEntities.Count,actualEntities.Count);
        }

        [Test]
        public void CheckDbContext_GetCustomers_ShouldReturnCustomers()
        {
            // Arrage
            
            var expectedEntities = EntityFactory.GetProducts();
            
            // Act
            
            var actualEntities = _context.Customer.ToList();
            
            // Assert
        
            Assert.AreEqual(expectedEntities.Count,actualEntities.Count);
        }

        [Test]
        public async Task UnitOfWork_GetCustomers_ShouldReturnCustomers()
        {
            // Arrage
            
            var expectedEntities = EntityFactory.GetCustomers();
            
            // Act

            var actualEntities = await _unitOfWork.CustomerRepository.GetAll();
            
            // Assert
        
            Assert.AreEqual(expectedEntities.Count,actualEntities.ToList().Count);
        }
    }
}