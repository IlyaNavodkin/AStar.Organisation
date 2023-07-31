using AStar.Organisation.Core.Application.Dtos;
using AStar.Organisation.Core.Domain.Entities;
using AStar.Organisation.Core.DomainServices.IUnitOfWork;
using AStar.Organisation.Infrastructure.API.Utills;
using AStar.Organization.Infrastructure.BLL.Services;
using AStar.Organization.Infrastructure.BLL.Validators;
using FluentValidation;
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
            _customerValidator = new CustomerValidator();
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
            
            _unitOfWork.Verify(c => c.CustomerRepository.Create(It.IsAny<Customer>()), Times.Once);
            _unitOfWork.Verify(c => c.SaveChanges(), Times.Once);
        } 
        
        [Test]
        public async Task CustomerService_Delete_ShouldDeleteValidUser()
        {
            // Arrange
            
            var validEntity = EntityInitilizeUtill.GetValidCustomer();

            _unitOfWork.Setup(c => c.CustomerRepository.GetById(validEntity.Id))
                .ReturnsAsync(EntityInitilizeUtill.GetValidCustomer());

            var entity = await _unitOfWork.Object.CustomerRepository.GetById(validEntity.Id);
            
            var validDto = new CustomerDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email,
                Phone = entity.Phone,
            };
            
            // Act
            
            var service = new CustomerService(_unitOfWork.Object, _customerValidator);
            await service.Delete(entity.Id);

            // Assert
            
            _unitOfWork.Verify(c => c.CustomerRepository.Delete(validDto.Id), Times.Once);
            _unitOfWork.Verify(c => c.SaveChanges(), Times.Once);
        } 
        
        [Test]
        public async Task CustomerService_Create_ShouldCatchValidationError()
        {
            // Arrange
            
            var invalidEntity = EntityInitilizeUtill.GetNotValidCustomer();

            _unitOfWork.Setup(c => c.CustomerRepository.GetById(invalidEntity.Id))
                .ReturnsAsync(invalidEntity);
            
            // Act
            
            var entity = await _unitOfWork.Object.CustomerRepository.GetById(invalidEntity.Id);
            
            var invalidDto = new CustomerDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email,
                Phone = entity.Phone,
            };
            
            // Assert

            var service = new CustomerService(_unitOfWork.Object, _customerValidator);
            Assert.ThrowsAsync<ValidationException>(async () => await service.Create(invalidDto));
        } 
    }
}