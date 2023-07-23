using AStar.Organisation.Core.Application.Dtos;
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
        public async Task CustomerService_Create_ShouldCreateValidUser()
        {
            // Arrange
            _unitOfWork.Setup(c => c.CustomerRepository.GetAll()).ReturnsAsync(EntityInitilizeUtill.GetCustomers());
            var validUser = EntityInitilizeUtill.GetValidCustomer();
            var validDto = new CustomerDto
            {
                Name = validUser.Name,
                Email = validUser.Email,
                Phone = validUser.Phone,
            };
            
            // Act
            
            var service = new CustomerService(_unitOfWork.Object, _customerValidator);
            await service.Create(validDto);

            // Assert
            
            
        } 
    }
}