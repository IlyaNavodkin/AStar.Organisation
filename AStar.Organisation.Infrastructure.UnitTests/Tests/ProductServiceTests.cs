using AStar.Organisation.Core.Application.IServices;
using AStar.Organisation.Core.Domain.Entities;
using AStar.Organisation.Core.DomainServices.IUnitOfWork;
using AStar.Organisation.Infrastructure.API.Utills;
using AStar.Organisation.Infrastructure.DAL.Contexts;
using AStar.Organization.Infrastructure.BLL.Services;
using AStar.Organization.Infrastructure.BLL.Validators;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace AStar.Organisation.Infrastructure.UnitTests.Tests
{
    [TestFixture]
    public class ProductServiceTests
    {
        private ProductValidator _customerValidator;
        private ProductService _productService;
        private Mock<OrganizationContext> _context;
        private DbSet<Product> _mockProductDbSet;
         
        [SetUp]
        public void Setup()
        {
            _context = new Mock<OrganizationContext>();
        }
         
        [Test]
        public async Task GetProducts()
        {
            // Arrange
            _context.Setup(c => c.Product.ToList()).Returns(EntityInitilizeUtill.GetProducts);
            
            
        } 
    }
}