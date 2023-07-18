using System.Net;
using AStar.Organisation.Infrastructure.API;
using AStar.Organisation.Infrastructure.DAL.Contexts;
using AStar.Organisation.Infrastructure.IntegrationTests.Factories;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace AStar.Organisation.Infrastructure.IntegrationTests
{
    [TestFixture]
    public class IntegrationTests
    {
        private IntegrationTestsApplicationFactory _factory = new();
        private OrganizationContext _context = null!;
        private HttpClient _client = null!;
        private IServiceScope _scope = null!;
        private WebApplicationFactory<Startup> _webHost = null!;
        
        [SetUp] 
        public void Setup()
        {
            _scope = _factory.Services.CreateScope();
            _context = _scope.ServiceProvider.GetRequiredService<OrganizationContext>();
            _client = _factory.CreateClient();
            _webHost = new IntegrationTestsApplicationFactory().WithWebHostBuilder(_ => { });
        }

        [TearDown] public void TearDown()
        {
            _context.Database.EnsureDeleted(); 
            _scope.Dispose();
            _client.Dispose();
        }

        [Test]
        public async Task CheckDbContext_GetProducts_ShouldReturnProducts()
        {
            // Arrage
            
            var expectedProducts = EntityFactory.GetProducts();
            
            // Act
            
            var actualProducts = _context.Product.ToList();
            
            // Assert
        
            Assert.AreEqual(expectedProducts.Count,actualProducts.Count);
        }
    }
}