// using AStar.Organisation.Core.Application.Dtos;
// using AStar.Organisation.Core.Application.IServices;
// using AStar.Organisation.Core.Domain.Entities;
// using AStar.Organisation.Core.DomainServices.IRepositories;
// using AStar.Organisation.Core.DomainServices.IUnitOfWork;
// using AStar.Organization.Infrastructure.BLL.Exceptions;
// using AStar.Organization.Infrastructure.BLL.Validators;
// using FluentValidation;
// using Moq;
//
// namespace AStar.Organisation.Infrastructure.UnitTests.Position
// {
//     [TestFixture]
//     public class PositionServiceTests
//     {
//         private Mock<IUnitOfWork> _unitOfWork;
//         private CustomerValidator _customerValidator;
//         private ICustomerService _positionService;
//         
//         [SetUp]
//         public void Setup()
//         {
//             _unitOfWork = new Mock<IUnitOfWork>();
//         }
//
//         [Test]
//         public async Task GetAll_Should_Return_All_PositionDtos()
//         {
//             // Arrange
//             _unitOfWork.Setup(r => r.CustomerRepository.GetAll()).ReturnsAsync(GetAllPositions());
//             var expectedPositions = await _unitOfWork.Object.GetAll();
//
//             // Act
//             var actualPositions = await _positionService.GetAll();
//
//             // Assert
//             Assert.AreEqual(expectedPositions.Count(), actualPositions.Count());
//         }
//         
//         [Test]
//         public async Task GetById_Should_Return_PositionDto()
//         {
//             // Arrange
//             _unitOfWork.Setup(r => r.CustomerRepository.GetById(1)).ReturnsAsync(GetById());
//             var expected = await _unitOfWork.Object.GetById(1);
//
//             // Act
//             var actual = await _positionService.GetById(1);
//
//             // Assert
//             Assert.AreEqual(expected.Name, actual.Name);
//             Assert.AreEqual(expected.Id, actual.Id);
//             Assert.AreEqual(expected.DepartmentId, actual.DepartmentId);
//         }
//         
//         [Test]
//         public async Task GetById_Should_Throw_NotFoundEntityException_When_Position_Not_Found()
//         {
//             // Arrange
//             var id = 1;
//             _unitOfWork.Setup(r => r.CustomerRepository.GetById(1)).ReturnsAsync((Customer) null);
//             
//             // Act & Assert
//             Assert.ThrowsAsync<NotFoundEntityException>(async () => await _positionService.GetById(id));
//         }
//         
//         [Test]
//         public async Task Create_Should_Call_Repository_Create_With_Valid_Entity()
//         {
//             // Arrange
//             var positionDto = new PositionDto { Name = "New Position", DepartmentId = 1 };
//             var entity = new Core.Domain.Entities.Position { Name = positionDto.Name, DepartmentId = positionDto.DepartmentId };
//             _unitOfWork.Setup(r => r
//                     .Create(It.IsAny<Core.Domain.Entities.Position>()))
//                 .Returns(Task.CompletedTask);
//             
//             // Act
//             await _positionService.Create(positionDto);
//
//             // Assert
//             _unitOfWork.Verify(r => r
//                 .Create(It.Is<Core.Domain.Entities.Position>(p => p.Name == entity.Name && 
//                                                                   p.DepartmentId == entity.DepartmentId)), Times.Once);
//         }
//         
//         [Test]
//         [TestCaseSource(nameof(GetNotValidPositions))]
//         public void Create_Should_Throw_ValidationException_When_Entity_Fails_Validation(CustomerDto positionDto)
//         {
//             // Act & Assert
//             Assert.ThrowsAsync<ValidationException>(async () => await _positionService.Create(positionDto));
//         }
//         
//         private static IEnumerable<Customer> GetAllPositions()
//         {
//             var entities = new List<Customer>
//             {
//                 new Customer { Name = "John Doe", Email = "john.doe@example.com", Phone = "123456789" },
//                 new Customer { Name = "Jane Smith", Email = "jane.smith@example.com", Phone = "987654321" },
//                 new Customer { Name = "Bob Johnson", Email = "bob.johnson@example.com", Phone = "555555555" }
//             };
//             
//             return entities;
//         }
//         
//         private static IEnumerable<CustomerDto> GetNotValidPositions()
//         {
//             var entities = new List<CustomerDto>
//             {
//                 new CustomerDto { Name = "John DoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoe", 
//                     Email = "john", Phone = "123456789" },
//             };
//             
//             return entities;
//         }
//
//         private static Customer GetById() => new() { Id = 1, Name = "John Doe", Email = "john.doe@example.com", Phone = "123456789" };
//     }
// }