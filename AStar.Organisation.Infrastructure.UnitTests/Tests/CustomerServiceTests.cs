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
    public class CustomerServiceTests
    {
        private CustomerValidator _customerValidator;
        private CustomerService _customerService;
        private Mock<IUnitOfWork> _unitOfWork;
         
        [SetUp]
        public void Setup()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
        }
         
        [Test]
        public async Task GetProducts()
        {
            // Arrange
            _unitOfWork.Setup(c => c.CustomerRepository.GetAll()).ReturnsAsync(EntityInitilizeUtill.GetCustomers());
            
            // Act
            
            var service = new CustomerService(_unitOfWork.Object, _customerValidator);

            // Assert
            
            var allCustomers = service.GetAll();
        } 
    }
}